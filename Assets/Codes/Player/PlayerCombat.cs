using System.Collections;
using System.Collections.Generic;
using Sirenix.Utilities;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{

    [Header("Raycast Properties")]
    [SerializeField] private Vector2 _length;
    [SerializeField] private LayerMask _hitMaskes;
    [SerializeField] private float _pushForce;


    [Header("Components")]
    [SerializeField] private InputHandler _inputHandler;
    [SerializeField] private PlayerMoovee _move;
    [SerializeField] private Rigidbody2D _rb;

    void Update()
    {
        Attack();
    }

    private void Attack()
    {
        if (_inputHandler.AttackInput)
        {
            _move.CanMove = false;
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

                //fiziklisi burada
                // _rb.AddForce(Mathf.Sign((hits.transform.position - transform.position).x) * 100 * Vector2.right * _pushForce);
                
                // pozisyon degisikligi yapilan versiyonu burada
                transform.position += Vector3.right * _move.MoveDirection * _pushForce;
                damageable.OnHit(1, _move.MoveDirection, _pushForce);
            }
        });
        // Animasyon sonunda truelanicak
        _move.CanMove = true;
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + (Vector3.right * _move.MoveDirection), _length); 
    }

}
