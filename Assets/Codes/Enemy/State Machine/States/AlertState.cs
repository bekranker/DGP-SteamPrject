using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AlertState : BaseState
{
    public AlertState(Enemy _enemy, Animator _animator) : base (_enemy, _animator){}

    private float _counter;


    public override void OnEnter()
    {
        _counter = _enemy.EnemyT.AlertTriggerDelay;
        Debug.Log("Entered State: Alert");
        base.OnEnter();
    }
    public override void Update()
    {
        WaitForAlert();
        base.Update();
    }
    public override void OnExit()
    {
        Debug.Log("Exited State: Alert");
        _counter = _enemy.EnemyT.AlertTriggerDelay;
        base.OnExit();
    }
    private void WaitForAlert()
    {
        if (_counter <= 0)
        {
            _enemy.Alerted = true;
        }
        else
        {
            _counter -= Time.deltaTime;
        }
    }
}