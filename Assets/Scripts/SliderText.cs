using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderText : MonoBehaviour
{
    [SerializeField] private Slider sl;
    [SerializeField] private Text t;

    private void Start()
    {
        t.text = System.Math.Round(sl.value, 2).ToString();
    }
    public void UpdateValue() 
    {
        t.text = System.Math.Round(sl.value,2).ToString();
    }
}
