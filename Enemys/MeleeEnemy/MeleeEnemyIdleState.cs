public class MeleeEnemyIdleState : State
{
    public MeleeEnemyIdleState(ContainerForEnemyComponents components) : base(components){ }

    public override void Enter()
    {
        base.Enter();
        _components.NavMesh.isStopped = true;
    }
}
