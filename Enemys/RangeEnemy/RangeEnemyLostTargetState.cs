
using System;
using UnityEngine;

public class RangeEnemyLostTargetState : State {
    private Vector3 _lastTargetPos;

    public RangeEnemyLostTargetState(ContainerForEnemyComponents components) : base(components) {}

    public override void Enter()
    {
        base.Enter();
        _components.AIData.HasTarget = false;
        _components.NavMesh.isStopped = false;

        _components.Ranges.ChasingRange = _components.Ranges.ChasingRange * 2f;
        _components.AIData.LastTargetPos = _components.PlayerTransform.position;
        _lastTargetPos = _components.AIData.LastTargetPos;

        FixPosition();

        _components.NavMesh.SetDestination(_components.AIData.LastTargetPos);
        _components.Animator.SetBool(EnemyAnimationHashed.IsAproaching, true);
    }

    private void FixPosition()
    {
        RaycastHit hit;

        if (Physics.Raycast(_components.AIData.LastTargetPos, Vector3.down, out hit, 100f, LayerMask.GetMask("Ground")))
        {
            Vector3 hitVec = hit.point;
            float distance = Vector3.Distance(_components.AIData.LastTargetPos, hitVec);

            if (distance !< 2f)
            {
                _lastTargetPos.y -= distance;
                _components.AIData.LastTargetPos = _lastTargetPos;
            }
        }
    }

    public override void Update() {
        base.Update();
    }

    public override void Exit() {
        base.Exit();
        _components.Ranges.ChasingRange = _components.Ranges.ChasingRange / 2f;
        _components.Animator.SetBool(EnemyAnimationHashed.IsAproaching, false);
    }
}
