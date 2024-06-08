using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
    [SerializeField] Image BarImage;

    private void OnEnable()
    {
        Player.OnTakeHit += OnPLayerGetHit;
    }

    private void OnDisable()
    {
        Player.OnTakeHit -= OnPLayerGetHit;
    }

    void OnPLayerGetHit(float value, float MaxHealth)
    {
        BarImage.fillAmount = (value / MaxHealth);
    }
}
