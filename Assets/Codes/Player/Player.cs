using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Sirenix.OdinInspector;

public class Player : MonoBehaviour, IDamage
{
    [SerializeField] private FollowCamera _followCamera;
    public float MaxHealth;
    public float Health;

    public static event Action<float,float> OnTakeHit;

    public void OnDead()
    {
        print("Dead");
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
        print("Hit");
        transform.position += Vector3.right * direction * pushForce;
    }
    private async Task TimeFreeze()
    {
        Time.timeScale = 0;
        await Task.Delay(TimeSpan.FromSeconds(.1f));
        Time.timeScale = 1;
    }
}
