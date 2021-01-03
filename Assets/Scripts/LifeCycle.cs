using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LifeCycle : MonoBehaviour
{
    [SerializeField] private GameObject agent;
    private BaseAgent baseAgent;
    private float duration = 1f;
    private float probabilityOfSucces;
    private float probabilityOfDead;
    private float rand;

    public void Start()
    {
        rand = Random.value;
        probabilityOfSucces = rand;
        //Debug.Log("S: "+ probabilityOfSucces);
        rand = Random.Range(0.50f, 1);
        probabilityOfDead = rand;
        //Debug.Log("D: " + probabilityOfDead);
        baseAgent = GetComponent<BaseAgent>();
        //CreateFullCycle();
        if (!gameObject.CompareTag("HuevosRana") && !gameObject.CompareTag("HuevosPez") && !gameObject.CompareTag("Player")) return;
        if (agent != null)
        {

            StartCoroutine(StartPhase(duration));
        }
        StartCoroutine(DeadAgent(duration));
    }

    public void initCycle(float _duration, float _succes, float _dead)
    {
        duration = _duration;
        probabilityOfSucces = _succes;
        probabilityOfDead = _dead;
        if (gameObject.CompareTag("Rana") || gameObject.CompareTag("Pez"))
        {
            //corrutina de muerte
            StartCoroutine(DeadAgent(duration));

        }
        else
        {
            StartCoroutine(StartPhase(duration));
            //corrutina de muerte
            StartCoroutine(DeadAgent(duration));

        }
    }
    public void NewPhase()
    {
        if (agent != null)
        {
            GameObject obj = Instantiate(agent, new Vector2(this.transform.position.x, this.transform.position.y), Quaternion.identity);

            obj.GetComponent<BaseAgent>().initObject(baseAgent._agent);
        
        obj.GetComponent<LifeCycle>().initCycle(duration, probabilityOfSucces, probabilityOfDead);

        Destroy(gameObject);
        }
    }

    IEnumerator StartPhase(float dur)
    {
        yield return new WaitForSeconds(dur);
        rand = Random.value;
        if (rand <= probabilityOfSucces)
        {
            StopCoroutine(DeadAgent(dur));
            StopCoroutine(StartPhase(duration));
            NewPhase();
        }
        else
        {
            StopCoroutine(StartPhase(duration));
            StartCoroutine(StartPhase(duration));
        }
    }

    IEnumerator DeadAgent(float dur)
    {
        yield return new WaitForSeconds(dur);
        rand = Random.value;
        if (rand<=probabilityOfDead)
        {
            if (agent != null)
            {
                StopCoroutine(StartPhase(duration));
            }
            StopCoroutine(DeadAgent(duration));
            baseAgent.DeadAction();
        }
        else
        {
            StopCoroutine(DeadAgent(duration));
            StartCoroutine(DeadAgent(duration));

        }
    }
}
