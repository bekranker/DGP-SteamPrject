using System.Collections;
using System.Collections.Generic;
using Sirenix.Utilities;
using UnityEngine;
using UnityEngine.TextCore.Text;

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

    [Header("Audios")]
    [SerializeField] private List<AudioClip> _combatAudios = new List<AudioClip>();

    public bool EndAnim;

    void Awake()
    {
        EndAnim = true;
    }
    
    public void AnimationEnd()
    {
        EndAnim = true;
        Move.CanMove = true;
        Rb.gravityScale = 1;
    }
    public void AnimationStart()
    {
        EndAnim = false;
        Move.CanMove = false;
    }
    public void RaycastForAttack()
    {
        Collider2D[] hit2D = Physics2D.OverlapBoxAll(transform.position + (Vector3.right * C_InputHandler.MoveInput.x), Length, 0, HitMaskes);

        hit2D?.ForEach((hits) => 
        {
            if (hits.TryGetComponent(out IDamage damageable))
            {
                //movement combat bitene kadar durmali

                //fiziklisi burada
                // _rb.AddForce(Mathf.Sign((hits.transform.position - transform.position).x) * 100 * Vector2.right * _pushForce);
                
                // pozisyon degisikligi yapilan versiyonu burada
                transform.position += Vector3.right * Move._moveInput.x * PushForce;
                float direction =  Mathf.Sign((hits.transform.position - transform.position).x);
                damageable.OnHit(1, direction, PushForce);
            }
        });
        if (hit2D.Length != 0)
        {
            Rb.velocity = Vector2.zero;
            Rb.gravityScale = 0;
        }
        // Animasyon sonunda truelanicak
        
    }
    
}