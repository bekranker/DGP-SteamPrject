using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ICombat : MonoBehaviour
{
    public virtual void CombatFunction(Enemy enemy, Animator animator){}
    public virtual void CombatEnterFunction(Enemy enemy, Animator animator){}
}