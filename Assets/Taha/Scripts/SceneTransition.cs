using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;


public class SceneTransition : MonoBehaviour
{
    [SerializeField] private SceneTransaction _sceneTransaction;
    [SerializeField] private string _sceneName;
    void Start()
    {
        _sceneTransaction.ExitScene().OnComplete(() =>
        {
            SceneManager.LoadScene(_sceneName);
        });
    }
}
