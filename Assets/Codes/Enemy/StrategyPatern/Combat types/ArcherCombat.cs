using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherCombat : ICombat
{
    [Header("Combat Props")]
    [SerializeField] private Arrow _arrowPrefab;
    private Transform _playerT;

    void Start()
    {
        _playerT = GameObject.FindWithTag("Player").transform;
    }

    public override void CombatEnterFunction(Enemy enemy, Animator animator)
    {
        animator.Play("Idle");
    }

    public override void CombatFunction(Enemy enemy, Animator animator)
    {
        Arrow spawnedArrow = Instantiate(_arrowPrefab, enemy.transform.position, Quaternion.identity);
        var direction = _playerT.position - enemy.transform.position;
        spawnedArrow.Direction = (int)direction.x;
    }
}