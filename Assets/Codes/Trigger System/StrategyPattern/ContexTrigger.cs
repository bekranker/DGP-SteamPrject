using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ContexTrigger
{
    public ITriggerStrategy _patrollingType;
    public ContexTrigger(ITriggerStrategy state)
    {
        this._patrollingType = state;
    }
    public void SetState(ITriggerStrategy state)
    {
        this._patrollingType = state;
    }
    public void DoSomeLogic()
    {
        _patrollingType.Action();
    }
}
