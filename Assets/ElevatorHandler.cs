using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class ElevatorHandler : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Transform _target;
    [SerializeField] private float _delay;


    private Vector3 _startPosition;


    private void Start()
    {
        _startPosition = transform.position;
        Elevate();
    }
    private void Elevate()
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOMove(_target.position, _speed).SetEase(Ease.Linear));
        seq.Append(DOVirtual.DelayedCall(_delay, () => print("delayed call one")));
        seq.Append(transform.DOMove(_startPosition, _speed).SetEase(Ease.Linear));
        seq.Append(DOVirtual.DelayedCall(_delay, () => Elevate()));
    }
}