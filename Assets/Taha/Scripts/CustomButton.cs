using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ZilyanusLib.Audio;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using DG.Tweening;

public class CustomButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{

    [SerializeField] UnityEvent ButtonDown;
    [SerializeField] UnityEvent ButtonUp;
    [SerializeField] UnityEvent PointerEnter;
    [SerializeField] UnityEvent PointerExit;

    public string ButtonSound;
    [Header("Or")]
    [SerializeField] AudioClip ButtonClip;

    Animator animator;

    Vector3 DefaultScale;

    Button button;

    [SerializeField] float ButtonScaleFactor = 1.25f;
    [SerializeField] float ButtonScaleSpeed = 0.3f;

    [SerializeField] GameObject Cursor;
    private void Start()
    {
        animator = GetComponent<Animator>();
        DefaultScale = transform.localScale;

        button = GetComponent<Button>();
    }
  

    public void OnPointerDown(PointerEventData eventData)
    {
        ButtonDown.Invoke();

        if (animator != null)
            animator.SetBool("Click", true);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        ButtonUp.Invoke();
        AudioClass.PlayAudio("ButtonSound",0.2f);
        if (animator != null)
            animator.SetBool("Click", false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (animator != null)
            animator.SetBool("On", true);
        else if (!Application.isMobilePlatform && (button == null || button.interactable))
        {
            PointerEnter.Invoke();
            Vector3 ToScale = new Vector3(DefaultScale.x * ButtonScaleFactor, DefaultScale.y * ButtonScaleFactor, 1);
            transform.DOScale(ToScale, ButtonScaleSpeed).SetUpdate(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (animator != null)
            animator.SetBool("On", false);
        else if (!Application.isMobilePlatform && (button == null || button.interactable))
        {
            PointerExit.Invoke();
            transform.DOScale(DefaultScale, ButtonScaleSpeed).SetUpdate(true);
        }
    }

    private void OnDisable()
    {
        if (Cursor != null)
            Cursor.SetActive(false);

        transform.localScale = DefaultScale;
    }
}
