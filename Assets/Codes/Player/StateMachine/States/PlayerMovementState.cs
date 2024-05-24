using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementState : PlayerState
{
    public override void EnterState(PlayerState playerState)
    {
        playerState.C_PlayerContex.C_PlayerMovement.AllStartFunctionsForMovement();
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

        if (playerState.C_PlayerContex.CMP_Rb.velocity == Vector2.zero)
        {
            playerState.C_PlayerContex.TransitionTo(playerState.C_PlayerContex.C_PlayerIdleState);
        }
        if (playerState.C_PlayerContex.C_InputHandler.AttackInput)
        {
            playerState.C_PlayerContex.TransitionTo(playerState.C_PlayerContex.C_PlayerCombatState);
        }
    }
}