using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class ContainerForEnemyComponents
{
    private EnemySettings _settings;
    private StateMachine _stateMachine;

    private Transform _ownTransform;
    private AudioManager _audioManager;
    private Enemy _enemy;
    private Transform _playerTransform;
    private EnemyRanges _ranges;

    public FieldOfView FOWInfo { get ; private set; }
    public EnemyRanges Ranges { get { return _ranges; } set { _ranges = value; } }
    public Image HealthBar { get; private set; }
    public RagdollSwitcher Ragdoll { get; private set; }
    public EnemySoundsToggle SoundsToggle { get; private set; }
    public AudioManager AudioManager { get { return _audioManager; } }
    public Animator Animator { get; private set; }
    public NavMeshAgent NavMesh { get; private set; }
    public AnimatorEventReceiver EventReceiver { get; private set; }
    public Transform SelfTransform { get { return _ownTransform; } }
    public EnemySettings MeleeEnemySettings { get { return _settings; } }
    public EnemyHealthSystem Health { get; private set; }
    public Enemy Enemy { get { return _enemy; } set { _enemy = value; } }
    public Transform PlayerTransform { get { return _playerTransform; } }

    public EnemyAIData AIData;
    

    public ContainerForEnemyComponents(
        EnemySettings settings, StateMachine stateMachine,
        AudioManager audioManager, Transform playerTransform ) 
    {
        _settings = settings;
        _stateMachine = stateMachine;
        _audioManager = audioManager;
        _playerTransform = playerTransform;

        Init();
    }

    private void Init()
    {
        _ownTransform = _stateMachine.transform;

        AIData = new EnemyAIData();

        NavMesh = _stateMachine.GetComponent<NavMeshAgent>();
        Animator = _stateMachine.GetComponent<Animator>();
        Ragdoll = _stateMachine.GetComponent<RagdollSwitcher>();
        EventReceiver = _stateMachine.GetComponent<AnimatorEventReceiver>();
        FOWInfo = _stateMachine.GetComponent<FieldOfView>();

        HealthBar = _stateMachine.GetComponentInChildren<Image>();
        Health = new EnemyHealthSystem(_settings.MaxHealth);
        SoundsToggle = new EnemySoundsToggle(this, _audioManager);
        _ranges = new EnemyRanges(_settings.ChaseRange, _settings.AproachRange, _settings.AttackRange);
    }
}
