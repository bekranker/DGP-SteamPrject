using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamage
{
    public float Health;

    public void OnDead()
    {
        print("Dead");
    }

    public void OnHit(float damage, float direction, float pushForce)
    {
        if (Health - damage <= 0)
        {
            OnDead();
            return;
        }
        print("Hit");
        transform.position += Vector3.right * direction * pushForce;
    }
}
