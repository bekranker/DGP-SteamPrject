using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    [SerializeField] string FloatName;
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] Slider slider;

    float LastValue = 1;
    // Update is called once per frame
    void Update()
    {
        audioMixer.SetFloat(FloatName, Mathf.Lerp(-60, 0, slider.value));
    }

    public void MuteButton()
    {
        if (slider.value != 0)
        {
            LastValue = slider.value;
            slider.value = 0;
        }
        else
        {
            slider.value = LastValue;
        }
    }
}
