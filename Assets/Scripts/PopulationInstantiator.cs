using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * PopulationInstantiator: clase encargada de generar todas las huevas, moscas, plantas y rocas en la escena. Además, es
 * la clase que se encarga de asignar los valores máximos y mínimos del ADN de los agentes.
 */
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

    [Header("Agents Prefabs")]
    public GameObject huevoPezPrefab;
    public GameObject huevoRanaPrefab;
    public GameObject rocaPrefab;
    public GameObject plantaPrefab;
    public GameObject moscaPrefab;

    [Header("Number of Agents")]
    //Campo para indicar el número de renacuajos del flock
    /*[100]*/[SerializeField]public int numeroHuevosPez;
    /*[101]*/[SerializeField] public int numeroHuevosRana;
    /*[102]*/public int numeroRocas;
    /*[103]*/public int numeroMoscas;
    /*[104]*/public int numeroPlantas;
    /*[40]*/public int flyDelay;
    private List<float> perfectFlyDna;
    private List<float> worstFlyDna;

    // Huevos Pez
    [Header("Fish Attributes")]
    /*[20]*/[Range(0.1f, 10)] public float maxFishVel = 10;
    /*[21]*/[Range(0.1f, 10)] public float minFishVel = 0.1f;
    /*[22]*/[Range(0.1f, 10)] public float maxFishAcceleration = 10;
    /*[23]*/[Range(0.1f, 10)] public float minFishAcceleration = 0.1f;
    /*[24]*/[Range(0.05f, 0.2f)] public float maxFishPhys = 10;
    /*[25]*/[Range(0.05f, 0.2f)] public float minFishPhys = 0.1f;
    /*[26]*/[Range(0.1f, 10)] public float maxFishOffspring = 10;
    /*[27]*/[Range(0.1f, 10)] public float minFishOffspring = 0.1f;
    /*[28]*/[Range(0.1f, 10)] public float maxFishEggLayingTime = 10;
    /*[29]*/[Range(0.1f, 10)] public float minFishEggLayingTime = 0.1f;
    /*[210]*/[Range(0.1f, 10)] public float maxFishGrowingTime = 10;
    /*[211]*/[Range(0.1f, 10)] public float minFishGrowingTime = 0.1f;
    /*[212]*/[Range(0.1f, 10)] public float maxFishHatchingTime = 10;
    /*[213]*/[Range(0.1f, 10)] public float minFishHatchingTime = 0.1f;
    /*[214]*/[Range(0.1f, 10)] public float maxFishNutritionalValue = 10;
    /*[215]*/[Range(0.1f, 10)] public float minFishNutritionalValue = 0.1f;
    /*[216]*/[Range(40f,200)] public float maxFishLifespan = 10;
    /*[217]*/[Range(40f,200)] public float minFishLifespan = 0.1f;
    /*[218]*/[Range(0.1f, 10)] public float maxFishPossibilityOfSuccess = 10;
    /*[219]*/[Range(0.1f, 10)] public float minFishPossibilityOfSuccess = 0.1f;
    /*[220]*/[Range(0.1f, 1)]  public float minFishMutationRate = 0.01f;
    /*[221]*/[Range(0.1f, 1)] public float maxFishMutationRate = 1;

    //Huevos Rana
    [Header("Frog Attributes")]
    /*[30]*/[Range(0.1f, 10)] public float maxFrogVel = 10;
    /*[31]*/[Range(0.1f, 10)] public float minFrogVel = 0.1f;
    /*[32]*/[Range(0.1f, 10)] public float maxFrogAcceleration = 10;
    /*[33]*/[Range(0.1f, 10)] public float minFrogAcceleration = 0.1f;
    /*[34]*/[Range(0.05f, 0.2f)] public float maxFrogPhys = 10;
    /*[35]*/[Range(0.05f, 0.2f)] public float minFrogPhys = 0.1f;
    /*[36]*/[Range(0.1f, 10)] public float maxFrogOffspring = 10;
    /*[37]*/[Range(0.1f, 10)] public float minFrogOffspring = 0.1f;
    /*[38]*/[Range(0.1f, 10)] public float maxFrogEggLayingTime = 10;
    /*[39]*/[Range(0.1f, 10)] public float minFrogEggLayingTime = 0.1f;
    /*[310]*/[Range(0.1f, 10)] public float maxFrogGrowingTime = 10;
    /*[311]*/[Range(0.1f, 10)] public float minFrogGrowingTime = 0.1f;
    /*[312]*/[Range(0.1f, 10)] public float maxFrogHatchingTime = 10;
    /*[313]*/[Range(0.1f, 10)] public float minFrogHatchingTime = 0.1f;
    /*[314]*/[Range(0.1f, 10)] public float maxFrogNutritionalValue = 10;
    /*[315]*/[Range(0.1f, 10)] public float minFrogNutritionalValue = 0.1f;
    /*[316]*/[Range(40f,200)] public float maxFrogLifespan = 10;
    /*[317]*/[Range(40f,200)] public float minFrogLifespan = 0.1f;
    /*[318]*/[Range(0.1f, 10)] public float maxFrogPossibilityOfSuccess = 10;
    /*[319]*/[Range(0.1f, 10)] public float minFrogPossibilityOfSuccess = 0.1f;
    /*[320]*/[Range(0.1f, 1)] public float minFrogMutationRate = 0.01f;
    /*[321]*/[Range(0.1f, 1)] public float maxFrogMutationRate = 1;

    //Moscas
    [Header("Fly Attributes")]
    /*[41]*/[Range(0.1f, 10)] public float maxFlyVel = 10;
    /*[42]*/[Range(0.1f, 10)] public float minFlyVel = 0.1f;
    /*[43]*/[Range(0.1f, 10)] public float maxFlyAcceleration = 10;
    /*[44]*/[Range(0.1f, 10)] public float minFlyAcceleration = 0.1f;
    [Range(0.05f, 0.2f)] public float maxFlyPhys = 10;
    [Range(0.05f, 0.2f)] public float minFlyPhys = 0.1f;
    private float maxFlyOffspring = 0;
    private float minFlyOffspring = 0;
    private float maxFlyEggLayingTime = 0;
    private float minFlyEggLayingTime = 0;
    private float maxFlyGrowingTime = 0;
    private float minFlyGrowingTime = 0;
    private float maxFlyHatchingTime = 0;
    private float minFlyHatchingTime = 0;
    [Range(0.1f, 10)] public float maxFlyNutritionalValue = 10;
    [Range(0.1f, 10)] public float minFlyNutritionalValue = 0.1f;
    /*[45]*/[Range(40f,200)] public float maxFlyLifespan = 10;
    /*[46]*/[Range(40f,200)] public float minFlyLifespan = 0.1f;
    private float maxFlyPossibilityOfSuccess = 0;
    private float minFlyPossibilityOfSuccess = 0;
    private float minFlyMutationRate = 0;
    private float maxFlyMutationRate = 0;

    #region Unity Functions
    /*
     * StartSimulation: método encargado de guardar los listas de los mejores y peores ADNs
     * y de instanciar a los agentes.
     */
    public void StartSimulation()
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

        //Peces perfect y worst
        perfectFlyDna = new List<float>();
        perfectFlyDna.Add(maxFlyVel);
        perfectFlyDna.Add(maxFlyAcceleration);
        perfectFlyDna.Add(maxFlyPhys);
        perfectFlyDna.Add(maxFlyOffspring);
        perfectFlyDna.Add(maxFlyEggLayingTime);
        perfectFlyDna.Add(maxFlyGrowingTime);
        perfectFlyDna.Add(maxFlyHatchingTime);
        perfectFlyDna.Add(maxFlyNutritionalValue);
        perfectFlyDna.Add(maxFlyLifespan);
        perfectFlyDna.Add(maxFlyPossibilityOfSuccess);
        perfectFlyDna.Add(minFlyMutationRate);

        worstFlyDna = new List<float>();
        worstFlyDna.Add(minFlyVel);
        worstFlyDna.Add(minFlyAcceleration);
        worstFlyDna.Add(minFlyPhys);
        worstFlyDna.Add(minFlyOffspring);
        worstFlyDna.Add(minFlyEggLayingTime);
        worstFlyDna.Add(minFlyGrowingTime);
        worstFlyDna.Add(minFlyHatchingTime);
        worstFlyDna.Add(minFlyNutritionalValue);
        worstFlyDna.Add(minFlyLifespan);
        worstFlyDna.Add(minFlyPossibilityOfSuccess);
        worstFlyDna.Add(maxFlyMutationRate);

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

            roeFish.Init(fishRoeDna, perfectFishDna, worstFishDna);

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

            roeFrog.Init(frogRoeDna, perfectFrogDna, worstFrogDna);

        }

        for (int i = 0; i < numeroPlantas; i++)
        {
            Vector3 randomPos = Random.insideUnitSphere*18;
            randomPos.z = 0;
            Instantiate(plantaPrefab, transform.position+randomPos, Quaternion.identity);
        }

        for (int i = 0; i < numeroRocas; i++)
        {
            Vector3 randomPos = Random.insideUnitSphere*18;
            randomPos.z = 0;
            Instantiate(rocaPrefab, transform.position+randomPos, Quaternion.identity);
        }
        
        for (int i = 0; i < numeroMoscas; i++)
        {
            SpawnFly();
        }
        StartCoroutine(FlySpawn());
    }

    /*
     * OnDestroy: método encargardo de parar todas las corrutinas de la clase una vez se destruyan.
     */
    void OnDestroy()
    {
        StopAllCoroutines();
    }
    #endregion Unity Functions

    #region Functions
    /*
     * SpawnFly: método encargado de hacer aparecer moscas fuera del agua con un ADN determinado por
     * el mejor y el peor ADN de moscas.
     */
    private void SpawnFly()
    {
        List<float> flyDna;
        flyDna = new List<float>();
        flyDna.Add(Random.Range(minFlyVel, maxFlyVel));
        flyDna.Add(Random.Range(minFlyAcceleration, maxFlyAcceleration));
        flyDna.Add(Random.Range(minFlyPhys, maxFlyPhys));
        flyDna.Add(Random.Range(minFlyOffspring, maxFlyOffspring));
        flyDna.Add(Random.Range(minFlyEggLayingTime, maxFlyEggLayingTime));
        flyDna.Add(Random.Range(minFlyGrowingTime, maxFlyGrowingTime));
        flyDna.Add(Random.Range(minFlyHatchingTime, maxFlyHatchingTime));
        flyDna.Add(Random.Range(minFlyNutritionalValue, maxFlyNutritionalValue));
        flyDna.Add(Random.Range(minFlyLifespan, maxFlyLifespan));
        flyDna.Add(Random.Range(minFlyPossibilityOfSuccess, maxFlyPossibilityOfSuccess));
        flyDna.Add(Random.Range(minFlyMutationRate, maxFlyMutationRate));
        Vector2 randomPos = new Vector2(((Random.value<0.5f)?(-1):(1))*Random.Range(22,40),Random.Range(-22,20));
        BaseAgent fly = Instantiate(moscaPrefab, randomPos, Quaternion.identity).GetComponent<BaseAgent>();
        fly.Init(flyDna, perfectFlyDna, worstFlyDna);
    }

    /*
     * FlySpawn: corrutina encargada de llamar al método SpawnFly cada cierto tiempo. Una vez el obejto que
     * tenga esta clase se ddestruya, se parará la corrutina.
     */
    IEnumerator FlySpawn()
    {
        while (true)
        {
            SpawnFly();
            yield return new WaitForSeconds(flyDelay);//retornamos algo cualquiera
        }
    }
    #endregion Functions
}
