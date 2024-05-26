using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerState
{
    public override void EnterState(PlayerState playerState)
    {
        playerState.C_PlayerContex.CMP_Rb.isKinematic = false;
    }

    public override void ExitState(PlayerState playerState)
    {
    }

    public override void OnFixedUpdate(PlayerState playerState)
    {
        playerState.C_PlayerContex.C_PlayerMovement.AllFixedUpdateFunctionsForMovement();
    }

    public override void OnUpdate(PlayerState playerState)
    {
        playerState.C_PlayerContex.C_PlayerMovement.AllUpdateFunctionsForMovement();
        if (playerState.C_PlayerContex.C_InputHandler.AttackInput)
        {
            playerState.C_PlayerContex.TransitionTo(playerState.C_PlayerContex.C_PlayerCombatStateOne);
        }
        if (playerState.C_PlayerContex.C_InputHandler.MoveInput != Vector2.zero)
        {
            playerState.C_PlayerContex.TransitionTo(playerState.C_PlayerContex.C_PlayerMovementState);
        }
        if (playerState.C_PlayerContex.C_InputHandler.JumpInput)
        {
            playerState.C_PlayerContex.TransitionTo(playerState.C_PlayerContex.C_PlayerMovementState);
        }
    }
}