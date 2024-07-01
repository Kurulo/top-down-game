using System.Collections;
using UnityEngine;


public class TimerTransition : Transition
{
    private float _endTime;
    private float _currentTime;

    public TimerTransition(float endTime)
    {
        _endTime = endTime;
    }

    public override void Enter()
    {
        base.Enter();
        _currentTime = 0f;
    }

    public override bool CheckCondition()
    {
        _currentTime += Time.deltaTime;

        if (_currentTime > _endTime)
            return true;
        else return false;
    }
}
