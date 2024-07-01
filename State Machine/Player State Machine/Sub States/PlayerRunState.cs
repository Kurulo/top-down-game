using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.ProBuilder.Shapes;

public class PlayerRunState : PlayerBaseState
{
    private PlayerMoving _playerMoving;

    public PlayerRunState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
    : base (currentContext, playerStateFactory) {}

    public override void EnterState()
    {
        if (_playerMoving == null) _playerMoving = new PlayerMoving(Context);

        Context.PlayerAnimator.SetBool(PlayerAnimationHashed.IsRunnigHashed, true);
    }

    public override void InitializeSubState()
    {
        
    }

    public override void UpdateState()
    {
        if (_playerMoving != null)
        {
            MovePlayer(Context.MoveDirection);
            RotatePlayer(Context.MoveDirection);
        }

        CheackSwitchState();
    }

    public override void CheackSwitchState()
    {
        if (Context.IsAttack)
        {
            SwitchState(Factory.Attack());
        }
        else if (!Context.IsRunning)
        {
            SwitchState(Factory.Idle());
        }
        else if (Context.IsSprinting)
        {
            SwitchState(Factory.Sprint());
        }
    }

    public override void ExitState()
    {
        Context.PlayerAnimator.SetBool(PlayerAnimationHashed.IsRunnigHashed, false);
    }

    private void MovePlayer(Vector2 direction)
    {
        _playerMoving.MovePlayer(direction);
    }

    private void RotatePlayer(Vector2 direction)
    {
        _playerMoving.RotatePlayer(direction);
    }
}
