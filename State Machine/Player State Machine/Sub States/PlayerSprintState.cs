using Unity.VisualScripting;
using UnityEngine;


public class PlayerSprintState : PlayerBaseState
{
    private PlayerMoving _playerMoving;

    public PlayerSprintState(PlayerStateMachine context, PlayerStateFactory factory) : base(context, factory) {}

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
        MovePlayer(Context.MoveDirection);
        RotatePlayer(Context.MoveDirection);
        
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
        else if (Context.IsRunning && !Context.IsSprinting)
        {
            SwitchState(Factory.Run());
        }
    }

    public override void ExitState()
    {
        Context.PlayerAnimator.SetBool(PlayerAnimationHashed.IsRunnigHashed, false);
    }


    private void MovePlayer(Vector2 direction)
    {
        float speedMultiplier = Context.PlayerSettings.SprintMultiplier;

        _playerMoving.MovePlayer(direction, speedMultiplier);
    }

    private void RotatePlayer(Vector2 direction)
    {
        _playerMoving.RotatePlayer(direction);
    }
}
