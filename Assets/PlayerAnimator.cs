using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private PlayerMovement _mov;
    private Animator _anim;


    [Header("Props")]
    [SerializeField] private InputHandler _inputHandler;
    [SerializeField] private Grounded _grounded;
    public float currentVelY;


    #region Booleans
    public bool startedJumping {  private get; set; }
    public bool justLanded { private get; set; }
    private bool _canPlaySlidingAnim, _canPlayRunAnim, _canPlayIdleAnim;
    #endregion

    private void Start()
    {
        _canPlaySlidingAnim = true;
        _canPlayRunAnim = true;
        _canPlayIdleAnim = true;
        _mov = GetComponent<PlayerMovement>();
        _anim = GetComponent<Animator>();
    }

    private void Update()
    {
        #region Sliding Animation
        // if (_mov.IsSliding)
        // {
        //     if (_canPlaySlidingAnim)
        //     {
        //         //sliding animation
        //         _anim.Play("Slide");
        //         _canPlaySlidingAnim = false;
        //     }
        // }
        // else
        // {
        //     _canPlaySlidingAnim = true;
        // }

        if (_mov.IsSliding)
        {
            _anim.SetFloat("Sliding", 1);
        }
        else
        {
            _anim.SetFloat("Sliding", -1);
        }


        #endregion
        #region Running Animation
        // if (_inputHandler.MoveInput.x != 0 /* !_combat.IsAttacking */)
        // {
        //     if (_canPlayRunAnim)
        //     {
        //         //running animation
        //         _anim.Play("Run");
        //         _canPlayRunAnim = false;
        //     }
        // }
        // else
        // {
        //     _canPlayRunAnim = true;
        // }
        if (_inputHandler.MoveInput.x != 0 /* !_combat.IsAttacking */)
        {
            _anim.SetFloat("Run", Mathf.Abs(_inputHandler.MoveInput.x));
        }
        else
        {
            _anim.SetFloat("Run", -1);
        }
        
        #endregion

        JumpStates();
    }

    private void JumpStates()
    {
        if (_grounded.IsGrounded()/*!mov.isSliding()*/)
        {
            _anim.SetFloat("VelocityY", 0);
            if(_canPlayIdleAnim)
            {
                _anim.Play("Idle");
                _canPlayIdleAnim = false;
            }
        }
        else
        {
            _anim.SetFloat("VelocityY", _mov.RB.velocity.y);
            _canPlayIdleAnim = true;
        }
    }
}
