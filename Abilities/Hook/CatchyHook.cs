using UnityEngine;

public class CatchyHook : MonoBehaviour
{
    private HookRaycast _hookRaycast;
    private HookRender _hookRender;

    private Transform _player;

    private void Start()
    {
        GetComponents();
    }

    public void Construct(Transform player)
    {
        _player = player;
    }

    public void CreateHook()
    {
        RaycastHit hit = _hookRaycast.LaunchRay(_player);

        if (hit.collider != null)
            Debug.Log($"Hit whith: {hit.collider.name}");
    }

    public void DisableHook()
    {

    }

    private void GetComponents()
    {
        _hookRaycast = GetComponent<HookRaycast>();
        _hookRender = GetComponent<HookRender>();
    }
}
