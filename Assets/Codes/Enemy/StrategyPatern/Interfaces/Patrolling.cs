using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Patrolling : MonoBehaviour
{
    public virtual void PatrollingFunction(Enemy enemy){}
    public virtual void PatrollEnterFunction(Enemy enemy, Animator animator){}
}