using UnityEngine;
using Zenject;

public class PlayerAbilities : MonoBehaviour
{
    private PlayerInput _input;
    private CatchyHook _catchyHook;

    [Inject]
    public void Construct(CatchyHook catchyHook)
    {
        _catchyHook = catchyHook;

        _catchyHook.Construct(transform);   
    }

    private void Awake()
    {
        BindToInputEvents();
    }

    private void OnEnable()
    {
        _input.Enable();
    }

    private void Hook()
    {
        _catchyHook.CreateHook();
    }

    private void Dash()
    {

    }

    private void BindToInputEvents()
    {
        _input = new PlayerInput();

        _input.Player.Hook.performed += context => Hook();
        _input.Player.Dash.performed += context => Dash();
    }

    private void OnDisable()
    {
        _input.Disable();
    }
}
