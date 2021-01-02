using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialPopulation : MonoBehaviour
{
    private List<Fish> fishPopulation;
    public int fishPopulationSize;
    public int maxFishVel = 10;
    public int minFishVel = 0;
    public int maxFishAcceleration = 10;
    public int minFishAcceleration = 0;
    public int maxFishPhys = 10;
    public int minFishPhys = 0;
    public int maxFishOffspring = 10;
    public int minFishOffspring = 0;
    public int maxFishEggLayingTime = 10;
    public int minFishEggLayingTime = 0;
    public int maxFishGrowingTime = 10;
    public int minFishGrowingTime = 0;
    public int maxFishHatchingTime = 10;
    public int minFishHatchingTime = 0;
    public int maxFishNutritionalValue = 10;
    public int minFishNutritionalValue = 0;
    public int maxFishLifespan = 10;
    public int minFishLifespan = 0;
    public float fishMutationRate = 0.01f;

    private List<Fly> flyPopulation;
    public int flyPopulationSize;
    


    // Start is called before the first frame update
    void Start()
    {
        fishPopulation = new List<Fish>();
        System.Random r = new System.Random();

        List<int> perfectFishDna = new List<int>();
        perfectFishDna.Add(maxFishVel);
        perfectFishDna.Add(maxFishAcceleration);
        perfectFishDna.Add(maxFishPhys);
        perfectFishDna.Add(maxFishOffspring);
        perfectFishDna.Add(maxFishEggLayingTime);
        perfectFishDna.Add(maxFishGrowingTime);
        perfectFishDna.Add(maxFishHatchingTime);
        perfectFishDna.Add(maxFishNutritionalValue);
        perfectFishDna.Add(maxFishLifespan);

        List<int> worstFishDna = new List<int>();
        worstFishDna.Add(minFishVel);
        worstFishDna.Add(minFishAcceleration);
        worstFishDna.Add(minFishPhys);
        worstFishDna.Add(minFishOffspring);
        worstFishDna.Add(minFishEggLayingTime);
        worstFishDna.Add(minFishGrowingTime);
        worstFishDna.Add(minFishHatchingTime);
        worstFishDna.Add(minFishNutritionalValue);
        worstFishDna.Add(minFishLifespan);

        List<int> fishDna;
        GameObject g;
        for (int i = 0; i < fishPopulationSize; i++)
        {
            fishDna = new List<int>();
            fishDna.Add(Random.Range(minFishVel, maxFishVel));
            fishDna.Add(Random.Range(minFishAcceleration, maxFishAcceleration));
            fishDna.Add(Random.Range(minFishPhys, maxFishPhys));
            fishDna.Add(Random.Range(minFishOffspring, maxFishOffspring));
            fishDna.Add(Random.Range(minFishEggLayingTime, maxFishEggLayingTime));
            fishDna.Add(Random.Range(minFishGrowingTime, maxFishGrowingTime));
            fishDna.Add(Random.Range(minFishHatchingTime, maxFishHatchingTime));
            fishDna.Add(Random.Range(minFishNutritionalValue, maxFishNutritionalValue));
            fishDna.Add(Random.Range(minFishLifespan, maxFishLifespan));

            g = new GameObject();
            g.AddComponent<BaseAgent>();
            g.AddComponent<Fish>();
            Fish fish = g.GetComponent<Fish>();
            
            fish.Init(fishDna, r, fishMutationRate, perfectFishDna, worstFishDna);
            fishPopulation.Add(fish);
        }

        for(int i = 0; i < flyPopulationSize; i++)
        {
            g = new GameObject();
            g.AddComponent<BaseAgent>();
            g.AddComponent<Fly>();
            Fly fly = g.GetComponent<Fly>();

            fly.Init( r);
            flyPopulation.Add(fly);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
