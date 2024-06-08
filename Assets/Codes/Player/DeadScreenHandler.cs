using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class DeadScreenHandler : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private CanvasGroup _imageCanvasGroup;

    
    
    public void DeadScreen()
    {
        _canvasGroup.gameObject.SetActive(true);
        _canvasGroup.alpha = 0;
        _imageCanvasGroup.alpha = 0;
        _canvasGroup.DOFade(1, 1f).OnComplete(() =>
        {
            _imageCanvasGroup.DOFade(1, 1f);
        });
        gameObject.SetActive(true);
    }
}