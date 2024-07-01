using UnityEngine;


public class InAttackRange : DistanceToPlayer
{
    private ContainerForEnemyComponents _components;

    public InAttackRange(Transform origin, Transform player, ContainerForEnemyComponents components) : base(origin, player)
    {
        _components = components;
    }

    public override bool CheckCondition()
    {
        if (_components.Health.IsAlive())
        {
            return _distance <= _components.MeleeEnemySettings.AttackRange;
        }
        else { return false; }
    }
}
