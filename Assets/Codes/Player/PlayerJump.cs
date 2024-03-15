using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{

    [Space(10)]
    [Header("Components")]
    [Space(10)]

    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private InputHandler _inputHandler;

    [Space(10)]
    [Header("Properties")]
    [Space(10)]
    
    [SerializeField] private float _jumpHeight;
    [SerializeField] private float _fallGravity;
    [SerializeField] private float _normalGravity;
    [SerializeField] private float _jumpforce;


    void Update()
    {
        Jump();
    }

    void Jump()
    {
        if (_inputHandler.JumpInput)
        {
            _rb.gravityScale = _normalGravity;
            var force = Mathf.Sqrt(_jumpHeight * -2 * _rb.gravityScale * Physics2D.gravity.y) * _rb.mass;
            _rb.AddForce(Vector2.up * force, ForceMode2D.Impulse);
        }
        if (_rb.velocity.y < 0)
        {
            _rb.gravityScale = _fallGravity;
        }
        else
        {
            _rb.gravityScale = _normalGravity;
        }
    }
}