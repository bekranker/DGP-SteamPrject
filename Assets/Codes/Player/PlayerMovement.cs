using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class PlayerMovement : MonoBehaviour
{
    [Space(10)]
    [Header("Components")]
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private InputHandler _inputHandler;

    [Space(10)]
    [Header("Properties")]
    [SerializeField, Range(0, 100)] private float _acceleration;
    [SerializeField, Range(0, 100)] private float _decceleration;
    [SerializeField, Range(0.01f, 1)] private float _velocityPower;
    [SerializeField, Range(0.1f, 10)] private float _speed;



    void FixedUpdate()
    {
        Run();
    }

    void Run()
    {
        var targetSpeed = _inputHandler.MoveInput.x * _speed;
        float speedDif = targetSpeed - _rb.velocity.x;
        float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? _acceleration : _decceleration;
        float movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, _velocityPower) * Mathf.Sign(speedDif);

        _rb.AddForce(Vector2.right * movement);


        
    }
}
