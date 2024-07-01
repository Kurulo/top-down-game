using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerSettings
{
    [Header("Move")]
    [SerializeField] private float _movingSpeed;
    [SerializeField] private float _rotatingSpeed;
    [SerializeField] private float _sprintMultiplier;

    [Header("Attack")]
    [SerializeField] private List<NormalAttackSO> _combo;
    [SerializeField] private float _damage = 50f;
    [SerializeField] private LayerMask _enemyLayer;
    [SerializeField] private LayerMask _destroyableLayer;

    [Header("Gravity")]
    [SerializeField] private float _playerMass = 1.0f;
    [SerializeField] private float _fallingSpeed = 2.0f;

    [Header("Check Ground")]
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private float _distanceToGround = 1f;

    // Move
    public float MovingSpeed { get { return _movingSpeed; } }
    public float RotatingSpeed { get { return _rotatingSpeed; } }
    public float SprintMultiplier { get { return _sprintMultiplier; } }

    // Attack 
    public List<NormalAttackSO> Combo { get { return _combo; } }
    public float Damage { get { return _damage; } }
    public LayerMask EnemyLayer { get { return _enemyLayer; } }
    public LayerMask DestroyableLayer { get { return _destroyableLayer; } }

    // Gravity
    public float PlayerMass { get { return _playerMass; } }
    public float FallingSpeed { get { return _fallingSpeed; } }

    // Ground Check
    public float DistanceToGround { get { return _distanceToGround; } }
    public LayerMask GroundMask { get { return _groundMask; } }
}
