using System.Collections.Generic;
using System.Security;
using UnityEngine;


public abstract class StateMachine : MonoBehaviour
{
    private State _curentState;

    protected ContainerForEnemyComponents _components;
    public ContainerForEnemyComponents Componets { get { return _components; } }

    private void Update()
    {
        if (_curentState != null)
            _curentState.Update();
    } 

    private void SetState(State state)
    {
        _curentState?.Exit();
        _curentState = state;
        _curentState.Enter();
    }

    protected void Init(State initialState, Dictionary<State, Dictionary<Transition, State>> states)
    {
        foreach (var state in states)
        {
            foreach (var transition in state.Value)
            {
                transition.Key.Callback = () => SetState(transition.Value);
                state.Key.AddTransition(transition.Key);
            }
        }

        SetState(initialState);
    }
}
