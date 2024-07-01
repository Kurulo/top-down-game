using ModestTree;
using System.Collections;
using UnityEngine;

public class PlayerFallState : PlayerBaseState
{
    public PlayerFallState(PlayerStateMachine currentContext, PlayerStateFactory factory) 
        : base(currentContext, factory)
    {
        IsRootState = true;
    }

    public override void EnterState()
    {
        InitializeSubState();
    }

    public override void UpdateState()
    {
        SetGravity();
        CheackSwitchState();
    }

    private void SetGravity()
    {
        Context.Velocity = Context.Gravity * Context.PlayerSettings.PlayerMass * Time.deltaTime;

        float smoothedSpeed = Context.PlayerSettings.FallingSpeed * Time.deltaTime;

        Vector3 moveGravity = new Vector3(Context.MoveDirection.x, 0f, Context.MoveDirection.y);
        moveGravity *= 0.25f;
      
        moveGravity.y = Context.Velocity;

        Context.CharacterController.Move(moveGravity * smoothedSpeed);
    }

    public override void CheackSwitchState()
    {
        if (Context.IsGrounded)
        {
            SwitchState(Factory.Ground());
        }
    }

    public override void InitializeSubState()
    {

    }

    public override void ExitState()
    {
        Context.Velocity = 0.0f;
    }
}
