using System.Collections;
using UnityEngine;


public class ExitFromAttackState : Transition
{
    private ContainerForEnemyComponents _components;

    public ExitFromAttackState(ContainerForEnemyComponents components)
    {
        _components = components;
    }


    public override bool CheckCondition()
    {
        return _components.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1f;
    }
}
