using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * MaxMinSlider: clase que obliga a los sliders
 * que modifican el valor máximo y el mínimo de
 * un mismo atributo a que sean coherentes.
 */
public class MaxMinSlider : MonoBehaviour
{
    [SerializeField] private Slider MinSlider;
    [SerializeField] private Slider MaxSlider;

    public void OnMinSliderChange() 
    {
        if (MinSlider.value > MaxSlider.value && MinSlider.interactable && MaxSlider.interactable)
            MaxSlider.value = MinSlider.value;
    }
    public void OnMaxSliderChange()
    {
        if (MinSlider.value > MaxSlider.value && MinSlider.interactable && MaxSlider.interactable)
            MinSlider.value = MaxSlider.value;
    }

}
