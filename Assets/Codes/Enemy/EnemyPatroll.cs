using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatroll : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;

    public void Patroll()
    {
        _enemy.EnemyT.PatrollType.PatrollingFunction(_enemy);
    }
    public void PatrollEnter()
    {
        _enemy.EnemyT.PatrollType.PatrollEnterFunction(_enemy, _enemy.GetComponent<Animator>());
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
