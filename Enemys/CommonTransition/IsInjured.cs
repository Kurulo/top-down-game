using System.Collections;
using UnityEngine;


public class IsInjured : Transition
{
    protected ContainerForEnemyComponents _components;
    private bool _isAttacked;

    public IsInjured(ContainerForEnemyComponents components)
    {
        _components = components;

        _isAttacked = false;
        _components.Health.OnDemageEvent += SetIsAttacked;
    }

    public override bool CheckCondition()
    {
        if (_isAttacked)
        {
            _isAttacked = false;
            return true;
        }
        else return false;
    }

    private void SetIsAttacked()
    {
        _isAttacked = true;
    }
}
