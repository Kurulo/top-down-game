using UnityEngine;


public class InAproachingRange : DistanceToPlayer
{
    private EnemySettings _ranges;
    private ContainerForEnemyComponents _components;
    public InAproachingRange(Transform origin, Transform player, ContainerForEnemyComponents components) : base(origin, player)
    {
        _ranges = components.MeleeEnemySettings;
        _components = components;
    }

    public override bool CheckCondition()
    {
        if (_components.Health.IsAlive())
        {
            return _distance <= _components.Ranges.AproachRange && _distance >= _components.Ranges.AttackRange;
        }
        else { return false; }
    }
}
