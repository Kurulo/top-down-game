using UnityEngine;


public class MeleeEnemyAttackState : State
{
    private bool _canSwitch;

    public MeleeEnemyAttackState(ContainerForEnemyComponents components) : base(components)
    {
        _components.EventReceiver.OnEndAnimation.AddListener(CheckCanSwitchState);
        _components.Health.OnDemageEvent += CanSwitchState; 
    }

    public override void Enter()
    {
        base.Enter();
        _canSwitch = false;
        _components.Animator.SetBool(EnemyAnimationHashed.IsAttack, true);
    }

    public override void Update()
    {
        if (_canSwitch)
        {
            base.Update();
        }
    }

    public override void Exit() 
    { 
        base.Exit();
        _canSwitch = false;
        _components.Animator.SetBool(EnemyAnimationHashed.IsAttack, false);
        _components.EventReceiver.OnEndAnimation?.RemoveListener(CheckCanSwitchState);
    }

    private void CheckCanSwitchState()
    {
        Vector3 selfPos = _components.SelfTransform.position;
        Vector3 playerPos = _components.PlayerTransform.position;
        float attackRange = _components.MeleeEnemySettings.AttackRange;

        if (Vector3.Distance(selfPos, playerPos) > attackRange)
            _canSwitch = true;
        else
            _canSwitch = false;
    }

    private void CanSwitchState() => _canSwitch = true;
}
