using UnityEngine;

public class MeleeEnemyAproachState : State
{
    public MeleeEnemyAproachState(ContainerForEnemyComponents components) : base (components)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _components.Animator.SetBool(EnemyAnimationHashed.IsAproaching, true);
        _components.NavMesh.isStopped = false;
        _components.AIData.HasTarget = true;
    }

    public override void Update()
    {
        base.Update();
        _components.Enemy.Aproaching();
    }

    public override void Exit() 
    {
        base.Exit();
        _components.Animator.SetBool(EnemyAnimationHashed.IsAproaching, false);
        _components.NavMesh.isStopped = true;
    }
}
