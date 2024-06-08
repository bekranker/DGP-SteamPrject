using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationHandler : MonoBehaviour
{
    [SerializeField] private InputHandler _inputHandler;
    [SerializeField] private Animator _animator;
    
    void Update()
    {
        if (_inputHandler.MoveInput.x != 0)
        {
            _animator.Play("Walk");
        }
        else
        {
            _animator.Play("Idle");
        }
    }
}
