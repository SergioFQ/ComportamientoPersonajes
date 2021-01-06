using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * MiniCameraController: clase encargada de definir los comportamientos de la minicámara que muestra las
 * estadísticas de un agente en concreto de la escena elegido por el usuario.
 */
public class MiniCameraCotroller : MonoBehaviour
{
    private GameObject target;
    public Camera cam;
    public Text info;
    private string infoAux = "";

    #region Unity Functions
    /*
     * Update: actualiza la información del agente por pantalla. 
     */
    public void Update()
    {
        if (target != null)
        {
            infoAux = "";
            infoAux += "AGENT INFO:" + "\n";
            infoAux += "Time alive: " + Mathf.Round(target.gameObject.GetComponent<LifeCycle>().timeOfLife) + "s \n";
            infoAux += "Time left: " + (Mathf.Round(target.gameObject.GetComponent<LifeCycle>().dna[8]) - Mathf.Round(target.gameObject.GetComponent<LifeCycle>().timeOfLife)) + "s \n";
            if (!target.CompareTag("HuevosRana") && !target.CompareTag("HuevosPez"))
            {
                infoAux += "Hunger: " + Mathf.Round(target.gameObject.GetComponent<BaseAgent>().hunger*100) + "%\n";
                if (target.CompareTag("Rana"))
                {
                    infoAux += "Dryness: " + Mathf.Round(target.gameObject.GetComponent<Frog>().dryness*100) + "%\n";
                    infoAux += "Wetness: " + Mathf.Round(target.gameObject.GetComponent<Frog>().wetness*100) + "%\n";
                }
            }
            info.text = infoAux;
            cam.transform.position = new Vector3(target.transform.position.x, target.transform.position.y, cam.transform.position.z);
        }
    }
    #endregion Unity Functions

    #region Functions
    /*
     * SelectTarget: función llamada al clickar sobre un agente de la escena y cuando este evoluciona para mostrar la
     * información del propio agente.
     */
    public void SelectTarget(GameObject _target) 
    {
        if (!cam.gameObject.activeSelf) cam.gameObject.SetActive(true);
        if (target != null) TargetToNull();
        target = _target;
        target.GetComponent<Clickeable>().SetTarget();
    }

    /*
     * TargetToNull: método usado para quitar el foco del antiguo agente seleccionado cuando este muere o 
     * cuando el usuario decide seleccionar otro.
     */
    public void TargetToNull() 
    {
        if (target == null) return;
        target.GetComponent<Clickeable>().SetNotTarget();
        target = null;
        info.text = "";
    }

    /*
     * CloseMiniCamera: método encargado de cerrar la minicamera cuando el usuario lo desee o cuando muera el 
     * agente que estábamos observando.
     */
    public void CloseMiniCamera() 
    {
        TargetToNull();
        cam.gameObject.SetActive(false);
    }

    

    #endregion Functions
}
