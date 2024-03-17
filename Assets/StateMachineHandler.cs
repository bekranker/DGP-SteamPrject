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



    private StateMachine _stateMachine;


    void Awake()
    {
        _stateMachine = new StateMachine();
        var patrollState = new PatrollState(_enemy, _animator);
        var followState = new FollowState(_enemy, _animator);
        var combatState = new CombatState(_enemy, _animator);
        var locomotionState = new LocomotionState(_enemy, _animator);

        
        Any(followState, new FuncPredicate(() => _enemyMovement.CanFollow()));
        Any(combatState, new FuncPredicate(() => _enemyCombat.CanCombat()));
        Any(patrollState, new FuncPredicate(() => _enemyPatrol.CanPatroll()));
        Any(locomotionState, new FuncPredicate(() => 
        {
            return false; 
        }));
        
        
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