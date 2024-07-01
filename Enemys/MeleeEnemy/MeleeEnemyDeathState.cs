using UnityEngine;

public class MeleeEnemyDeathState : State
{

    public MeleeEnemyDeathState(ContainerForEnemyComponents components) : base(components){ }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("You in death state");
        _components.Animator.SetTrigger(EnemyAnimationHashed.Death);
    }

    public override void Exit()
    {
        base.Exit();
        _components.Animator.ResetTrigger(EnemyAnimationHashed.Death);
    }
}
