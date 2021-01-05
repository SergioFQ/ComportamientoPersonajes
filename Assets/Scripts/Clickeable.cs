using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Clickable: clase encargada de definir si un objeto de la escena es clickable o no.
 */
public class Clickeable : MonoBehaviour
{
    public MiniCameraCotroller miniCameraController;
    public SpriteRenderer sR;
    private LifeCycle lC;
    private Color mainC;
    #region Unity Functions
    /*
     * Awake: inicializa valores al crearse el agente.
     */
    public void Awake()
    {
        lC = gameObject.GetComponent<LifeCycle>();
        miniCameraController = FindObjectOfType<MiniCameraCotroller>();
        mainC = sR.color;
    }

    /*
     * OnMouseDown: método encargado de activar la cámara cuando un agente es seleccionado por
     * el usuario.
     */
    private void OnMouseDown()
    {
        miniCameraController.SelectTarget(gameObject);
    }
    #endregion Unity Functions
    
    #region Functions
    /*
     * SetTarget: método encargado de mostrar en rojo al agente seleccionado.
     */
    public void SetTarget() 
    {
        sR.color = Color.red;
        lC.isTarget = true;
    }
    /*
     * SetNotTarget: método encargado de mostrar con su color original al agente una
     * vez haya dejado de ser el objetivo de la minicámara.
     */
    public void SetNotTarget() 
    {
        sR.color = mainC;
        lC.isTarget = false;
    }

    /*
     * Evolve: se encarga de definir el nuevo objetivo de la cámara cuando un agente evoluciona.
     */
    public void Evolve() 
    {
        miniCameraController.SelectTarget(gameObject);
    }

    /*
     * Dead: desactiva la minicámara cuando el agente objetivo muere.
     */
    public void Dead() 
    {
        miniCameraController.CloseMiniCamera();
    }
    #endregion Functions
}
