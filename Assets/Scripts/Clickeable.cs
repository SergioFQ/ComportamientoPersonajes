using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clickeable : MonoBehaviour
{
    public MiniCameraCotroller miniCameraController;
    public SpriteRenderer sR;
    private Color mainC;

    public void Awake()
    {
        miniCameraController = FindObjectOfType<MiniCameraCotroller>();
        mainC = sR.color;
    }

    public void IsTarget() 
    {
        sR.color = Color.red;
    }

    public void IsNotTarget() 
    {
        sR.color = mainC;
    }

    private void OnMouseDown()
    {
        miniCameraController.SelectTarget(gameObject);
    }
}
