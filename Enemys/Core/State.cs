using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    protected List<Transition> _transitions = new List<Transition>();

    protected ContainerForEnemyComponents _components;

    public State(ContainerForEnemyComponents components) 
    {
        _components = components;
    }

    public void AddTransition(Transition transition)
    {
        _transitions.Add(transition);
    }

    public virtual void Enter()
    {
        foreach (var transition in _transitions) { transition.Enter(); }
    }

    public virtual void Update()
    {
        foreach (var transition in _transitions) 
        {
            transition.Update(); 
        }
    }

    public virtual void Exit()
    {
        foreach (var transition in _transitions) { transition.Exit(); }
    }
}
