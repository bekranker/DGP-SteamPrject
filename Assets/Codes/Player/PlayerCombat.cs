using System.Collections;
using System.Collections.Generic;
using Sirenix.Utilities;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{

    [Header("Raycast Properties")]
    [SerializeField] private Vector2 _length;
    [SerializeField] private LayerMask _hitMaskes;


    [Header("Components")]
    [SerializeField] private InputHandler _inputHandler;
    [SerializeField] private PlayerMoovee _move;

    void Update()
    {
        Attack();
    }

    private void Attack()
    {
        if (_inputHandler.AttackInput)
        {
            RaycastForAttack();
        }
    }
    private void RaycastForAttack()
    {
        Collider2D[] hit2D = Physics2D.OverlapBoxAll(transform.position + (Vector3.right * _move.MoveDirection), _length, 0, _hitMaskes);

        hit2D?.ForEach((hits) => 
        {
            if (hits.TryGetComponent(out IDamage damageable))
            {
                //movement combat bitene kadar durmali
                damageable.OnHit();
            }
        });
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + (Vector3.right * _move.MoveDirection), _length); 
    }

}
