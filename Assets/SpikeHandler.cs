using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;



public class SpikeHandler : MonoBehaviour
{

    [SerializeField] private Transform _spikeT;
    [SerializeField] private Transform _to;
    [SerializeField] private Transform _from;
    [SerializeField] private float _inSpeed;
    [SerializeField] private float _outSpeed;
    [SerializeField] private float _delay;




    void Start()
    {
        TheLoop();
    }
    void TheLoop()
    {
        DOVirtual.DelayedCall(_delay, ()=>
        {
            _spikeT.DOMove(_from.position, _outSpeed).SetEase(Ease.Linear).OnComplete(()=>
            {
                DOVirtual.DelayedCall(_delay, ()=>
                {
                    _spikeT.DOMove(_to.position, _inSpeed).SetEase(Ease.Linear).OnComplete(()=>TheLoop());
                });
            });
        });
    }
}