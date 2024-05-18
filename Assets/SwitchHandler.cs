using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchHandler : MonoBehaviour, IDamage
{
    public event Action OnSwitch;
    private bool _isOpen;



    public void OnDead()
    {
    }

    public void OnHit(float damage, float direction, float pushForce)
    {
        if(_isOpen) return;
        OnSwitch?.Invoke();
        _isOpen = true;
    }
}
