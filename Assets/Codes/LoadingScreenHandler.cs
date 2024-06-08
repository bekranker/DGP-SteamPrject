using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;



public class LoadingScreenHandler : MonoBehaviour
{
    [Header("DoTween Props")]
    [SerializeField] private float _fadeInOutSpeed;

    [Header("Texts")]
    [SerializeField] private TMP_Text _sceneTitle;
    [SerializeField] private TMP_Text _sceneDescription;
    [SerializeField] private TMP_Text _loadingText;

    [SerializeField] private string _sceneName;
    [SerializeField] private string _sceneDescriptionText;
    [SerializeField] private string _nextSceneName;

    IEnumerator Start()
    {
        _sceneTitle.text = _sceneName;
        _sceneDescription.text = _sceneDescriptionText;
        DoTransaction(1);
        yield return new WaitForSeconds(5);
        DoTransaction(0).OnComplete(() => SceneManager.LoadScene(_nextSceneName));
    }




    private Tween DoTransaction(float endVale)
    {
        DOTween.KillAll();
        Sequence sequence = DOTween.Sequence();
        sequence.Join(_sceneTitle.DOFade(endVale, _fadeInOutSpeed));
        sequence.Join(_sceneDescription.DOFade(endVale, _fadeInOutSpeed));
        sequence.Join(_loadingText.DOFade(endVale, _fadeInOutSpeed));

        return sequence;
    }

}
