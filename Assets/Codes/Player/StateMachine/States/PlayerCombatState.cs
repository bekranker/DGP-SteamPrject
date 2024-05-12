using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatState : PlayerState
{
    public override void EnterState(PlayerState playerState)
    {
    }

    public override void ExitState(PlayerState playerState)
    {
    }

    public override void OnFixedUpdate(PlayerState playerState)
    {
    }

    public override void OnUpdate(PlayerState playerState)
    {
        playerState.C_PlayerContex.C_PlayerCombat.Attack();
        if (playerState.C_PlayerContex.C_PlayerCombat.EndAnim && playerState.C_PlayerContex.C_PlayerCombat.Counter <= 0)
        {
            if (playerState.C_PlayerContex.C_InputHandler.MoveInput != Vector2.zero)
            {
                playerState.C_PlayerContex.TransitionTo(playerState.C_PlayerContex.C_PlayerMovementState);
            }
        }
    }
}