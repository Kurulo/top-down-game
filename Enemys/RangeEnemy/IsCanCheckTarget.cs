using UnityEngine;


public class IsCanCheckTarget : DistanceToPlayer {
    private ContainerForEnemyComponents _components;

    public IsCanCheckTarget(Transform origin, Transform player, ContainerForEnemyComponents components) : base(origin, player) {
        _components = components;
    }

    public override bool CheckCondition() {
        return DistanceToTarget(_components.AIData.LastTargetPos) < 1f;
    } 
}