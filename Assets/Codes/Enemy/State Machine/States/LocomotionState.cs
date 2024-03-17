using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocomotionState : BaseState
{
    public LocomotionState(Enemy _enemy, Animator _animator) : base(_enemy, _animator){}

    public override void OnEnter()
    {
        Debug.Log("Entered State: Locomotion");
        base.OnEnter();
    }

}