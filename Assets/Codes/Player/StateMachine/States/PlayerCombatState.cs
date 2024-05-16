using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.Utilities;


public class PlayerCombatState : PlayerState
{
    private float _attackTimer = 1.5f;
    private float _counter;
    private int _animCounter;

    public override void EnterState(PlayerState playerState)
    {
        Attack(playerState);
        _counter = _attackTimer;
    }

    public override void ExitState(PlayerState playerState)
    {
        _animCounter = 0;
        playerState.C_PlayerContex.C_PlayerCombat.CMP_Animator.Play("Idle");
    }

    public override void OnFixedUpdate(PlayerState playerState)
    {
    }
    public void Attack(PlayerState playerState)
    {
        if (!playerState.C_PlayerContex.C_PlayerCombat.CanAttack) return;
        
        if (playerState.C_PlayerContex.C_PlayerCombat.C_InputHandler.AttackInput)
        {
            playerState.C_PlayerContex.C_PlayerCombat.Move.CanMove = false;
            playerState.C_PlayerContex.C_PlayerCombat.CanAttack = false;
            RaycastForAttack(playerState);
            SetAnimation(playerState);
        }
    }
    public void RaycastForAttack(PlayerState playerState)
    {
        Collider2D[] hit2D = Physics2D.OverlapBoxAll(playerState.C_PlayerContex.transform.position + (Vector3.right * playerState.C_PlayerContex.C_PlayerCombat.Move._moveInput.x), playerState.C_PlayerContex.C_PlayerCombat.Length, 0, playerState.C_PlayerContex.C_PlayerCombat.HitMaskes);

        hit2D?.ForEach((hits) => 
        {
            if (hits.TryGetComponent(out IDamage damageable))
            {
                //movement combat bitene kadar durmali

                //fiziklisi burada
                // _rb.AddForce(Mathf.Sign((hits.transform.position - transform.position).x) * 100 * Vector2.right * _pushForce);
                
                // pozisyon degisikligi yapilan versiyonu burada
                playerState.C_PlayerContex.transform.position += Vector3.right * playerState.C_PlayerContex.C_PlayerCombat.Move._moveInput.x * playerState.C_PlayerContex.C_PlayerCombat.PushForce;
                float direction =  Mathf.Sign((hits.transform.position - playerState.C_PlayerContex.transform.position).x);
                damageable.OnHit(1, direction, playerState.C_PlayerContex.C_PlayerCombat.PushForce);
            }
        });
        if (hit2D.Length != 0)
        {
            playerState.C_PlayerContex.C_PlayerCombat.Rb.velocity = Vector2.zero;
            playerState.C_PlayerContex.C_PlayerCombat.Rb.gravityScale = 0;
        }
        // Animasyon sonunda truelanicak
        
    }
    
    void SetAnimation(PlayerState playerState)
    {
        playerState.C_PlayerContex.C_PlayerCombat.EndAnim = false;
        playerState.C_PlayerContex.C_PlayerCombat.CMP_Animator.Play(playerState.C_PlayerContex.C_PlayerCombat.Animations[_animCounter].name);
        
        
        if (_animCounter + 1 >= playerState.C_PlayerContex.C_PlayerCombat.Animations.Count)
        {
            _animCounter = 0;
        }
        else
            _animCounter += 1;
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
            if (_counter <= 0)
            {
                _animCounter = 0;
                _counter = _attackTimer;
            }
            Attack(playerState);
        }
        //if the combat animation is end and the combat counter timer is 0, then we can move
        if (playerState.C_PlayerContex.C_PlayerCombat.EndAnim && _counter <= 0)
        {
            if (playerState.C_PlayerContex.C_InputHandler.MoveInput != Vector2.zero)
            {
                playerState.C_PlayerContex.TransitionTo(playerState.C_PlayerContex.C_PlayerMovementState);
            }
        }
    }
}