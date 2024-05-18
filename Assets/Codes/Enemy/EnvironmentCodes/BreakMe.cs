
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public static class BreakMe
{
    public static void ShakeObject(int repeatCount, Transform target, float shakeForce, float shakeDuration, bool delay = false,Action action = null)
    {
        for (int i = 0; i < repeatCount; i++)
        {
            if (i == repeatCount - 1)
            {
                target.DOPunchPosition(Vector2.right * UnityEngine.Random.Range(-1f, 1f) * shakeForce, shakeDuration).OnComplete(() => 
                {
                    if (delay)
                    {
                        DOVirtual.DelayedCall(0.5f,() => action?.Invoke());
                    }
                    else
                    {
                        action?.Invoke();
                    }
                });
            }
            else
            {
                target.DOPunchPosition(Vector2.right * UnityEngine.Random.Range(-1f, 1f) * shakeForce, shakeDuration);
            }
        }
    }
}