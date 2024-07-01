public class MeleeEnemyChasingState : State
{
    public MeleeEnemyChasingState(ContainerForEnemyComponents components) : base(components){ }

    public override void Enter()
    {
        base.Enter();
        _components.Ranges.ChasingRange = _components.Ranges.ChasingRange * 2f;
        _components.Animator.SetBool(EnemyAnimationHashed.IsChasing, true);
        _components.NavMesh.isStopped = false;
        _components.AIData.HasTarget = true;
    }

    public override void Update()
    {
        base.Update();
        _components.Enemy.Chasing();
    }

    public override void Exit()
    {
        _components.Ranges.ChasingRange = _components.Ranges.ChasingRange / 2f;
        _components.Animator.SetBool(EnemyAnimationHashed.IsChasing, false);
        _components.NavMesh.isStopped = true;
    }
}
