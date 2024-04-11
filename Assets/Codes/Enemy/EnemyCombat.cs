using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    [Header("Combat Props")]
    [SerializeField, Range(0.1f, 3f)] private float _pushForce;



    [Header("Ray Props")]
    [SerializeField] private LayerMask _playerLayer;
    [SerializeField, Range(0.1f, 3f)] private float _rayLength;

    [Header("Components")]
    [SerializeField] private Enemy _enemy;
    [SerializeField] private EnemyMovement _enemyMovement;
    [SerializeField] private EnemyPatroll _enemyPatroll;
    [SerializeField] private Animator _animator;

    private Transform _player;

    void Start()
    {
        _player = GameObject.FindWithTag("Player").transform;
    }
    
    
    public void RaycastingToPlayer()
    {
        var direction = Mathf.Sign(_player.position.x - transform.position.x);
        RaycastHit2D hit2D = Physics2D.Raycast(transform.position, Vector2.right * direction, _rayLength, _playerLayer);
        if (hit2D.collider != null)
        {
            if (hit2D.collider.TryGetComponent(out IDamage damageable))
            {
                damageable.OnHit(1, direction, _pushForce);
            }
        }
    }
    private void OnDrawGizmos()
    {
        if (_player == null) return;
        Gizmos.DrawRay(transform.position, Vector2.right * Mathf.Sign(_player.position.x - transform.position.x) * _rayLength);
    }
    public bool CanCombat()
    { 
        //eger ki dusman kombata girmeden once alerted olmasi kesin gerekiyorsa buraya _enemy.Alerted bool kontrolu konmali 
        return !_enemyMovement.CanFollow();
    }
}