using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private float _pushForce;
    [SerializeField] private float _playerPushForce;
    public int Direction;

    void Update()
    {

    }
    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.TryGetComponent(out IDamage damageable))
        {
            damageable.OnHit(1, Direction, _playerPushForce);
        }
    }

}