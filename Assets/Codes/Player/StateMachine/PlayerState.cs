using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public abstract class PlayerState
{
    public PlayerContex C_PlayerContex;


    public void SetContext(PlayerContex context)
    {
        this.C_PlayerContex = context;
    }

    public abstract void EnterState(PlayerState playerState);
    public abstract void ExitState(PlayerState playerState);
    public abstract void OnUpdate(PlayerState playerState);
    public abstract void OnFixedUpdate(PlayerState playerState);
}