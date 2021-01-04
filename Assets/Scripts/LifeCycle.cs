using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    private float timeOfLive;
    private Plant plantComponent;
    public List<float> dna;
    public List<float> perfectDna;
    public List<float> worstDna;

    public void Start()
    {
        
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
        if (!gameObject.CompareTag("HuevosRana") && !gameObject.CompareTag("HuevosPez") && !gameObject.CompareTag("Player")) return;
        if (agent != null)
        {
            StartCoroutine(StartPhase(dna[5]));
        }
        timeOfLive = 0.0f;
        StartCoroutine(DeadAgent(dna[5]));
    }

    private void Update()
    {
        timeOfLive += Time.deltaTime;
    }

    public void initCycle(List<float> d/*float _duration, float _succes, float _dead*/, float _timeLived, List<float> worst, List<float> perfect)
    {
        dna = d;
        worstDna = worst;
        perfectDna = perfect;
        //probabilityOfSucces = _succes;
        //probabilityOfDead = _dead;
        timeOfLive = _timeLived;
        if (gameObject.CompareTag("Rana") || gameObject.CompareTag("Pez"))
        {
            //corrutina de muerte
            StartCoroutine(DeadAgent(dna[5]));
        }
        else
        {
            StartCoroutine(StartPhase(dna[5]));
            //corrutina de muerte
            StartCoroutine(DeadAgent(dna[5]));
        }
    }
    public void NewPhase(List<float> worst, List<float> perfect)
    {
        if (agent != null)
        {
            GameObject obj = Instantiate(agent, new Vector2(this.transform.position.x, this.transform.position.y), Quaternion.identity);

            obj.GetComponent<BaseAgent>().initObject(baseAgent._agent);
        


            obj.GetComponent<LifeCycle>().initCycle(dna, timeOfLive, worst, perfect);

        Destroy(gameObject);
        }
    }

    IEnumerator StartPhase(float dur)
    {
        yield return new WaitForSeconds(dur);
        rand = Random.Range(worstDna[9], perfectDna[9]);
        if (rand <= dna[9])
        {
            StopCoroutine(DeadAgent(dur));
            StopCoroutine(StartPhase(dna[5]));
            NewPhase(worstDna, perfectDna);
        }
        else
        {
            StopCoroutine(StartPhase(dna[5]));
            StartCoroutine(StartPhase(dna[5]));
        }
    }

    IEnumerator DeadAgent(float dur)
    {
        yield return new WaitForSeconds(dur);
        rand = Random.Range(worstDna[9], perfectDna[9]);
        if (rand>dna[9])
        {
            if (agent != null)
            {
                StopCoroutine(StartPhase(dna[5]));
            }
            StopCoroutine(DeadAgent(dna[5]));
            baseAgent.DeadAction();
        }
        else
        {
            StopCoroutine(DeadAgent(dna[5]));
            StartCoroutine(DeadAgent(dna[5]));

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
