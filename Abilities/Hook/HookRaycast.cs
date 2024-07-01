using UnityEngine;

public class HookRaycast : MonoBehaviour
{
    [Header("Raycast Setting")]
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private float _maxDistance;

    public RaycastHit LaunchRay(Transform shootFrom)
    {
        RaycastHit hit;
        Physics.Raycast(shootFrom.position, shootFrom.forward, out hit, _maxDistance, _layerMask);

        return hit;
    }
}
