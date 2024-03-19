using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AlertState : BaseState
{
    public AlertState(Enemy _enemy, Animator _animator) : base (_enemy, _animator){}

    override public void OnEnter()
    {
        Debug.Log("Entered State: Alert");
        base.OnEnter();
    }
    public override void OnExit()
    {
        Debug.Log("Exited State: Alert");
        base.OnExit();
    }
}