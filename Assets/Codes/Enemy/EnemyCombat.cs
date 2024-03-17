using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    private Enemy _enemy;
    private EnemyMovement _enemyMovement;
    private EnemyPatroll _enemyPatroll;

    void Awake()
    {
        _enemy = GetComponent<Enemy>();
    }
    public void AttackToPlayer()
    {
        Debug.Log("AttackToPlayer");
    }
    public bool CanCombat()
    {
        return false || true;
    }
}