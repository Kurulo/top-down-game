using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour {
    // Inspector
    [Header("Settings")]
    [SerializeField] private float _speed = 10f;
    [SerializeField] private float _damage = 10f;
    [SerializeField] private float _offsetYDestination = 10f;

    [Header("Coponents")]
    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private ParticleSystem _particles;

    // Public Variables
    public float Speed { get { return _speed; } }
    public float Damage { get { return _damage; } }

    // Private Variables
    private Vector3 _destination = Vector3.zero;
    private const float _lifeTime = 5f;
    private float _leftLT;

    // Components
    private Transform _spawnTransform;
    private BulletPool _pool;
    private Rigidbody _rb;
    private SphereCollider _collider;



    // Initializing (Contstructor)
    public void Init(Transform spawnTransform, BulletPool pool) {
        _spawnTransform = spawnTransform;
        _pool = pool;
    }

    // Unity Methods

    private void OnEnable() {
        if (_rb == null) _rb = GetComponent<Rigidbody>();
        if (_collider == null) _collider = GetComponentInChildren<SphereCollider>();

        _collider.enabled = true;
        _rb.isKinematic = false;
        _meshRenderer.enabled = true;
        transform.position = _spawnTransform.position;
        transform.LookAt(_destination);

        _leftLT = _lifeTime;

        SetLocalVelocity();
    }

    private void Update() {
        if (_destination != Vector3.zero) {
            MoveToDestination();
        }

        _leftLT -= Time.deltaTime;

        if ( _leftLT < 0 ) {
            ReturnToPool();
        }
    }

    private void OnCollisionEnter(Collision collision) {
        Debug.Log("I Hit something");
        _meshRenderer.enabled = false;
        _rb.isKinematic = true;
        _collider.enabled = false;

        StartCoroutine(HitProcess(1f));
    }

    // Public Methods

    public void SetDestination(Vector3 destination) {
        _destination = destination;
        _destination.y += _offsetYDestination;
    }

    // Private Methods

    private void MoveToDestination() {
        _rb.AddForce(transform.forward * _speed * Time.deltaTime, ForceMode.Impulse);
    }

    private void SetLocalVelocity() {
        var locVel = transform.InverseTransformDirection(_rb.velocity);
        locVel = new Vector3 (0f, 0f, _speed);
        _rb.velocity = transform.TransformDirection(locVel);
    }

    private IEnumerator HitProcess(float wait) {
        _particles.Play();
        yield return new WaitForSeconds(wait);

        ReturnToPool();
    }

    private void ReturnToPool() {
        gameObject.SetActive(false);

        _pool.ReturnBullet(this);
    }
}
