using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : BaseAgent
{
    

    private System.Random random;
    public List<int> dna;
    private float mutationRate;
    private List<int> perfectFish;
    private List<int> worstFish;
    public bool juvenile;

    public void Init(List<int> d,  System.Random r, float m, List<int> perfect, List<int> worst)
    {

        dna = d;
        mutationRate = m;
        random = r;
        worstFish = worst;
        perfectFish = perfect;
        worstFish = worst;
    }



    /*// Start is called before the first frame update
    void Start()
    {
        Debug.Log(CalculateFitness());
    }

    // Update is called once per frame
    void Update()
    {
        
    }*/
    
    // Una vez se ha detectado un pez con el que poder reproducirse se calculan las posibilidades y si son favorables se lleva a cabo
    public void Reproduction (Fish OtherFish)
    {
        if(!OtherFish.juvenile && !juvenile)
        {
            double av = (CalculateFitness() + OtherFish.CalculateFitness())/ 2;

            // Las posibilidades de reproduccion son mas altas conforme mayor sea el fitness de ambos
            if(random.NextDouble() < av)
            {
                int offspringNumber = (dna[3] + OtherFish.dna[3]) / 2;
                for(int i = 0; i < offspringNumber; i++)
                {
                    Crossover(OtherFish);
                }
            }
        }
    }

    public FishRoe Crossover(Fish otherParent)
    {
        
        GameObject g = new GameObject();
        g.AddComponent<FishRoe>();
        FishRoe child = g.GetComponent<FishRoe>();
        child.Init(random, mutationRate, perfectFish, worstFish);


        child.dna = new List<int>();
        int stat;
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
