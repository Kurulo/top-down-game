using UnityEngine;

public class InChasingRange : DistanceToPlayer
{
    private EnemySettings _ranges;
    private ContainerForEnemyComponents _components;

    public InChasingRange(Transform origin, Transform player, ContainerForEnemyComponents components) : base(origin, player)
    {
        _ranges = components.MeleeEnemySettings;
        _components = components;
    }

    public override bool CheckCondition()
    {
        if (_components.Health.IsAlive())
        {
            if (_distance <= _components.Ranges.ChasingRange && 
                _distance > _components.Ranges.AproachRange && 
                _distance > _components.MeleeEnemySettings.RangeAttackRange)
            {
                if (IsPlaeyerInFOW()) return true;
                else return false;
            }
            else { return false; }
        }
        
        return false;
    }

    private bool IsPlaeyerInFOW()
    {
        if (!_components.AIData.HasTarget && _components.FOWInfo.PlayerInVisibleRange)
        {
            Debug.Log(_components.AIData.HasTarget);
            return true;
        }       
        else if (_components.AIData.HasTarget)
        {  
            return true;
        }
        else return false;
    }
}
