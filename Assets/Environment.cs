using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;



public class Environment : MonoBehaviour, IDamage
{
    [SerializeField] private float _punchSclae;
    [SerializeField] private float _punchSpeed;
    [SerializeField] private float _rotationSclae;
    [SerializeField] private float _rotationSpeed;

    [SerializeField] private Transform _pivot;

    public void OnDead()
    {
    }

    public void OnHit(float damage, float direction, float pushForce)
    {
        transform.DOShakeScale(_punchSpeed, _punchSclae);
        _pivot.DOShakeRotation(_rotationSclae, _rotationSpeed);
        print("Hit");
    }
}