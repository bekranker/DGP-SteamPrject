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
    [SerializeField] private Rigidbody2D _rb;
    public float CurrentHealt {get; set;}
    public static event Action OnHitAction, OnDeadAction;
    public bool Alerted{get; set;}
    public bool Locomotion{get; set;}
    void Awake()
    {
        Alerted = false;
        Locomotion = false;
        CurrentHealt = EnemyT.Health;
    }


    public void OnDead()
    {
        print("Dead");
        OnDeadAction?.Invoke();
        //destroy or do something else
    }

    //this function is calling every time when this enemy take damage
    public void OnHit(float damage, float direction, float pushForce)
    {
        print("hit");
        if (CurrentHealt - damage <= 0)
        {
            OnDead();
        }
        else
        {
            transform.position += Vector3.right * direction * pushForce;
            // fiziklisi burada
            // _rb.AddForce(direction * Vector2.right * 100 *  pushForce);
            CurrentHealt -= damage;
            OnHitAction?.Invoke();
        }
    }
}