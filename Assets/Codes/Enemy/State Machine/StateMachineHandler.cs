using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachineHandler : MonoBehaviour
{
    [Space(10)]
    [Header("Components")]
    [Space(10)]
    [SerializeField] private Animator _animator;
    [SerializeField] private Enemy _enemy;
    [SerializeField] private EnemyMovement _enemyMovement;
    [SerializeField] private EnemyCombat _enemyCombat;
    [SerializeField] private EnemyPatroll _enemyPatrol;
    [SerializeField] private bool _melee;


    private StateMachine _stateMachine;


    void Awake()
    {
        
        _stateMachine = new StateMachine();
        var alertState = new AlertState(_enemy, _animator);
        var patrollState = new PatrollState(_enemy, _animator);
        var followState = new FollowState(_enemy, _animator);
        var combatState = new CombatState(_enemy, _animator);
        var locomotionState = new LocomotionState(_enemy, _animator);


        //melee
        if (_melee)
        {
            At(locomotionState, patrollState, new FuncPredicate(() => _enemyPatrol.CanPatroll() && !_enemy.Alerted && _enemy.Locomotion && _enemyCombat.B_AnimEnd));
            Any(alertState, new FuncPredicate(() => _enemyMovement.CanFollow() && !_enemy.Alerted));
            At(alertState, followState, new FuncPredicate(() => _enemyMovement.CanFollow() && _enemy.Alerted && _enemyCombat.B_AnimEnd));
            At(followState, combatState, new FuncPredicate(() => _enemyCombat.CanCombat()  && _enemyMovement.PlayerCloseFromUs()));
            At(alertState, combatState, new FuncPredicate(() => _enemyCombat.CanCombat() && _enemy.Alerted));
            At(combatState, followState, new FuncPredicate(() => _enemy.Alerted && !_enemyCombat.CanCombat() && _enemyCombat.B_AnimEnd));

            Any(locomotionState, new FuncPredicate(() => (!_enemyMovement.CanFollow() && !_enemyMovement.PlayerCloseFromUs()) && _enemy.Alerted && _enemyCombat.B_AnimEnd));
        
        }
        //archer
        else
        {
            At(locomotionState, patrollState, new FuncPredicate(() => !_enemyMovement.CanFollowArcher() && !_enemy.Alerted && _enemy.Locomotion && _enemyCombat.B_AnimEnd));
            Any(alertState, new FuncPredicate(() => _enemyMovement.CanFollowArcher() && !_enemy.Alerted));
            At(alertState, combatState, new FuncPredicate(() => _enemyMovement.CanFollowArcher() && _enemy.Alerted));
            Any(locomotionState, new FuncPredicate(() => (!_enemyMovement.CanFollowArcher() && !_enemyMovement.PlayerCloseFromUs()) && _enemy.Alerted && _enemyCombat.B_AnimEnd));
        }
        
        
        _stateMachine.SetState(locomotionState);
    }

    void At(IState From, IState To, IPredicie condition) => _stateMachine.AddTransition(From, To, condition);
    void Any(IState to, IPredicie condition) => _stateMachine.AddAnyTransition(to, condition);


    void Update()
    {
        _stateMachine.Update();
    }
    void FixedUpdate()
    {
        _stateMachine.FixedUpdate();
    }
}