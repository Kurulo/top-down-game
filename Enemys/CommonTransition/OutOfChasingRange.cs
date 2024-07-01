using UnityEngine;


public class OutOfChasingRange : DistanceToPlayer
{
    private EnemySettings _ranges;
    private ContainerForEnemyComponents _components;
    public OutOfChasingRange(Transform origin, Transform player, ContainerForEnemyComponents components) : base(origin, player)
    {
        _ranges = components.MeleeEnemySettings;
        _components = components;
    }

    public override void Enter()
    {
       
    }

    public override bool CheckCondition()
    {
        return _distance > _components.Ranges.ChasingRange;
    }
}
