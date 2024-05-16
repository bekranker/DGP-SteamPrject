using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private float _pushForce;
    [SerializeField] private float _playerPushForce;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    public int Direction;
    private bool _canGo;


    void Awake()
    {
        _canGo = true;
    }

    void Update()
    {
        if (_canGo)
        {
            _rb.velocity = Vector2.right * Direction * _pushForce;
        }
        else
            _rb.velocity = Vector2.zero;

        if (Direction == 1)
            _spriteRenderer.flipX = false;
        else
            _spriteRenderer.flipX = true;
 
    }
    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.TryGetComponent(out IDamage damageable) && _canGo)
        {
            damageable.OnHit(1, Direction, _playerPushForce);
            transform.SetParent(collider2D.transform);
            _rb.velocity = Vector2.zero;
            _rb.gravityScale = 0;
            _rb.isKinematic = true;
            _canGo = false;
        }
    }
}