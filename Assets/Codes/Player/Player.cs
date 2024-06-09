using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.SceneManagement;
using ZilyanusLib.Audio;

public class Player : MonoBehaviour, IDamage
{
    [SerializeField] private FollowCamera _followCamera;
    [SerializeField] private Animator _animator;
    [SerializeField] private PlayerContex _playerSTM;
    [SerializeField] private PlayerAnimator _playerAnimator;
    [SerializeField] private DeadScreenHandler _deadScreenHandler;
    [SerializeField] private List<Transform> _teleports = new List<Transform>();
    [SerializeField] private List<AudioClip> _footSteps = new List<AudioClip>();
    [SerializeField] private bool _isScene2;
    [SerializeField] private Rigidbody2D _rigidBody2D;
    [SerializeField] private float _footStepSoundVolume;
    public float MaxHealth;
    public float Health;
    private bool _dead;
    public static event Action<float,float> OnTakeHit;

    public void OnDead()
    {
        _followCamera.ScreenShake();
        _playerSTM.enabled = false;
        _playerAnimator.enabled = false;
        _animator.Play("Dead");
        _animator.SetFloat("VelocityY", 0);
        _animator.SetFloat("Run", 0);
        _animator.SetBool("Slide", false);
        StartCoroutine(DeadSleep());
    }
    private IEnumerator DeadSleep()
    {
        yield return new WaitForSeconds(.2f);
        _rigidBody2D.velocity = Vector2.zero;
        AudioClass.PlayAudio("deadFX", 1);
        yield return new WaitForSeconds(3);
        _dead = true;
        _deadScreenHandler.DeadScreen();
    } 
    [Button]
    public async void OnHit(float damage, float direction, float pushForce)
    {
        Health -= damage;
        OnTakeHit.Invoke(Health, MaxHealth);
        if (Health <= 0)
        {
            OnDead();
            return;
        }
        _followCamera.TakeDamage();
        await TimeFreeze();
        transform.position += Vector3.right * direction * pushForce;
        _playerSTM.TransitionTo(_playerSTM.C_PlayerDamageState);
    }
    private async Task TimeFreeze()
    {
        Time.timeScale = 0;
        await Task.Delay(TimeSpan.FromSeconds(.1f));
        Time.timeScale = 1;
    }
    void Update()
    {
        if (_dead)
        {
            if (Input.anyKeyDown)
            {
                SceneManager.LoadScene("GamePlay");
                _dead = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            transform.position = _teleports[0].position;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            transform.position = _teleports[1].position;
        }
        else if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            transform.position = _teleports[2].position;
        }
    }
    public void FootStepFunction()
    {
        if(!_isScene2) return;
        int index = UnityEngine.Random.Range(0, _footSteps.Count);
        AudioClass.PlayAudio(_footSteps[index], _footStepSoundVolume);
    }
}