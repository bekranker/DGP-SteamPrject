using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContex : MonoBehaviour
{
    public PlayerCombat C_PlayerCombat;
    public PlayerMovement C_PlayerMovement;
    public Animator CMP_Animator;
    public InputHandler C_InputHandler;
    public Rigidbody2D CMP_Rb;
    private PlayerState _playerState;
    

    public PlayerMovementState C_PlayerMovementState {get; set;}
    public PlayerIdleState C_PlayerIdleState {get; set;}
    public PlayerDamageState C_PlayerDamageState {get; set;}
    public PlayerCombatState C_PlayerCombatState {get; set;}
    public PlayerCustomAnimationState C_PlayerCustomAnimationState {get; set;}


    public PlayerContex(PlayerState state)
    {
        this.TransitionTo(state);
    }
    void Start()
    {

        C_PlayerMovementState = new PlayerMovementState();
        C_PlayerCombatState = new PlayerCombatState();
        C_PlayerDamageState = new PlayerDamageState();
        C_PlayerIdleState = new PlayerIdleState();
        C_PlayerCustomAnimationState = new PlayerCustomAnimationState();
        TransitionTo(C_PlayerIdleState);
    }
    public void TransitionTo(PlayerState state)
    {
        _playerState?.ExitState(state);
        this._playerState = state;
        this._playerState.SetContext(this);
        _playerState?.EnterState(state);
    }
    void Update()
    {
        this._playerState.OnUpdate(_playerState);
    }
    void FixedUpdate()
    {
        this._playerState.OnFixedUpdate(_playerState);
    }
}