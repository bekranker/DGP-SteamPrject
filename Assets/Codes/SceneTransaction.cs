using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransaction : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private float _fadeInOutSpeed;
    [SerializeField] private bool _fadeOutEnter;

    private bool _can;

    void Start()
    {
        if (_fadeOutEnter)
        {
            EnterScene();
        }
        _can = true;
    }

    public void EnterScene()
    {
        _canvasGroup.alpha = 1;
        _canvasGroup.DOFade(0, _fadeInOutSpeed);
    } 
    public Tween ExitScene()
    {
        if (!_can)return null;
        _canvasGroup.alpha = 0;
        _can = false;
        return _canvasGroup.DOFade(1, _fadeInOutSpeed);
    }
    public void GoToScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}