using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : BaseAgent
{
    [SerializeField] private LifeCycle ciclo;
    private System.Random random;
    public List<float> dna;
    private float mutationRate;
    private List<float> perfectFish;
    private List<float> worstFish;
    public bool juvenile;

    public void Init(List<float> d,  System.Random r, float m, List<float> perfect, List<float> worst)
    {
        dna = d;
        ciclo.dna = dna;
        mutationRate = m;
        random = r;
        worstFish = worst;
        perfectFish = perfect;  
        ciclo.perfectDna = perfectFish;
        ciclo.worstDna = worstFish;
    }

    
    // Una vez se ha detectado un pez con el que poder reproducirse se calculan las posibilidades y si son favorables se lleva a cabo
    public void Reproduction (Fish otherFish)
    {
        if(!otherFish.juvenile && !juvenile)
        {
            double av = (CalculateFitness() + otherFish.CalculateFitness())/ 2;

            // Las posibilidades de reproduccion son mas altas conforme mayor sea el fitness de ambos
            if(random.NextDouble() < av)
            {
                float offspringNumber = (dna[3] + otherFish.dna[3]) / 2;
                for(int i = 0; i < offspringNumber; i++)
                {
                    Crossover(otherFish);
                }
            }
        }
    }

    public Roe Crossover(Fish otherParent)
    {
        
        GameObject g = new GameObject();
        g.AddComponent<Roe>();
        Roe child = g.GetComponent<Roe>();
        string type = "Fish";
        child.Init(random, mutationRate, perfectFish, worstFish, type);


        child.dna = new List<float>();
        float stat;
        for(int i = 0; i < dna.Count; i++)
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
        for (int i = 0; i < perfectFish.Count; i++)
        {
            media += (float)dna[i] / perfectFish[i];
        }
        score = (float) media/dna.Count;

        return score;
    }

    
}
