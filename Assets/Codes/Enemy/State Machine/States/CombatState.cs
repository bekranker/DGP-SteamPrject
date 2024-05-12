using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatState : BaseState
{
    public CombatState(Enemy _enemy, Animator _animator) : base (_enemy, _animator){}
    private float _counter;

    public override void OnEnter()
    {
        _counter = _enemy.EnemyT.CombatDelay;
        Debug.Log("Entered State: Combat");
        _enemy.Combat.CombatEnterFunction(_enemy, _animator);
        base.OnEnter();
    }
    public override void Update()
    {
        CounterConfig();
        base.Update();
    }
    private void CounterConfig()
    {
        if (_counter > 0)
        {
            _counter -= Time.deltaTime;
        }
        else
        {
            _counter = _enemy.EnemyT.CombatDelay;
            _animator.Play("Combat");
        }
    }
}