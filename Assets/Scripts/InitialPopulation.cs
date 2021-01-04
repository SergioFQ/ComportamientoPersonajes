using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialPopulation : MonoBehaviour
{
    /* DNA sequence
     * 0 - Vel
     * 1 - Accelaration
     * 2 - Physique (duracion que aguantan sin comer)
     * 3 - Offspring (potencial máximo de número de descendencia)
     * 4 - EggLayingTime (Duración en poner un huevo)
     * 5 - GrowingTime (Tiempo que tarda en crecer)
     * 6 - Hatching time (Tiempo que tarda un huevo en eclosionar)
     * 7 - NutritionalValue (Cantidad de hambre que quitan al depredador al ser comidos, el nutritionalValue de un animal aumenta en 10 al pasar a la siguiente etapa del ciclo de vida)
     * 8 - Lifespan (Duración máxima del ciclo de vida una vez son adultos)
     * 9 - PossibilityOfSuccess (Posibilidad de pasar a la siguiente etapa)
     */

    // Peces
    private List<Fish> fishPopulation;
    public float fishPopulationSize;
    public float maxFishVel = 10;
    public float minFishVel = 0;
    public float maxFishAcceleration = 10;
    public float minFishAcceleration = 0;
    public float maxFishPhys = 10;
    public float minFishPhys = 0;
    public float maxFishOffspring = 10;
    public float minFishOffspring = 0;
    public float maxFishEggLayingTime = 10;
    public float minFishEggLayingTime = 0;
    public float maxFishGrowingTime = 10;
    public float minFishGrowingTime = 0;
    public float maxFishHatchingTime = 10;
    public float minFishHatchingTime = 0;
    public float maxFishNutritionalValue = 10;
    public float minFishNutritionalValue = 0;
    public float maxFishLifespan = 10;
    public float minFishLifespan = 0;
    public float maxFishPossibilityOfSuccess = 10;
    public float minFishPossibilityOfSuccess = 0;
    public float fishMutationRate = 0.01f;

    // Moscas
    private List<Fly> flyPopulation;
    public int flyPopulationSize;

    // Ranas
    private List<Frog> frogPopulation;
    public float frogPopulationSize;
    public float maxFrogVel = 10;
    public float minFrogVel = 0;
    public float maxFrogAcceleration = 10;
    public float minFrogAcceleration = 0;
    public float maxFrogPhys = 10;
    public float minFrogPhys = 0;
    public float maxFrogOffspring = 10;
    public float minFrogOffspring = 0;
    public float maxFrogEggLayingTime = 10;
    public float minFrogEggLayingTime = 0;
    public float maxFrogGrowingTime = 10;
    public float minFrogGrowingTime = 0;
    public float maxFrogHatchingTime = 10;
    public float minFrogHatchingTime = 0;
    public float maxFrogNutritionalValue = 10;
    public float minFrogNutritionalValue = 0;
    public float maxFrogLifespan = 10;
    public float minFrogLifespan = 0;
    public float maxFrogPossibilityOfSuccess = 10;
    public float minFrogPossibilityOfSuccess = 0;
    public float frogMutationRate = 0.01f;

    // Plantas
    public int plantPopulationSize;

    // Start is called before the first frame update
    void Start()
    {
        fishPopulation = new List<Fish>();
        System.Random r = new System.Random();

        List<float> perfectFishDna = new List<float>();
        perfectFishDna.Add(maxFishVel);
        perfectFishDna.Add(maxFishAcceleration);
        perfectFishDna.Add(maxFishPhys);
        perfectFishDna.Add(maxFishOffspring);
        perfectFishDna.Add(maxFishEggLayingTime);
        perfectFishDna.Add(maxFishGrowingTime);
        perfectFishDna.Add(maxFishHatchingTime);
        perfectFishDna.Add(maxFishNutritionalValue);
        perfectFishDna.Add(maxFishLifespan);
        perfectFishDna.Add(maxFishPossibilityOfSuccess);

        List<float> worstFishDna = new List<float>();
        worstFishDna.Add(minFishVel);
        worstFishDna.Add(minFishAcceleration);
        worstFishDna.Add(minFishPhys);
        worstFishDna.Add(minFishOffspring);
        worstFishDna.Add(minFishEggLayingTime);
        worstFishDna.Add(minFishGrowingTime);
        worstFishDna.Add(minFishHatchingTime);
        worstFishDna.Add(minFishNutritionalValue);
        worstFishDna.Add(minFishLifespan);
        worstFishDna.Add(minFishPossibilityOfSuccess);

        List<float> fishDna;
        GameObject g;
        for (int i = 0; i < fishPopulationSize; i++)
        {
            fishDna = new List<float>();
            fishDna.Add(Random.Range(minFishVel, maxFishVel));
            fishDna.Add(Random.Range(minFishAcceleration, maxFishAcceleration));
            fishDna.Add(Random.Range(minFishPhys, maxFishPhys));
            fishDna.Add(Random.Range(minFishOffspring, maxFishOffspring));
            fishDna.Add(Random.Range(minFishEggLayingTime, maxFishEggLayingTime));
            fishDna.Add(Random.Range(minFishGrowingTime, maxFishGrowingTime));
            fishDna.Add(Random.Range(minFishHatchingTime, maxFishHatchingTime));
            fishDna.Add(Random.Range(minFishNutritionalValue, maxFishNutritionalValue));
            fishDna.Add(Random.Range(minFishLifespan, maxFishLifespan));
            fishDna.Add(Random.Range(minFishPossibilityOfSuccess, maxFishPossibilityOfSuccess));

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

        frogPopulation = new List<Frog>();

        List<float> perfectFrogDna = new List<float>();
        perfectFrogDna.Add(maxFrogVel);
        perfectFrogDna.Add(maxFrogAcceleration);
        perfectFrogDna.Add(maxFrogPhys);
        perfectFrogDna.Add(maxFrogOffspring);
        perfectFrogDna.Add(maxFrogEggLayingTime);
        perfectFrogDna.Add(maxFrogGrowingTime);
        perfectFrogDna.Add(maxFrogHatchingTime);
        perfectFrogDna.Add(maxFrogNutritionalValue);
        perfectFrogDna.Add(maxFrogLifespan);
        perfectFrogDna.Add(maxFrogPossibilityOfSuccess);

        List<float> worstFrogDna = new List<float>();
        worstFrogDna.Add(minFrogVel);
        worstFrogDna.Add(minFrogAcceleration);
        worstFrogDna.Add(minFrogPhys);
        worstFrogDna.Add(minFrogOffspring);
        worstFrogDna.Add(minFrogEggLayingTime);
        worstFrogDna.Add(minFrogGrowingTime);
        worstFrogDna.Add(minFrogHatchingTime);
        worstFrogDna.Add(minFrogNutritionalValue);
        worstFrogDna.Add(minFrogLifespan);
        worstFrogDna.Add(minFrogPossibilityOfSuccess);

        List<float> frogDna;
        for (int i = 0; i < frogPopulationSize; i++)
        {
            frogDna = new List<float>();
            frogDna.Add(Random.Range(minFrogVel, maxFrogVel));
            frogDna.Add(Random.Range(minFrogAcceleration, maxFrogAcceleration));
            frogDna.Add(Random.Range(minFrogPhys, maxFrogPhys));
            frogDna.Add(Random.Range(minFrogOffspring, maxFrogOffspring));
            frogDna.Add(Random.Range(minFrogEggLayingTime, maxFrogEggLayingTime));
            frogDna.Add(Random.Range(minFrogGrowingTime, maxFrogGrowingTime));
            frogDna.Add(Random.Range(minFrogHatchingTime, maxFrogHatchingTime));
            frogDna.Add(Random.Range(minFrogNutritionalValue, maxFrogNutritionalValue));
            frogDna.Add(Random.Range(minFrogLifespan, maxFrogLifespan));
            frogDna.Add(Random.Range(minFrogPossibilityOfSuccess, maxFrogPossibilityOfSuccess));

            g = new GameObject();
            g.AddComponent<BaseAgent>();
            g.AddComponent<Frog>();
            Frog frog = g.GetComponent<Frog>();

            frog.Init(frogDna, r, fishMutationRate, perfectFrogDna, worstFrogDna);
            frogPopulation.Add(frog);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
