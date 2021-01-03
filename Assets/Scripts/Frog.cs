using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : BaseAgent
{
    private System.Random random;
    public List<int> dna;
    private float mutationRate;
    private List<int> perfectFrog;
    private List<int> worstFrog;

    public void Init(List<int> d, System.Random r, float m, List<int> perfect, List<int> worst)
    {

        dna = d;
        mutationRate = m;
        random = r;
        worstFrog = worst;
        perfectFrog = perfect;

    }
    

    // Una vez se ha detectado una rana con la que poder reproducirse se calculan las posibilidades y si son favorables se lleva a cabo
    public void Reproduction(Frog otherFrog)
    {
        double av = (CalculateFitness() + otherFrog.CalculateFitness()) / 2;

        // Las posibilidades de reproduccion son mas altas conforme mayor sea el fitness de ambos
        if (random.NextDouble() < av)
        {
            int offspringNumber = (dna[3] + otherFrog.dna[3]) / 2;
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


        child.dna = new List<int>();
        int stat;
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
}
