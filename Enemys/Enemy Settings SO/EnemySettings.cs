using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemies/Melee/StandartEnemy", fileName = "New Enemy")]
public class EnemySettings : ScriptableObject
{
    [Header("Enemy Type")]
    [Tooltip("Enemy type"), SerializeField] private EnemyTypes _enemyType;

    [Header("Moving")]
    [Tooltip("Enemy moving speed")][SerializeField] private float _movingSpeed;
    [Tooltip("Enemy turn speed to target")][SerializeField] private float _turnSpeed;

    [Header("Range Setting")]
    [Tooltip("View Angle"), SerializeField, Range(0, 360)] private float _viewAngle;
    [Tooltip("Range when enemy can attack")][SerializeField] private float _attackRange;
    [Tooltip("Aproach distance between attack")][SerializeField] private float _aproachRange;
    [Tooltip("Range when enemy can chase")][SerializeField] private float _chaseRange;
    [Tooltip("Range attack range"), SerializeField] private float _rangeAttackRange;

    [Header("Attack")]
    [Tooltip("Interval between attack")][SerializeField] private float _attackInterval;
    [Tooltip("Damage attack")][SerializeField] private float _damage;
    [Tooltip("Main player layer")][SerializeField] private LayerMask _playerLayer;

    [Header("Helth")]
    [Tooltip("Enemy health")][SerializeField] private float _maxHealth;

    // Core
    public EnemyTypes EnemyType { get { return _enemyType; } }

    // Moving 
    public float MovingSpeed { get { return _movingSpeed; } set { _movingSpeed = value; } }
    public float TurnSpeed { get { return _turnSpeed; } set { _turnSpeed = value; } }

    // Range Settings   
    public float ViewAngle { get { return _viewAngle; } }
    public float AttackRange { get { return _attackRange; } set { _attackRange = value; } }
    public float RangeAttackRange { get { return _rangeAttackRange; } set { _rangeAttackRange = value; } }
    public float AproachRange { get { return _aproachRange; } set { _aproachRange = value; } }
    public float ChaseRange { get { return _chaseRange; } set { _chaseRange = value; } }

    // Attack 
    public float AttackInterval { get { return _attackInterval; } set { _attackInterval = value; } }
    public float Damage { get { return _damage; } set { _damage = value; } }
    public LayerMask PlayerLayer { get { return _playerLayer; } }

    // Health
    public float MaxHealth { get { return _maxHealth;} }
}
