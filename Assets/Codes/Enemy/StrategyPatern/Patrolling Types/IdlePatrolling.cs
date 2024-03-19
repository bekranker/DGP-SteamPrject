using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdlePatrolling : Patrolling
{
    public void PatrollEnter(Animator animator)
    {
        animator.Play("Idle");
    }

    public void Patrolling(Enemy enemy)
    {
    }
}