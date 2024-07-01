using UnityEngine;


public class InRangeAttackRange : DistanceToPlayer
{
    private ContainerForEnemyComponents _components;

    public InRangeAttackRange(Transform origin, Transform player, ContainerForEnemyComponents components) : base(origin, player)
    {
        _components = components;
    }

    public override bool CheckCondition()
    {
        if (_components.Health.IsAlive())
        {
            return _distance <= _components.MeleeEnemySettings.RangeAttackRange 
                && _distance >= _components.MeleeEnemySettings.AproachRange;
        }
        else { return false; }
    }
}
