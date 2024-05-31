using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderColorScript : MonoBehaviour
{
    [SerializeField] Slider MainSlider;
    [SerializeField] Image SliderHandle;
    float AlphaMin = 0.04313726f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SliderHandle.color = new Color(SliderHandle.color.r, SliderHandle.color.g, SliderHandle.color.b, Mathf.Lerp(AlphaMin,1, MainSlider.value));
    }
}
