using System.Collections;
using System.Collections.Generic;
using Sirenix.Utilities;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{

    [Header("Animation Properties")]
    [SerializeField] private List<AnimationClip> _animations = new List<AnimationClip>();
    [SerializeField] private AnimationClip _idleClip;


    [Header("Raycast Properties")]
    [SerializeField] private Vector2 _length;
    [SerializeField] private LayerMask _hitMaskes;
    [SerializeField] private float _pushForce;


    [Header("Components")]
    [SerializeField] private InputHandler _inputHandler;
    [SerializeField] private PlayerMoovee _move;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private Animator _animator;


    private int _animCounter;
    private bool _canAttack;


    void Awake()
    {
        _canAttack = true;
    }

    void Update()
    {
        Attack();
    }

    private void Attack()
    {
        if (!_canAttack) return;
        if (_inputHandler.AttackInput)
        {
            _move.CanMove = false;
            _canAttack = false;
            RaycastForAttack();
            SetAnimation();
        }
    }
    public void RaycastForAttack()
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
        if (hit2D.Length != 0)
        {
            _rb.velocity = Vector2.zero;
            _rb.gravityScale = 0;
        }
        // Animasyon sonunda truelanicak
        
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + (Vector3.right * _move.MoveDirection), _length); 
    }
    void SetAnimation()
    {
        _animator.Play(_animations[_animCounter].name);
        _animCounter = (_animCounter + 1 >= _animations.Count) ? 0 : _animCounter + 1;
    }
    public void AnimationEnd()
    {
        _move.CanMove = true;
        _canAttack = true;
        _animator.Play(_idleClip.name);
        _rb.gravityScale = 1;
    }
    public void AnimationStart()
    {
        _move.CanMove = false;
    }
}