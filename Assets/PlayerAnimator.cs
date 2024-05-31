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
    private bool _canPlaySlidingAnim, _canPlayRunAnim, _canPlayIdleAnim, _canPlayDashAnim;
    #endregion

    private void Start()
    {
        _canPlaySlidingAnim = true;
        _canPlayRunAnim = true;
        _canPlayDashAnim = true;
        _canPlayIdleAnim = true;
        _mov = GetComponent<PlayerMovement>();
        _anim = GetComponent<Animator>();
    }

    private void Update()
    {
        #region Dashing Animation
        
        if (_mov.IsDashing)
        {
            if (_canPlayDashAnim)
            {
                _anim.SetTrigger("Dash");
                _canPlayDashAnim = false;
            }
        }
        else
        {
            _canPlayDashAnim = true;
        }

        #endregion
        #region Running Animation
        
        if (_inputHandler.MoveInput.x != 0 && !_mov.IsSliding && !_mov.IsDashing)
        {
            _anim.SetFloat("Run", Mathf.Abs(_inputHandler.MoveInput.x));
        }
        else
        {
            _anim.SetFloat("Run", -1);
        }
        
        #endregion
        #region Sliding Animation

        if (_mov.IsSliding && _inputHandler.MoveInput.x != 0)
        {
            if (_canPlaySlidingAnim)
            {
                _anim.SetBool("Slide", true);
                _canPlaySlidingAnim = false;
            }
        }
        else
        {
            _anim.SetBool("Slide", false);
            _canPlaySlidingAnim = true;
        }

        #endregion
        JumpStates();
    }
    #region Jump Animation
    private void JumpStates()
    {
        if (_grounded.IsGrounded() || _mov.IsSliding)
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
            if (!_mov.IsDashing && !_mov.IsSliding && _mov.CanMove)
            {
                _anim.SetFloat("VelocityY", _mov.RB.velocity.y);
                _canPlayIdleAnim = true;
            }
            
        }
    }
    #endregion
}
