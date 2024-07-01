using UnityEngine;

public class PlayerGroundState : PlayerBaseState
{
    public PlayerGroundState(PlayerStateMachine context, PlayerStateFactory factory) : base(context, factory)
    {
        IsRootState = true;
    }

    public override void EnterState()
    {
        InitializeSubState();
    }

    public override void InitializeSubState()
    {
        if (!Context.IsRunning)
        {
            SetSubState(Factory.Idle());
        }
        else if (Context.IsRunning && !Context.IsSprinting)
        {
            SetSubState(Factory.Run());
        }
        else if (Context.IsSprinting && Context.IsRunning)
        {
            SetSubState(Factory.Sprint());
        }
    }

    public override void UpdateState()
    {
        CheackSwitchState();
    }

    public override void CheackSwitchState()
    {
        if (!Context.IsGrounded)
        {
            SwitchState(Factory.Fall());
        }
    }

    public override void ExitState()
    {
       
    }
}
