using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadState : BaseState
{
    public DeadState(Enemy _enemy, Animator _animator) : base(_enemy, _animator){}


    public override void OnEnter()
    {
        Debug.Log("Entered State: Dead");
        _animator.Play("Dead");
        base.OnEnter();
    }
    public override void Update()
    {
        base.Update();
    }
}