public class RangeEnemyRangeAttackState : State
{
    private Shooting _shooting;

    private BulletPool _bulletPool;

    public RangeEnemyRangeAttackState(ContainerForEnemyComponents components) : base(components)
    {
        _bulletPool = _components.SelfTransform.GetComponentInChildren<BulletPool>();
        _shooting = new Shooting(_bulletPool, _components.PlayerTransform);
    }

    public override void Enter()
    {
        base.Enter();
        _components.EventReceiver.OnShoot.AddListener(Shooting);

        _components.Animator.SetBool(EnemyAnimationHashed.IsRangeAttack, true);
        _components.NavMesh.isStopped = true;
    }

    public override void Update()
    {
        base.Update();
    }
    
    public override void Exit()
    {
        base.Exit();
        _components.Animator.SetBool(EnemyAnimationHashed.IsRangeAttack, false);
        _components.EventReceiver.OnShoot?.RemoveListener(Shooting);
        _components.NavMesh.isStopped = false;
    }

    private void Shooting() {
        _shooting.Shoot();
    }
}
