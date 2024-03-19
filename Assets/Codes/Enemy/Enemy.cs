using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamage
{
    public EnemyType EnemyT;
    public EnemyMovement Movement;
    public EnemyCombat Combat;
    public EnemyPatroll Patroll;
    public Grounded Ground;

    public float CurrentHealt {get; set;}
    public static event Action OnHitAction, OnDeadAction;


    void Awake()
    {
        CurrentHealt = EnemyT.Health;
    }


    public void OnDead()
    {
        OnDeadAction?.Invoke();
        //destroy or do something else
    }

    //this function is calling every time when this enemy take damage
    public void OnHit()
    {
        if (CurrentHealt - 1 <= 0)
        {
            OnDead();
        }
        else
        {
            OnHitAction?.Invoke();
        }
    }
}