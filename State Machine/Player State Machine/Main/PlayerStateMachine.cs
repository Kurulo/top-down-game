using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;
using Zenject;

public class PlayerStateMachine : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private AnimatorEventReceiver _animatorEvents;
    [SerializeField] private Animator _playerAnimator;
    [SerializeField] private Image _healthImage;

    [Header("Settings")]
    [SerializeField] private PlayerSettings _playerSettings;

    [Header("Debug")]
    public bool DebuLog = false;

    private CharacterController _characterController;
    private Transform _selfTransform;
    private PlayerInput _input;
    private Camera _mainCamera;

    private bool _isRunning = false;
    private bool _isSprinting = false;
    private bool _isAttack = false;
    private bool _interaction = false;
    private bool _useHeal = false;
    private bool _isGrounded;

    private bool _mouseClicked = false;

    private Vector2 _moveDirection;
    private Vector3 _mouseClickPosition;

    private PlayerBaseState _currentState;
    private PlayerStateFactory _stateFactory;

    private PlayerHealthSystem _health;
    private RagdollSwitcher _ragdoll;
    private InventoryManager _inventory;
    private PlayerSoundsToggle _playerSoundsToggle;

    private Collider _collidingSurface;

    private float _gravity = -9.81f;
    private float _velocity;


    public Collider CollidingSurface { get { return _collidingSurface; } }
    public PlayerSoundsToggle PlayerSoundsToggle { get { return _playerSoundsToggle; } }
    public Vector3 MouseClickPosition { get { return _mouseClickPosition; } }
    public PlayerInput Input { get { return _input; } }
    public Vector2 MoveDirection { get { return _moveDirection; } set { _moveDirection = value; } }
    public Camera MainCamera { get { return _mainCamera; } }
    public PlayerBaseState CurrentState { get { return _currentState; } set { _currentState = value; } }
    public CharacterController CharacterController { get { return _characterController; } }
    public Transform SelfTransform { get { return _selfTransform; } }
    public Animator PlayerAnimator { get { return _playerAnimator; } }
    public PlayerHealthSystem Health { get { return _health; } }
    public PlayerSettings PlayerSettings { get { return _playerSettings; } }
    public AnimatorEventReceiver AnimatorEvents { get { return _animatorEvents; } }

    public float Gravity { get { return _gravity; } }
    public float Velocity { get { return _velocity; } set { _velocity = value; } }

    public bool IsAttack { get { return _isAttack; } set { _isAttack = value; } }
    public bool IsRunning { get { return _isRunning; } set { _isRunning = value; } }
    public bool IsSprinting { get { return _isSprinting; } set { _isSprinting = value; } }
    public bool Interaction { get { return _interaction; } }
    public bool IsGrounded { get { return _isGrounded; } set { _isGrounded = value; } }
    public bool MouseClicked { get { return _mouseClicked; } }

    private Vector3 startDrawPos;
    private Vector3 controllStart;

    private float radious;

    private AudioManager _audio;

    [Inject]
    private void Construct(AudioManager audio)
    {
        _audio = audio;
    }

    private void OnEnable()
    {
        _input.Enable();
    }

    private void Awake()
    {
        SetInputVariables();
        GetComponents();

        radious = CharacterController.radius;

        _health = new PlayerHealthSystem(100f);
        _inventory = GetComponent<InventoryManager>();
        _playerSoundsToggle = new PlayerSoundsToggle(this, _audio);
    }

    private void Start()
    {
        SetStartState();
    }

    private void Update()
    {
        Death();

        if (_health.IsAlive())
        {
            CheckGrounded();

            if (_isGrounded)
            {
                ReadDirection();
            }

            _currentState.UpdateStates();
        }
        else
        {
            StartCoroutine(ReloadLevel());
        }
    }


    public void TakeDamage(float damage)
    {
        _health.DecreaseHealth(damage);

        float fill = _health.CurrentHp() / _health.MaxHealth();
        _healthImage.fillAmount = fill;
    }

    private IEnumerator ReloadLevel()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void ReadDirection()
    {
        _moveDirection = _input.Player.Move.ReadValue<Vector2>();

        if (_moveDirection != Vector2.zero)
            _isRunning = !_isRunning ? true : _isRunning;
        else
            _isRunning = _isRunning ? false : _isRunning;   
    }

    public Vector3 SlopeVelocity(Vector3 velocity)
    {
        Ray ray = new Ray(_selfTransform.position, Vector3.down);

        if (Physics.Raycast(ray, out RaycastHit hit, 0.75f))
        {
            Quaternion slopeRotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
            Vector3 adjustVelocity = slopeRotation * velocity;

            if (adjustVelocity.y < 0)
            {
                return adjustVelocity;
            }
        }

        return velocity;
    }

    private void CheckGrounded()    
    {
        RaycastHit hit;

        startDrawPos = _selfTransform.position;

        controllStart = new Vector3(0f, _characterController.center.y, 0f);

        if (Physics.SphereCast(startDrawPos + controllStart, radious, -_selfTransform.up, out hit, _playerSettings.DistanceToGround, _playerSettings.GroundMask))
            _isGrounded = true;
        else
            _isGrounded = _characterController.isGrounded ? true : false;

        _collidingSurface = hit.collider;
    }

    private void SprintPerformed()
    {
        _isSprinting = true;
    }

    private void SprintCanceled()
    {
        _isSprinting = false;
    }

    private void Death()
    {
        if (!_health.IsAlive()) 
        {
            _ragdoll.OnRagdoll();
        }
    }

    private void MouseClickStarted()
    {
        if (!_mouseClicked)
        {
            if (!_isAttack)
            {
                _isAttack = true;
            }

            _mouseClicked = true;
            _mouseClickPosition = _input.Player.MousePostion.ReadValue<Vector2>();
        }
    }

    private void MouseClickCanceled()
    {
        _mouseClicked = false;
    }

    private void InteractionPerformd()
    {
        if (!_interaction)
        {
            _interaction = true;
        }
    }

    private void InteractionCanceled()
    {
        _interaction = false;
    }

    private void SetStartState()
    {
        CheckGrounded();

        _stateFactory = new PlayerStateFactory(this);

        if(_isGrounded)
            _currentState = _stateFactory.Ground();
        else
            _currentState = _stateFactory.Fall();
    
        _currentState.EnterState();
    }

    private void GetComponents()
    {
        _characterController = GetComponent<CharacterController>();
        _selfTransform = GetComponent<Transform>();
        _ragdoll = GetComponent<RagdollSwitcher>();

        _mainCamera = Camera.main;
    }

    private void UseHealStarted()
    {
        if (!_useHeal)
        { 
            if (_inventory.IsItemInInventory(PotionType.Health))
            {
                _inventory.UsePotion(PotionType.Health);
            }
            else
            {
                Debug.Log("No no no");
            }

            _useHeal = true;
        }
    }

    private void UseHealCanceled()
    {
        _useHeal = false;
    }

    private void SetInputVariables()
    {
        _input = new PlayerInput();

        _input.Player.Sprint.performed += context => SprintPerformed();
        _input.Player.Sprint.canceled += context => SprintCanceled();

        _input.Player.Attack.started += context => MouseClickStarted();
        _input.Player.Attack.canceled += context => MouseClickCanceled();

        _input.Player.Interaction.started += context => InteractionPerformd();
        _input.Player.Interaction.canceled += context => InteractionCanceled();

        _input.Player.UseHeal.started += context => UseHealStarted();
        _input.Player.UseHeal.canceled += context => UseHealCanceled();
    } 

    private void OnDisable()
    {
        _input.Disable();
    }
}