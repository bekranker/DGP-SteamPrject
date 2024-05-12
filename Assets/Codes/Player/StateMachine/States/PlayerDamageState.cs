using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageState : PlayerState
{
    public override void EnterState(PlayerState playerState)
    {
        playerState.C_PlayerContex.CMP_Animator.Play("Hit");
    }

    public override void ExitState(PlayerState playerState)
    {
    }

    public override void OnFixedUpdate(PlayerState playerState)
    {
    }

    public override void OnUpdate(PlayerState playerState)
    {
    }
}