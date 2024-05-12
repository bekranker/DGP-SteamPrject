using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AlertState : BaseState
{
    public AlertState(Enemy _enemy, Animator _animator) : base (_enemy, _animator){}

    private float _counter;
    private Transform _player; 

    public override void OnEnter()
    {
        _player = GameObject.FindWithTag("Player").transform;
        _counter = _enemy.EnemyT.AlertTriggerDelay;
        Debug.Log("Entered State: Alert");
        _animator.Play("Idle");
        if (_player.position.x > _enemy.transform.position.x)
        {
            _enemy.SP.flipX = false;
        }
        else
        {
            _enemy.SP.flipX = true;
        }
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