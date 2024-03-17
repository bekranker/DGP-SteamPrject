using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState : IState
{
    protected readonly Enemy _enemy;
    protected readonly Animator _animator;


    public BaseState(Enemy enemy, Animator animator)
    {
        _enemy = enemy;
        _animator = enemy.GetComponent<Animator>();
    }

    public virtual void FixedUpdate()
    {
    }

    public virtual void OnEnter()
    {
    }

    public virtual void OnExit()
    {
    }

    public virtual void Update()
    {
    }
}
