public class RangeEnemyCheckState : State {
    public RangeEnemyCheckState(ContainerForEnemyComponents components) : base(components) { }

    public override void Enter() {
        base.Enter();
        _components.Ranges.ChasingRange = _components.Ranges.ChasingRange * 2f;
        _components.NavMesh.isStopped = true;
        _components.Animator.SetBool(EnemyAnimationHashed.IsChecking, true);
    }

    public override void Update() {
         base.Update();
    }

    public override void Exit() {
        base.Exit();
        _components.NavMesh.isStopped = false;
        _components.Animator.SetBool(EnemyAnimationHashed.IsChecking, false);
        _components.Ranges.ChasingRange = _components.Ranges.ChasingRange / 2f;
    }
}
