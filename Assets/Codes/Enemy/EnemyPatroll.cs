using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatroll : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;
    public Patrolling PattrolingTye;

    public void Patroll()
    {
        PattrolingTye.PatrollingFunction(_enemy);
    }
    public void PatrollEnter()
    {
        PattrolingTye.PatrollEnterFunction(_enemy, _enemy.GetComponent<Animator>());
    }
    public bool CanPatroll()
    {
        return (!_enemy.Movement.CanFollow());
    }
    public bool SameDirection()
    {
        return true;
    }
}