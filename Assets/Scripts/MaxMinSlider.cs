using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaxMinSlider : MonoBehaviour
{
    [SerializeField] private Slider MinSlider;
    [SerializeField] private Slider MaxSlider;

    public void OnMinSliderChange() 
    {
        if (MinSlider.value > MaxSlider.value)
            MaxSlider.value = MinSlider.value;
    }
    public void OnMaxSliderChange()
    {
        if (MinSlider.value > MaxSlider.value)
            MinSlider.value = MaxSlider.value;
    }

}
