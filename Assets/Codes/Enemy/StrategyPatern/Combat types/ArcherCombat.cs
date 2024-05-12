using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherCombat : ICombat
{
    [Header("Combat Props")]
    [SerializeField] private GameObject _arrowPrefab;


    public override void CombatEnterFunction(Enemy enemy, Animator animator)
    {
        animator.Play("Idle");
    }

    public override void CombatFunction(Enemy enemy, Animator animator) 
    {
        Instantiate(_arrowPrefab, enemy.transform.position, Quaternion.identity);
    }
}