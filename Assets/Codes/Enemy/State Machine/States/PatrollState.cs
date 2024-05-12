using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrollState : BaseState
{
    public PatrollState(Enemy _enemy, Animator _animator) : base(_enemy, _animator){}


    public override void Update()
    {
        _enemy.Patroll.Patroll();
        base.Update();
    }
    public override void OnEnter()
    {
        _animator.Play("Walk");
        _enemy.Patroll.PatrollEnter();
        Debug.Log("Entered State: Patroll");
        base.OnEnter();
    }
}