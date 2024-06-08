using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerDamageState : PlayerState
{
    public override void EnterState(PlayerState playerState)
    {
        playerState.C_PlayerContex.CMP_Animator.Play("Hit");
        DOVirtual.DelayedCall(.5f, ()=>
        {
            playerState.C_PlayerContex.TransitionTo(playerState.C_PlayerContex.C_PlayerIdleState);
        });
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