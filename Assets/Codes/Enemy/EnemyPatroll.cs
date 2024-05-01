using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatroll : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;
    [SerializeField] private Patrolling _patrollingType;
    public Patrolling SpawnedPatrolling;



    void Start()
    {
        SpawnedPatrolling = Instantiate(_patrollingType, _enemy.transform);
    }
    public void Patroll()
    {
        SpawnedPatrolling.PatrollingFunction(_enemy);
    }
    public void PatrollEnter()
    {
        SpawnedPatrolling.PatrollEnterFunction(_enemy, _enemy.GetComponent<Animator>());
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