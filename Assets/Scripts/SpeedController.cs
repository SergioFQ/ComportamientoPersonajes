using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/*
 * SpeedController: clase encargada de ajustar la
 * velocidad de la simulación.
 */
public class SpeedController : MonoBehaviour
{
    public Slider slider;
    public Text text;
    public void OnValueChanged()
    {
        Time.timeScale = slider.value/2;
        text.text = "Speed: " + slider.value/2 + "x";
    }

    public void ResetSimulation()
    {
        SceneManager.LoadScene(0);
    }

    public void CloseGame()
    {
        Application.Quit();
    }
}
