using UnityEngine;

public class RagdollSwitcher : MonoBehaviour
{
    [Header("Main References")]
    [SerializeField] private CapsuleCollider _mainCollider;
    [SerializeField] private Animator _animator;
    [SerializeField] private Rigidbody _mainRigidbody;

    [Header("For Ragdoll References")]
    [SerializeField] private GameObject _ragdollRig;

    [Header("State info")]
    [SerializeField] private bool On = false;

    // Ragdoll parts
    private Collider[] _ragdollColliders;
    private Rigidbody[] _ragdollRigidbodyes;

    void Start()
    {
        GetRagdollComponents();
        OffRagdoll();
    }

    void Update()
    {
        if (On)
        {
            OnRagdoll();
            On = false;
        }
    }

    public void OnRagdoll() 
    {

        _animator.enabled = false;

        foreach (Collider collider in _ragdollColliders)
        {
            collider.enabled = true;
            collider.gameObject.layer = LayerMask.NameToLayer("Ragdoll");

        }

        foreach (Rigidbody rigidbody in _ragdollRigidbodyes)
        {
            rigidbody.isKinematic = false;
        }

        _mainCollider.enabled = false;
    }

    public void OffRagdoll()
    {
        foreach (Collider collider in _ragdollColliders)
        {
            collider.enabled = false;
        }

        foreach (Rigidbody rigidbody in _ragdollRigidbodyes)
        {
            rigidbody.isKinematic = true;
        }

        _mainCollider.enabled = true;
        _animator.enabled = true;
    }

    private void GetRagdollComponents()
    {
        _ragdollColliders = _ragdollRig.GetComponentsInChildren<Collider>();
        _ragdollRigidbodyes = _ragdollRig.GetComponentsInChildren<Rigidbody>();
    }
}
