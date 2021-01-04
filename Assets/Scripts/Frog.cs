using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : BaseAgent
{
   
    private bool isWet;
    [SerializeField]
    private float wetness;
    [SerializeField]
    private bool isDry;
    [SerializeField]
    private float dryness;
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
        if (Mathf.Pow(transform.position.x, 2) + Mathf.Pow(transform.position.y, 2) < Mathf.Pow(20.5f, 2))
        {
            
            wetness += 0.05f * Time.fixedDeltaTime;
            dryness -= 0.05f * Time.fixedDeltaTime;
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
            wetness -= 0.05f * Time.fixedDeltaTime;
            if (wetness < 0.6)
            {
                ChangeState(state.FrogIn);
                isWet = false;
                isDry = true;
            }
            if (wetness < 0) DeadAction();

            dryness += 0.05f * Time.fixedDeltaTime;
          
           
            if (dryness > 1)
            {
                dryness = 1;
            }

        }


       
    }


    // Una vez se ha detectado una rana con la que poder reproducirse se calculan las posibilidades y si son favorables se lleva a cabo
    public void Reproduction(Frog otherFrog)
    {
        double av = (CalculateFitness() + otherFrog.CalculateFitness()) / 2;

        // Las posibilidades de reproduccion son mas altas conforme mayor sea el fitness de ambos
        if (random.NextDouble() < av)
        {
            float offspringNumber = (dna[3] + otherFrog.dna[3]) / 2;
            for (int i = 0; i < offspringNumber; i++)
            {
                
                Crossover(otherFrog);
            }
        }
    }

    
    public void Crossover(Frog otherParent)
    {
        List<float> newDna = new List<float>();
        float stat;
        for (int i = 0; i < dna.Count; i++)
        {
            stat = random.NextDouble() < 0.5 ? dna[i] : otherParent.dna[i];
            newDna.Add(stat);
        }

        Roe roeFrog = Instantiate(ranaPrefab, transform.position, Quaternion.identity).GetComponent<Roe>();
        roeFrog.Init(newDna, random,perfectDna, worstDna, "frog");

        roeFrog.Mutate();
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
                case state.Wander:
                case state.Evade:
                    return;

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
        _agent.SetDestination(transform.position.normalized * 22);
       // Debug.DrawRay(transform.position, transform.position.normalized*22-transform.position);
    } 
    
    protected override void FrogInAction() 
    {
        _agent.SetDestination(Vector3.zero);
       // Debug.DrawRay(transform.position, -transform.position);
    }
}
