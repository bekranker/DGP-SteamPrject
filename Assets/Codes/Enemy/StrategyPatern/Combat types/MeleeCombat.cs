using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeCombat : ICombat
{
    [Header("Combat Props")]
    [SerializeField, Range(0.1f, 3f)] private float _pushForce;

    [Header("Ray Props")]
    [SerializeField] private LayerMask _playerLayer;
    [SerializeField, Range(0.1f, 3f)] private float _rayLength;
    public override void CombatEnterFunction(Enemy enemy, Animator animator)
    {
        animator.Play("Idle");
    }

    public override void CombatFunction(Enemy enemy, Animator animator)
    {
        RaycastHit2D hit2D = Physics2D.Raycast(enemy.transform.position, Vector2.right * enemy.Movement.MoveDirection, _rayLength, _playerLayer);
        if (hit2D.collider != null)
        {
            if (hit2D.collider.TryGetComponent(out IDamage damageable))
            {
                damageable.OnHit(1, enemy.Movement.MoveDirection, _pushForce);
            }
        }
    }
}