using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.Assertions.Must;

public class WhileTriggerZone : MonoBehaviour
{
    private bool _triggered = false;
    [SerializeField] InputHandler _inputHandler; 
    [SerializeField] private GameObject _activeThat;
    [SerializeField] private Transform _camera;
    [SerializeField] private Image _blackImage;
    [SerializeField] private float _zoomOutSpeed, _fadeOutSpeed;


    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.tag == "Player")
        {
            print("Sound Played");
            _triggered = true;
        }
    }
    void OnTriggerExit2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.tag == "Player")
        {
            _triggered = false;
        }
    }
    void Update()
    {
        if (_inputHandler.InteractiveInput)
        {
            if(!_triggered) return;
            _activeThat.SetActive(true);
        }
    }
    public void SceneTransaction()
    {
        DOTween.KillAll();
        Sequence sequence= DOTween.Sequence();
        //_camera.GetComponent<FollowCamera>().enabled = false;
        sequence.Join(_blackImage.DOFade(1, _fadeOutSpeed));
        sequence.Join(_camera.DOMoveZ(-10, _zoomOutSpeed));
        sequence.OnComplete(() => 
        {
            print("Loading Screen loading");
            SceneManager.LoadScene("LoadingScene");
        });
    }
}
