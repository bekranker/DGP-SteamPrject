using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrollState : BaseState
{
    public PatrollState(Enemy _enemy, Animator _animator) : base(_enemy, _animator){}

    public override void OnEnter()
    {
        Debug.Log("Entered State: Patroll");
        base.OnEnter();
    }
}