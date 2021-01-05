using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Frog: clase rana que hereda de BaseAgent. Esta clase se 
 * encargará de sobrescribir ciertos métodos para su propio
 * comportamiento.
 */
public class Frog : BaseAgent
{
    [SerializeField]
    private bool isDry;
    private bool isWet;
    public float wetness;
    public float dryness;
    public GameObject ranaPrefab;

    #region Unity Functions
    /*
     * Start: llama al Start del padre y además inicializa parámetros propios de la rana.
     */
    protected override void Start()
    {
        base.Start();
        wetness = 1;
        dryness = 1;
        isWet = true;
        isDry = false;
    }
    /*
     * FixedUpdate: llama al FixedUpdate del padre y detecta cuando la rana necesita cambiar el estado de
     * salir al esterior del estanque a por oxígeno y cuando debe entrar en el estanque para hidratarse.
     */
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if (hunger < 0) return;
        if (Mathf.Pow(transform.position.x, 2) + Mathf.Pow(transform.position.y, 2) < Mathf.Pow(20.5f, 2))
        {
            
            wetness += 0.1f * Time.fixedDeltaTime;
            dryness -= 0.025f * Time.fixedDeltaTime;
            if (dryness < 0.6)
            { 
                ChangeState(state.FrogOut);
                isWet = true;
                isDry = false;
            } 
            if (dryness < 0) DeadAction();
            
            if (wetness > 1)
            {
                wetness = 1;
            }

        }
        else
        {
            wetness -= 0.025f * Time.fixedDeltaTime;
            if (wetness < 0.6)
            {
                ChangeState(state.FrogIn);
                isWet = false;
                isDry = true;
            }
            if (wetness < 0) DeadAction();

            dryness += 0.1f * Time.fixedDeltaTime;
          
           
            if (dryness > 1)
            {
                dryness = 1;
            }

        }  
    }
    #endregion Unity Functions

    #region Functions
    /*
     * ChangeState: sobrescribe el método del padre de change de ChangeState para implementar los estados 
     * únicos de la rana (entrar y salir del estanque).
     */
    protected override void ChangeState(state nextState)
    {
        if (nextState == state.Pursuit && _pursuitTarget!=null && _pursuitTarget.gameObject!=null && _pursuitTarget.gameObject.CompareTag("Pez") && _pursuitTarget.GetComponent<Fish>()?.juvenile==false) return;
        if (currentState == state.Pursuit && nextState==state.FrogIn && wetness>hunger) 
        {
            return;
        }
        if (currentState == state.Pursuit && nextState == state.FrogOut && dryness > hunger)
        {
            return;
        }

        if (currentState==state.FrogIn||currentState==state.FrogOut) 
        {
            switch (nextState)
            {
                case state.Pursuit:
                    if (currentState == state.FrogIn)
                    {
                        if (wetness < hunger)
                        {
                            return;
                        }
                    }
                    else if (currentState == state.FrogOut) 
                    {
                        if (dryness < hunger)
                        {
                            return;
                        }
                    }
                    break;
            }    
        }
      

        base.ChangeState(nextState);
    }

    /*
     * FrogOutAction: método llamado cuando el estado de la rana sea el de salir del estanque cuando necesite oxígeno.
     */
    protected override void FrogOutAction()
    {
        Vector2 dest = new Vector2(transform.position.x, transform.position.y);
        _agent.SetDestination(dest.normalized * 22);
        Debug.DrawRay(transform.position, transform.position.normalized*22-transform.position);
    }

    /*
     * FrogInAction: método llamado cuando el estado de la rana sea el de entrar en el estanque cuando necesite hidratarse.
     * 
     */
    protected override void FrogInAction() 
    {
        _agent.SetDestination(Vector3.zero);
        Debug.DrawRay(transform.position, -transform.position);
    }
    #endregion Functions
}
