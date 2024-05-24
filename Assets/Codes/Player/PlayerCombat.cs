using System.Collections;
using System.Collections.Generic;
using Sirenix.Utilities;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{

    [Header("Animation Properties")]
    [SerializeField] public List<AnimationClip> Animations = new List<AnimationClip>();
    [SerializeField] public AnimationClip IdleClip;


    [Header("Raycast Properties")]
    [SerializeField] public Vector2 Length;
    [SerializeField] public LayerMask HitMaskes;
    [SerializeField] public float PushForce;


    [Header("Components")]
    [SerializeField] public InputHandler C_InputHandler;
    [SerializeField] public PlayerMovement Move;
    [SerializeField] public Rigidbody2D Rb;
    [SerializeField] public Animator CMP_Animator;

    public bool CanAttack;
    public bool EndAnim;

    void Awake()
    {
        CanAttack = true;
        EndAnim = true;
    }
    
    public void AnimationEnd()
    {
        EndAnim = true;
        Move.CanMove = true;
        CanAttack = true;
        CMP_Animator.Play(IdleClip.name);
        Rb.gravityScale = 1;
    }
    public void AnimationStart()
    {
        Move.CanMove = false;
    }
    
}