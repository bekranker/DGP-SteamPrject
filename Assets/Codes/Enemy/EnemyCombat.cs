using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;
    [SerializeField] private EnemyMovement _enemyMovement;
    [SerializeField] private EnemyPatroll _enemyPatroll;

    private Transform _player;

    void Start()
    {
        _player = GameObject.FindWithTag("Player").transform;
    }

    public void AttackToPlayer()
    {
        Debug.Log("AttackToPlayer");
    }
    public bool CanCombat()
    { 
        //eger ki dusman kombata girmeden once alerted olmasi kesin gerekiyorsa buraya _enemy.Alerted bool kontrolu konmali 
        return !_enemyMovement.CanFollow();
    }
}