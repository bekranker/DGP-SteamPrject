using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowState : BaseState
{
    public FollowState(Enemy _enemy, Animator _animator) : base(_enemy, _animator){}


    public override void OnEnter()
    {
        Debug.Log("Entered State: Follow");
        base.OnEnter();
    }
    public override void FixedUpdate()
    {
        _enemy.Movement.MoveTo();
        base.FixedUpdate();
    }
}