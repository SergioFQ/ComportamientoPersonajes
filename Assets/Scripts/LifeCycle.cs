using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class LifeCycle : MonoBehaviour
{
    [SerializeField] private GameObject agent;
    public bool canBeEaten;
    private BaseAgent baseAgent;
    //private float duration = 1f;
    //private float probabilityOfSucces;
    //private float probabilityOfDead;
    private float rand;
    public float timeOfLive;
    [SerializeField]private float timeInPhase;
    private Plant plantComponent;
    public List<float> dna;
    public List<float> perfectDna;
    public List<float> worstDna;
    public MiniCameraCotroller miniCameraController;
    public bool isTarget;
    
    public void Start()
    {
        miniCameraController = FindObjectOfType<MiniCameraCotroller>();
        plantComponent = gameObject.GetComponent<Plant>();
        if (gameObject.CompareTag("Plantas")) { StartCoroutine(GrowPlant(plantComponent.plantGrowTime)); timeOfLive = 0.0f; return;  }
        rand = Random.value;
        //probabilityOfSucces = rand;
        //Debug.Log("S: "+ probabilityOfSucces);
        rand = Random.Range(0.50f, 1);
       // probabilityOfDead = rand;
        //Debug.Log("D: " + probabilityOfDead);
        baseAgent = GetComponent<BaseAgent>();
        //CreateFullCycle();
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
        timeOfLive = 0.0f;
        timeInPhase = 0.0f;
        //StartCoroutine(StartPhase());
    }

    private void Update()
    {
        timeOfLive += Time.deltaTime;
        timeInPhase += Time.deltaTime;
        if (!gameObject.CompareTag("Plantas")) StartPhase();
    }

    public void initCycle(List<float> d/*float _duration, float _succes, float _dead*/, float _timeLived, List<float> worst, List<float> perfect)
    {
        dna = d;
        worstDna = worst;
        perfectDna = perfect;
        //probabilityOfSucces = _succes;
        //probabilityOfDead = _dead;
        timeOfLive = _timeLived;
        timeInPhase = 0.0f;
        //StartCoroutine(StartPhase());
    }
    public void NewPhase()
    {
        if (agent != null)
        {
            GameObject obj = Instantiate(agent, new Vector2(this.transform.position.x, this.transform.position.y), Quaternion.identity);
            BaseAgent objAgent = obj.GetComponent<BaseAgent>();
            if (baseAgent!=null)
            {
                objAgent.Init(dna, perfectDna, worstDna);
                objAgent.initObject(baseAgent._agent);
                if (isTarget) obj.GetComponent<Clickeable>().Evolve();
                baseAgent.DeadAction();
            }
            else
            {
                objAgent.Init(dna, perfectDna, worstDna);
                objAgent.initObject(gameObject.GetComponent<NavMeshAgent>());
                if (isTarget) obj.GetComponent<Clickeable>().Evolve();
                Destroy(gameObject);
            }
            obj.GetComponent<LifeCycle>().initCycle(dna, timeOfLive, worstDna, perfectDna);
        }
    }

    void StartPhase()
    {
        if (timeOfLive >= dna[8])
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

    #region Plant things

    public void EatPlant() 
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = plantComponent.sprites[0];
        canBeEaten = false;
        StartCoroutine(GrowPlant(plantComponent.plantGrowTime));
    }
    IEnumerator GrowPlant(float dur)
    {
        yield return new WaitForSeconds(dur);
        gameObject.GetComponent<SpriteRenderer>().sprite = plantComponent.sprites[1];
        canBeEaten = true;
        StopCoroutine(GrowPlant(dur));
    }
    #endregion Plant things
}
