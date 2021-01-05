using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

/*
 * LifeCycle: clase encargada del ciclo de vida de los diferentes objetos.
 */
public class LifeCycle : MonoBehaviour
{
    [SerializeField] private GameObject agent;
    [SerializeField] private float timeInPhase;
    private BaseAgent baseAgent;
    public MiniCameraCotroller miniCameraController;
    private Plant plantComponent;
    public bool canBeEaten;
    public bool isTarget;
    public float timeOfLife;
    private float rand;
    public List<float> dna;
    public List<float> perfectDna;
    public List<float> worstDna;

    #region Unity Functions
    /*
     * Start: inicializa todo los atributos.
     */
    public void Start()
    {
        miniCameraController = FindObjectOfType<MiniCameraCotroller>();
        plantComponent = gameObject.GetComponent<Plant>();
        if (gameObject.CompareTag("Plantas")) { StartCoroutine(GrowPlant(plantComponent.plantGrowTime)); timeOfLife = 0.0f; return;  }
        baseAgent = GetComponent<BaseAgent>();
        if (!gameObject.CompareTag("HuevosRana") && !gameObject.CompareTag("HuevosPez"))
        {
            dna = baseAgent.dna;
            perfectDna = baseAgent.perfectDna;
            worstDna = baseAgent.worstDna;
            return;
        }
        if (agent != null)
        {
            dna = gameObject.GetComponent<Roe>().dna;
            perfectDna = gameObject.GetComponent<Roe>().perfectDna;
            worstDna = gameObject.GetComponent<Roe>().worstDna;
        }
        timeOfLife = 0.0f;
        timeInPhase = 0.0f;
    }

    /*
     * Update: actualiza el tiempo que lleva vivo un agente y el tiempo que lleva en la fase actual (por ejemplo en la fase renacuajo).
     * Además llama al método StatePhase que definirá si el agente o no cambia de fase.
     */
    private void Update()
    {
        timeOfLife += Time.deltaTime;
        timeInPhase += Time.deltaTime;
        if (!gameObject.CompareTag("Plantas")) StatePhase();
    }
    #endregion Unity Functions

    #region Agents Functions
    /*
     * InitCycle: define las características del agente en su nueva fase.
     */
    public void InitCycle(List<float> d, float _timeLived, List<float> worst, List<float> perfect)
    {
        dna = d;
        worstDna = worst;
        perfectDna = perfect;
        timeOfLife = _timeLived;
        timeInPhase = 0.0f;
    }
    /*
     * NewPhase: se encarga de cambiar la fase del agente.
     */
    public void NewPhase()
    {
        if (agent != null)
        {
            GameObject obj = Instantiate(agent, new Vector2(this.transform.position.x, this.transform.position.y), Quaternion.identity);
            BaseAgent objAgent = obj.GetComponent<BaseAgent>();
            if (baseAgent!=null)
            {
                objAgent.Init(dna, perfectDna, worstDna);
                if (isTarget) obj.GetComponent<Clickeable>().Evolve();
                baseAgent.DeadAction();
            }
            else
            {
                objAgent.Init(dna, perfectDna, worstDna);
                if (isTarget) obj.GetComponent<Clickeable>().Evolve();
                Destroy(gameObject);
            }
            obj.GetComponent<LifeCycle>().InitCycle(dna, timeOfLife, worstDna, perfectDna);
        }
    }
    /*
     * StatePhase: método que determina si el agente actual muere o evoluciona si está en una fase 
     * que puede evolucionar y lleva suficiente tiempo en esta fase. Además, el cambiar de fase 
     * viene dado por una probabilidad de éxito.
     */
    void StatePhase()
    {
        if (timeOfLife >= dna[8])
        {
            if (gameObject.CompareTag("HuevosPez") || gameObject.CompareTag("HuevosRana"))
            {
                gameObject.GetComponent<Roe>().DeadRoe();
            }
            else
            {
                baseAgent.DeadAction();
            }
        }
        else
        {
            bool comparison = false;
            if (gameObject.CompareTag("HuevosPez") || gameObject.CompareTag("HuevosRana"))
            {
                comparison = (timeInPhase >= dna[6]);
            }
            else
            {
                comparison = (timeInPhase >= dna[5]);
            }

            if (comparison)
            {
                rand = Random.Range(worstDna[9], perfectDna[9]);
                if (rand <= dna[9])
                {
                    NewPhase();
                }
            }
        }
    }
    #endregion Agents Functions

    #region Plant Functions
    /*
     * EatPlant: método usado solo en las plantas. Este método es el encargado de cambiar el sprite de las plantas
     * cuando un agente se las come y de impedir que se la vuelvan a comer hasta que haya vuelto a crecer.
     */
    public void EatPlant() 
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = plantComponent.sprites[0];
        canBeEaten = false;
        StartCoroutine(GrowPlant(plantComponent.plantGrowTime));
    }
    /*
     * GrowPlant: corrutina encargada de hacer crecer a la planta de nuevo una vez haya sido comida tras un 
     * tiempo determinado.
     */
    IEnumerator GrowPlant(float dur)
    {
        yield return new WaitForSeconds(dur);
        gameObject.GetComponent<SpriteRenderer>().sprite = plantComponent.sprites[1];
        canBeEaten = true;
        StopCoroutine(GrowPlant(dur));
    }
    #endregion Plant Functions
}
