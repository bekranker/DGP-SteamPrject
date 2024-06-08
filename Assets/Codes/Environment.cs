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
        print(direction);
        DOTween.Kill(transform);
        DOTween.Kill(_pivot);
        _pivot.rotation = Quaternion.Euler(0, 0, -direction * _rotationSclae);
        transform.DOShakeScale(_punchSpeed, _punchSclae);
        _pivot.DORotate(new Vector3(0, 0, 0), _rotationSpeed);
        print("Hit");
    }
}