using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopulationInstantiator : MonoBehaviour
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
     * 10 - MutationRate (Posibilidad de mutación genética) 
     */

    public GameObject huevoPezPrefab;
    public GameObject huevoRanaPrefab;

    //Campo para indicar el número de renacuajos del flock
    [SerializeField]
    public int numeroHuevosPez;
    [SerializeField]
    public int numeroHuevosRana;

    // Huevos Pez
    [Range(0.1f, 10)] public float maxFishVel = 10;
    [Range(0.1f, 10)] public float minFishVel = 0.1f;
    [Range(0.1f, 10)] public float maxFishAcceleration = 10;
    [Range(0.1f, 10)] public float minFishAcceleration = 0.1f;
    [Range(0.05f, 0.2f)] public float maxFishPhys = 10;
    [Range(0.05f, 0.2f)] public float minFishPhys = 0.1f;
    [Range(0.1f, 10)] public float maxFishOffspring = 10;
    [Range(0.1f, 10)] public float minFishOffspring = 0.1f;
    [Range(0.1f, 10)] public float maxFishEggLayingTime = 10;
    [Range(0.1f, 10)] public float minFishEggLayingTime = 0.1f;
    [Range(0.1f, 10)] public float maxFishGrowingTime = 10;
    [Range(0.1f, 10)] public float minFishGrowingTime = 0.1f;
    [Range(0.1f, 10)] public float maxFishHatchingTime = 10;
    [Range(0.1f, 10)] public float minFishHatchingTime = 0.1f;
    [Range(0.1f, 10)] public float maxFishNutritionalValue = 10;
    [Range(0.1f, 10)] public float minFishNutritionalValue = 0.1f;
    [Range(40f,200)] public float maxFishLifespan = 10;
    [Range(40f,200)] public float minFishLifespan = 0.1f;
    [Range(0.1f, 10)] public float maxFishPossibilityOfSuccess = 10;
    [Range(0.1f, 10)] public float minFishPossibilityOfSuccess = 0.1f;
    [Range(0.1f, 1)]  public float minFishMutationRate = 0.01f;
    [Range(0.1f, 1)] public float maxFishMutationRate = 1;

    //Huevos Rana
    [Range(0.1f, 10)] public float maxFrogVel = 10;
    [Range(0.1f, 10)] public float minFrogVel = 0.1f;
    [Range(0.1f, 10)] public float maxFrogAcceleration = 10;
    [Range(0.1f, 10)] public float minFrogAcceleration = 0.1f;
    [Range(0.05f, 0.2f)] public float maxFrogPhys = 10;
    [Range(0.05f, 0.2f)] public float minFrogPhys = 0.1f;
    [Range(0.1f, 10)] public float maxFrogOffspring = 10;
    [Range(0.1f, 10)] public float minFrogOffspring = 0.1f;
    [Range(0.1f, 10)] public float maxFrogEggLayingTime = 10;
    [Range(0.1f, 10)] public float minFrogEggLayingTime = 0.1f;
    [Range(0.1f, 10)] public float maxFrogGrowingTime = 10;
    [Range(0.1f, 10)] public float minFrogGrowingTime = 0.1f;
    [Range(0.1f, 10)] public float maxFrogHatchingTime = 10;
    [Range(0.1f, 10)] public float minFrogHatchingTime = 0.1f;
    [Range(0.1f, 10)] public float maxFrogNutritionalValue = 10;
    [Range(0.1f, 10)] public float minFrogNutritionalValue = 0.1f;
    [Range(40f,200)] public float maxFrogLifespan = 10;
    [Range(40f,200)] public float minFrogLifespan = 0.1f;
    [Range(0.1f, 10)] public float maxFrogPossibilityOfSuccess = 10;
    [Range(0.1f, 10)] public float minFrogPossibilityOfSuccess = 0.1f;
    [Range(0.1f, 1)] public float minFrogMutationRate = 0.01f;
    [Range(0.1f, 1)] public float maxFrogMutationRate = 1;


    // Start is called before the first frame update
    private void Start()
    { 
        //Peces perfect y worst
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
        perfectFishDna.Add(minFishMutationRate);

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
        worstFishDna.Add(maxFishMutationRate);

        //Ranas perfect y worst
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
        perfectFrogDna.Add(minFrogMutationRate);

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
        worstFrogDna.Add(maxFrogMutationRate);

        //Instanciador de huevos de pez
        List<float> fishRoeDna;
        for (int i = 0; i < numeroHuevosPez; i++)
        {
            fishRoeDna = new List<float>();
            fishRoeDna.Add(Random.Range(minFishVel, maxFishVel));
            fishRoeDna.Add(Random.Range(minFishAcceleration, maxFishAcceleration));
            fishRoeDna.Add(Random.Range(minFishPhys, maxFishPhys));
            fishRoeDna.Add(Random.Range(minFishOffspring, maxFishOffspring));
            fishRoeDna.Add(Random.Range(minFishEggLayingTime, maxFishEggLayingTime));
            fishRoeDna.Add(Random.Range(minFishGrowingTime, maxFishGrowingTime));
            fishRoeDna.Add(Random.Range(minFishHatchingTime, maxFishHatchingTime));
            fishRoeDna.Add(Random.Range(minFishNutritionalValue, maxFishNutritionalValue));
            fishRoeDna.Add(Random.Range(minFishLifespan, maxFishLifespan));
            fishRoeDna.Add(Random.Range(minFishPossibilityOfSuccess, maxFishPossibilityOfSuccess));
            fishRoeDna.Add(Random.Range(minFishMutationRate, maxFishMutationRate));
            Vector3 randomPos = Random.insideUnitSphere*10;
            randomPos.z = 0;
            Roe roeFish = Instantiate(huevoPezPrefab, transform.position+randomPos, Quaternion.identity).GetComponent<Roe>();

            roeFish.Init(fishRoeDna, perfectFishDna, worstFishDna, "fish");

        }

        //Instanciador de huevos de rana
        List<float> frogRoeDna;
        for (int i = 0; i < numeroHuevosRana; i++)
        {
            frogRoeDna = new List<float>();
            frogRoeDna.Add(Random.Range(minFrogVel, maxFrogVel));
            frogRoeDna.Add(Random.Range(minFrogAcceleration, maxFrogAcceleration));
            frogRoeDna.Add(Random.Range(minFrogPhys, maxFrogPhys));
            frogRoeDna.Add(Random.Range(minFrogOffspring, maxFrogOffspring));
            frogRoeDna.Add(Random.Range(minFrogEggLayingTime, maxFrogEggLayingTime));
            frogRoeDna.Add(Random.Range(minFrogGrowingTime, maxFrogGrowingTime));
            frogRoeDna.Add(Random.Range(minFrogHatchingTime, maxFrogHatchingTime));
            frogRoeDna.Add(Random.Range(minFrogNutritionalValue, maxFrogNutritionalValue));
            frogRoeDna.Add(Random.Range(minFrogLifespan, maxFrogLifespan));
            frogRoeDna.Add(Random.Range(minFrogPossibilityOfSuccess, maxFrogPossibilityOfSuccess));
            frogRoeDna.Add(Random.Range(minFrogMutationRate, maxFrogMutationRate));

            Vector3 randomPos = Random.insideUnitSphere*10;
            randomPos.z = 0;
            Roe roeFrog = Instantiate(huevoRanaPrefab, transform.position+randomPos, Quaternion.identity).GetComponent<Roe>();

            roeFrog.Init(frogRoeDna, perfectFrogDna, worstFrogDna, "Rana");

        }
    }
}
