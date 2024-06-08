using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    [SerializeField] private SceneTransaction _sceneTransaction;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            _sceneTransaction.ExitScene().OnComplete(()=> 
            {
                _sceneTransaction.GoToScene("CutScene2");
            });
        }        
    }
}
