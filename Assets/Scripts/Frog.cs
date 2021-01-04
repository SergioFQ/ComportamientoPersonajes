using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : BaseAgent
{
    [SerializeField] private LifeCycle ciclo;
    private System.Random random;
    public List<float> dna;
    private float mutationRate;
    private List<float> perfectFrog;
    private List<float> worstFrog;
    private bool isWet;
    [SerializeField]
    private float wetness;
    [SerializeField]
    private bool isDry;
    [SerializeField]
    private float dryness;

    public void Init(List<float> d, System.Random r, float m, List<float> perfect, List<float> worst)
    {
        dna = d;
        ciclo.dna = dna;
        mutationRate = m;
        random = r;
        worstFrog = worst;
        perfectFrog = perfect;
        ciclo.perfectDna = perfectFrog;
        ciclo.worstDna = worstFrog;
    }

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

    
    public Roe Crossover(Frog otherParent)
    {

        GameObject g = new GameObject();
        g.AddComponent<Roe>();
        Roe child = g.GetComponent<Roe>();
        string type = "frog";
        child.Init(random, mutationRate, perfectFrog, worstFrog, type);


        child.dna = new List<float>();
        float stat;
        for (int i = 0; i < dna.Count; i++)
        {
            stat = random.NextDouble() < 0.5 ? dna[i] : otherParent.dna[i];
            child.dna.Add(stat);
        }

        child.Mutate();
        return child;
    }


    float CalculateFitness()
    {
        float score;
        float media = 0;
        for (int i = 0; i < perfectFrog.Count; i++)
        {
            media += (float)dna[i] / perfectFrog[i];
        }
        score = (float)media / dna.Count;

        return score;
    }

    protected override void ChangeState(state nextState)
    {//habría que comprobar inicialmente en que estado nos encontramos

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
