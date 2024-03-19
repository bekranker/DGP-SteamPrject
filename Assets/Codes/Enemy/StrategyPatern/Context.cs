using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Context
{
    public Patrolling _patrollingType;
    public Context(Patrolling state)
    {
        this._patrollingType = state;
    }
    public void SetState(Patrolling state)
    {
        this._patrollingType = state;
    }
    public void DoSomeLogic()
    {
        _patrollingType.PatrollingFunction(default);
    }
}