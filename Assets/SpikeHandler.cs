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
        Sequence sequence= DOTween.Sequence();
        
        sequence.Append(_spikeT.DOMove(_to.position, _inSpeed)).SetEase(Ease.Linear);
        sequence.Append(DOVirtual.DelayedCall(_delay, () => print("")));
        sequence.Append(_spikeT.DOMove(_from.position, _outSpeed)).SetEase(Ease.Linear);
        sequence.Append(DOVirtual.DelayedCall(_delay, () => print("")));
        sequence.OnComplete(TheLoop);
        sequence.Play();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            other.GetComponent<IDamage>().OnHit(9999, 0, 0);
        }
    }
}
