using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamage
{
    public EnemyType EnemyT;
    public EnemyMovement Movement;
    public EnemyCombat Combat;
    public EnemyPatroll Patroll;
    public Grounded Ground;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private BloodVFXHandler _bloodVFXHandler;
    [SerializeField] private Transform _bloodVFXPosRight;
    [SerializeField] private FollowCamera _followCamera;
    [SerializeField] private StateMachineHandler _stateMachineHandler;
    [SerializeField] private Animator _animator;
    public SpriteRenderer SP;
    public float CurrentHealt {get; set;}
    public static event Action OnHitAction, OnDeadAction;
    public bool Alerted{get; set;}
    public bool Locomotion{get; set;}
    private WaitForSecondsRealtime _waitForSeconds = new(0.1f);
    private bool _canEffect;
    void Awake()
    {
        _canEffect = true;
        Alerted = false;
        Locomotion = false;
        CurrentHealt = EnemyT.Health;
    }
    void Start()
    {
        _followCamera = FindObjectOfType<FollowCamera>();
    }

    public void OnDead()
    {
        print("Dead");
        OnDeadAction?.Invoke();
        _stateMachineHandler.enabled = false;
        _animator.Play("Dead");
        _followCamera.ScreenShake();
        Destroy(gameObject, 1);
        //destroy or do something else
    }

    //this function is calling every time when this enemy take damage
    public void OnHit(float damage, float direction, float pushForce)
    {
        print("hit");
            BloodVFXHandler t_blood = Instantiate(_bloodVFXHandler, _bloodVFXPosRight.position, Quaternion.identity);
            t_blood.GetMe(direction);
            t_blood.transform.SetParent(transform);
        if (CurrentHealt - damage <= 0)
        {
            OnDead();
        }
        else
        {
            print("the direction is: "+direction);

            if (_canEffect)
            {
                StartCoroutine(takenDamage());
            }
            transform.position += Vector3.right * direction * pushForce;
            // fiziklisi burada
            // _rb.AddForce(direction * Vector2.right * 100 *  pushForce);
            CurrentHealt -= damage;
            OnHitAction?.Invoke();
        }
    }

    private IEnumerator takenDamage()
    {
        SP.color = Color.red;
        Time.timeScale = 0;
        _followCamera.ScreenShake();
        yield return _waitForSeconds;
        Time.timeScale = 1;
        _canEffect = true;
        SP.color = Color.white;
    }
}