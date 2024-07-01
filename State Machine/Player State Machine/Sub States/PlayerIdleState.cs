using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    public PlayerIdleState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
    : base (currentContext, playerStateFactory) {}

    public override void EnterState()
    {

    }

    public override void InitializeSubState()
    {

    }

    public override void UpdateState()
    {
        CheackSwitchState();
    }

    public override void CheackSwitchState()
    {
        if (Context.IsAttack)
        {
            SwitchState(Factory.Attack());
        }
        else if (Context.IsRunning && Context.IsSprinting)
        {
            SwitchState(Factory.Sprint());
        }
        else if (Context.IsRunning)
        {
            SwitchState(Factory.Run());
        }
    }

    public override void ExitState()
    {

    }
}
