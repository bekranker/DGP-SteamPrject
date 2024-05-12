using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowState : BaseState
{
    public FollowState(Enemy _enemy, Animator _animator) : base(_enemy, _animator){}


    public override void OnEnter()
    {
        Debug.Log("Entered State: Follow");
        _animator.Play("Walk");
        base.OnEnter();
    }
    public override void Update()
    {
        _enemy.Movement.MoveTo();
        base.Update();
    }
}