﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : BaseAgent
{
   
    private bool isWet;
    [SerializeField]
    public float wetness;
    [SerializeField]
    private bool isDry;
    [SerializeField]
    public float dryness;
    public GameObject ranaPrefab;

    protected override void Start()
    {
        base.Start();
        wetness = 1;
        dryness = 1;
        isWet = true;
        isDry = false;
    }

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
                /*case state.Wander:
                case state.Evade:
                    if (currentState == state.FrogIn)
                    {
                        if (wetness < 0.6)
                        {
                            return;
                        }
                    }
                    else if (currentState == state.FrogOut) 
                    {
                        if (dryness < 0.6)
                        {
                            return;
                        }
                    }
                    break;*/
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


    protected override void FrogOutAction()
    {
        Vector2 dest = new Vector2(transform.position.x, transform.position.y);
        _agent.SetDestination(dest.normalized * 22);
        Debug.DrawRay(transform.position, transform.position.normalized*22-transform.position);
    } 
    
    protected override void FrogInAction() 
    {
        _agent.SetDestination(Vector3.zero);
        Debug.DrawRay(transform.position, -transform.position);
    }
}
