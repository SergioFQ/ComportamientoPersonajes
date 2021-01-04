using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clickeable : MonoBehaviour
{
    public MiniCameraCotroller miniCameraController;
    public SpriteRenderer sR;
    private LifeCycle lC;
    private Color mainC;

    public void Awake()
    {
        lC = gameObject.GetComponent<LifeCycle>();
        miniCameraController = FindObjectOfType<MiniCameraCotroller>();
        mainC = sR.color;
    }

    public void IsTarget() 
    {
        sR.color = Color.red;
        lC.isTarget = true;
    }

    public void IsNotTarget() 
    {
        sR.color = mainC;
        lC.isTarget = false;
    }

    private void OnMouseDown()
    {
        miniCameraController.SelectTarget(gameObject);
    }

    public void Evolve() 
    {
        miniCameraController.SelectTarget(gameObject);
    }

    public void Dead() 
    {
        miniCameraController.CloseMiniCamera();
    }
}
