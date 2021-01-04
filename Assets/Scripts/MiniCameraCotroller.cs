using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniCameraCotroller : MonoBehaviour
{
    private GameObject target;
    public Camera cam;
    public Text info;
    private string infoAux = "";

    public void SelectTarget(GameObject _target) 
    {
        if (!cam.gameObject.activeSelf) cam.gameObject.SetActive(true);
        if (target != null) TargetToNull();
        target = _target;
        target.GetComponent<Clickeable>().IsTarget();
    }

    public void TargetToNull() 
    {
        if (target == null) return;
        target.GetComponent<Clickeable>().IsNotTarget();
        target = null;
        info.text = "";
    }

    public void CloseMiniCamera() 
    {
        TargetToNull();
        cam.gameObject.SetActive(false);
    }

    public void Update()
    {
        if (target != null)
        {
            infoAux = "";
            infoAux += "AGENT INFO:" + "\n";
            infoAux += "Time alive: " + System.Math.Round(target.gameObject.GetComponent<LifeCycle>().timeOfLive,2) + "sec \n";
            if (!target.CompareTag("HuevosRana") && !target.CompareTag("HuevosPez"))
            {
                infoAux += "Hunger: " + System.Math.Round(target.gameObject.GetComponent<BaseAgent>().hunger,2) + "\n";
                if (target.CompareTag("Rana"))
                {
                    infoAux += "Dryness: " + System.Math.Round(target.gameObject.GetComponent<Frog>().dryness,2) + "\n";
                    infoAux += "Wetness: " + System.Math.Round(target.gameObject.GetComponent<Frog>().wetness,2) + "\n";
                }
            }
            else 
            {
                //Cosas de los huevos
            }
            info.text = infoAux;
            cam.transform.position = new Vector3(target.transform.position.x, target.transform.position.y, cam.transform.position.z);
        }
    }

}
