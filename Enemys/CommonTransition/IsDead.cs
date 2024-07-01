using UnityEngine;

public class IsDead : IsInjured
{
    public IsDead(ContainerForEnemyComponents components) : base(components)
    {

    }

    public override bool CheckCondition()
    {
        return !_components.Health.IsAlive();
    }
}
