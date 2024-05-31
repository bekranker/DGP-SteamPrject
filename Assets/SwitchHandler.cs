using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchHandler : MonoBehaviour, IDamage
{
    public event Action OnSwitch;
    private bool _isOpen;
    [SerializeField] private SpriteRenderer _sp;
    [SerializeField] private Sprite _openSprite; 



    public void OnDead()
    {
    }

    public void OnHit(float damage, float direction, float pushForce)
    {
        if(_isOpen) return;
        _sp.sprite = _openSprite;
        OnSwitch?.Invoke();
        _isOpen = true;
    }
}
