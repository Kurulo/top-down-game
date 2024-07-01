using System;
using UnityEngine;

public abstract class DistanceToPlayer : Transition
{
    private Transform _origin;
    private Transform _player;

    protected DistanceToPlayer(Transform origin, Transform player)
    {
        _origin = origin;
        _player = player;
    }

    protected float DistanceToTarget(Vector3 target) => Vector3.Distance(_origin.position, target);

    private Vector3 _playerPos => _player.position;

    protected float _distance => Vector3.Distance(_origin.position, _playerPos);
}
