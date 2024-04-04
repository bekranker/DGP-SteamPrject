using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamage
{
    void OnHit(float damage, float direction, float pushForce);
    void OnDead();
}
