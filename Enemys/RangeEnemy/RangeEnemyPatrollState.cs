using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RangeEnemyPatrollState : State
{
    private PatrollWaypoints _waypoints;

    private NavMeshAgent _agent;
    private List<Vector3> _path = new List<Vector3>();

    private bool _changeRotation = false;

    private int _wpId = 1;

    public RangeEnemyPatrollState(ContainerForEnemyComponents components) : base(components)
    {
        _agent = _components.NavMesh;
        _waypoints = _components.SelfTransform.GetComponent<PatrollWaypoints>();
    }

    public override void Enter()
    {
        base.Enter();

        _components.Animator.SetBool(EnemyAnimationHashed.IsPatroll, true);

        _components.AIData.HasTarget = false;
        _agent.isStopped = false;
        _path = _waypoints.GetPath();

        _agent.speed = _components.MeleeEnemySettings.MovingSpeed;
        _agent.SetDestination(_path[_wpId]);
    }

    public override void Update()
    {
        base.Update();

        Vector3 worldPos = _components.SelfTransform.TransformPoint(Vector3.up);
        float distance = Vector3.Distance(worldPos, _path[_wpId]);

        if (_wpId == _path.Count - 1) 
            _changeRotation = true;
        else if (_wpId == 0)
            _changeRotation = false;

        if (distance < 2f)
        {
            if (!_changeRotation)
            {
                _wpId++;
                _agent.SetDestination(_path[_wpId]);
            }
            else
            {
                _wpId--;
                _agent.SetDestination(_path[_wpId]);
            }
        }
    }

    public override void Exit()
    {
        base.Exit();
        _agent.isStopped = true;
        _components.Animator.SetBool(EnemyAnimationHashed.IsPatroll, false);
    }
}
