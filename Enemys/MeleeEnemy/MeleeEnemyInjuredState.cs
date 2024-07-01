using System.Collections;
using UnityEngine;


public class MeleeEnemyInjuredState : State
{
    private float _fixedDuration = 0.5f;
    private float _durationStun = 0f;

    private bool _canSwitch;

    public MeleeEnemyInjuredState(ContainerForEnemyComponents components) : base(components) { }
    public override void Enter()
    {
        base.Enter();
        _canSwitch = false;
        _durationStun = _fixedDuration;
        _components.Animator.SetTrigger(EnemyAnimationHashed.Attacked);
        _components.Enemy.Injured();
        _components.Health.OnDemageEvent += CanSwitchState;
    }

    public override void Update()
    {
        _durationStun -= Time.deltaTime;

        if (_durationStun <= 0f || _canSwitch)
        {
            base.Update();
        }
    }

    public override void Exit()
    {
        base.Exit();
        _canSwitch = false;
        _durationStun = _fixedDuration;
        _components.Animator.ResetTrigger(EnemyAnimationHashed.Attacked);
    }

    private void CanSwitchState() => _canSwitch = true;
}
