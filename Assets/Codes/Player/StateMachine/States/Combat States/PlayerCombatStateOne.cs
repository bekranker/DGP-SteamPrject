using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.Utilities;


public class PlayerCombatStateOne : PlayerState
{
    private float _attackTimer = 1f;
    private float _counter = 1f;
    private bool _canContunie;



    public override void EnterState(PlayerState playerState)
    {
        _counter = _attackTimer;
        _canContunie = false;
        playerState.C_PlayerContex.CMP_Rb.velocity = new Vector2(0, 0);
        playerState.C_PlayerContex.C_PlayerCombat.EndAnim = false;
        playerState.C_PlayerContex.C_PlayerMovement.CanMove = false;
        playerState.C_PlayerContex.CMP_Rb.isKinematic = true;
        playerState.C_PlayerContex.CMP_Animator.Play("Combat1");
        playerState.C_PlayerContex.CMP_Animator.SetFloat("VelocityY", 0);

    }

    public override void ExitState(PlayerState playerState)
    {
        _counter = _attackTimer;
        _canContunie = false;
    }

    public override void OnFixedUpdate(PlayerState playerState)
    {
    }
    
    
    public override void OnUpdate(PlayerState playerState)
    {
        if (_counter <= 0)
        {
            _counter = 0;
        }
        else
        {
            _counter -= Time.deltaTime;
        }
        if (playerState.C_PlayerContex.C_InputHandler.AttackInput)
        {
            if (_counter > 0)
            {
                _canContunie = true;
            }
        }
        if (playerState.C_PlayerContex.C_PlayerCombat.EndAnim)
        {
            if (_canContunie)
            {
                playerState.C_PlayerContex.TransitionTo(playerState.C_PlayerContex.C_PlayerCombatStateTwo);
            }
            else
            {
                if (playerState.C_PlayerContex.C_InputHandler.AttackInput)
                {
                    playerState.C_PlayerContex.TransitionTo(playerState.C_PlayerContex.C_PlayerCombatStateOne);
                }
                if (playerState.C_PlayerContex.C_InputHandler.MoveInput != Vector2.zero && !_canContunie)
                {
                    playerState.C_PlayerContex.TransitionTo(playerState.C_PlayerContex.C_PlayerMovementState);
                }
                else
                {
                    playerState.C_PlayerContex.TransitionTo(playerState.C_PlayerContex.C_PlayerIdleState);
                }
            }
            
        }
    }
}