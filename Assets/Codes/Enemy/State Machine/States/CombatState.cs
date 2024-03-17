using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatState : BaseState
{
    public CombatState(Enemy _enemy, Animator _animator) : base (_enemy, _animator){}

    public override void OnEnter()
    {
        Debug.Log("Entered State: Combat");
        base.OnEnter();
    }
}