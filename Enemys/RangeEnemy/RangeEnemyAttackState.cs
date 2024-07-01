using UnityEngine;


public class RangeEnemyAttackState : State
{
    private bool _canSwitchState = false;

    public RangeEnemyAttackState(ContainerForEnemyComponents components) : base(components)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _components.EventReceiver.OnEndAnimation.AddListener(CheckCanSwitchState);

        _components.Animator.SetBool(EnemyAnimationHashed.IsMeleeAttack, true);
        _canSwitchState = false;
    }

    public override void Update()
    {
        if (!_canSwitchState) { return; }
        base.Update();
    }

    public override void Exit()
    {
        base.Exit();
        _components.Animator.SetBool(EnemyAnimationHashed.IsMeleeAttack, false);

        _components.EventReceiver.OnEndAnimation?.AddListener(CheckCanSwitchState);
    }

    private void CheckCanSwitchState()
    {
        Vector3 selfPos = _components.SelfTransform.position;
        Vector3 playerPos = _components.PlayerTransform.position;
        float attackRange = _components.MeleeEnemySettings.AttackRange;

        if (Vector3.Distance(selfPos, playerPos) > attackRange)
            _canSwitchState = true;
        else
            _canSwitchState = false;
    }
}
