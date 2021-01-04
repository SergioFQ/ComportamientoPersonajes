using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : BaseAgent
{
    public bool juvenile;
    public GameObject pezPrefab;
    
    // Una vez se ha detectado un pez con el que poder reproducirse se calculan las posibilidades y si son favorables se lleva a cabo
    public void Reproduction (Fish otherFish)
    {
        if(!otherFish.juvenile && !juvenile)
        {
            double av = (CalculateFitness() + otherFish.CalculateFitness())/ 2;

            // Las posibilidades de reproduccion son mas altas conforme mayor sea el fitness de ambos
            if(Random.value < av)
            {
                float offspringNumber = (dna[3] + otherFish.dna[3]) / 2;
                for(int i = 0; i < offspringNumber; i++)
                {
                    Crossover(otherFish);
                }
            }
        }
    }

    public void Crossover(Fish otherParent)
    {
        List<float> newDna = new List<float>();
        float stat;
        for (int i = 0; i < dna.Count; i++)
        {
            stat = Random.value < 0.5 ? dna[i] : otherParent.dna[i];
            newDna.Add(stat);
        }

        Roe roeFish = Instantiate(pezPrefab, transform.position, Quaternion.identity).GetComponent<Roe>();
        roeFish.Init(newDna, perfectDna, worstDna, "fish");

        roeFish.Mutate();
    }

    


    
}
