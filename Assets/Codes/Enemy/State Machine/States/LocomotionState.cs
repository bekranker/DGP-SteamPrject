using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocomotionState : BaseState
{
    public LocomotionState(Enemy _enemy, Animator _animator) : base(_enemy, _animator){}


    private float _counter;


    public override void OnEnter()
    {
        _counter = _enemy.EnemyT.PatrollDelay;
        _enemy.Alerted = false; 
        Debug.Log("Entered State: Locomotion");
        base.OnEnter();
    }
    public override void OnExit()
    {
        _enemy.Locomotion = false;
        base.OnExit();
    }
    public override void Update()
    {
        if (_counter > 0)
        {
            _counter -= Time.deltaTime;
        }
        else
        {
            _enemy.Locomotion = true;
        }
        base.Update();
    }
}