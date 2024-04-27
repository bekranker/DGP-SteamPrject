using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Player : MonoBehaviour, IDamage
{
    [SerializeField] private FollowCamera _followCamera;
    public float Health;

    public void OnDead()
    {
        print("Dead");
    }

    public async void OnHit(float damage, float direction, float pushForce)
    {
        if (Health - damage <= 0)
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
