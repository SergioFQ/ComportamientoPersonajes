using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedController : MonoBehaviour
{
    public Slider slider;
    public Text text;
    public void OnValueChanged()
    {
        Time.timeScale = slider.value/2;
        text.text = "Speed: " + slider.value/2 + "x";
    }
}
