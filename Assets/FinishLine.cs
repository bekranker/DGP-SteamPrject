using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    [SerializeField] private SceneTransaction _sceneT;
    void OnTriggerEnter2D (Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _sceneT.ExitScene().OnComplete(() =>
            {
                _sceneT.GoToScene("Credits");
            });
        }
    }
}
