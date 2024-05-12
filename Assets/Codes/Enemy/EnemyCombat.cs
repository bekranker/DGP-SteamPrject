using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    [Header("Combat Props")]
    [SerializeField] private ICombat _enemyCombatType;



    [Header("Components")]
    [SerializeField] private Enemy _enemy;
    [SerializeField] private EnemyMovement _enemyMovement;
    [SerializeField] private EnemyPatroll _enemyPatroll;
    [SerializeField] private Animator _animator;

    private Transform _player;
    public bool B_AnimEnd;

    void Start()
    {
        _player = GameObject.FindWithTag("Player").transform;
    }
    
    public void CombatFunction()
    {
        _enemyCombatType.CombatFunction(_enemy, _animator);
    }

    public void CombatEnterFunction(Enemy enemy, Animator _animator)
    {
        _enemyCombatType.CombatEnterFunction(enemy, _animator);
    }
    
    public void OnStartAnim()
    {
        B_AnimEnd = false;
        _enemyMovement.MoveDirection = Mathf.Sign(_player.position.x - transform.position.x);
    }
    public void AnimEnd()
    {
        B_AnimEnd = true;
    }
    public bool CanCombat()
    { 
        //eger ki dusman kombata girmeden once alerted olmasi kesin gerekiyorsa buraya _enemy.Alerted bool kontrolu konmali 
        return !_enemyMovement.CanFollow();
    }

}