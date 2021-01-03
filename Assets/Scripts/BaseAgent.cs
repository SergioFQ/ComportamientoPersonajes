﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BaseAgent : MonoBehaviour
{
    public List<RaycastHit> hits;
    public float maxPerceptionL;
    public int maxPerceptionA;
    [SerializeField]private string predatorTag;
    private string[] foodTag = new string[3];

    public NavMeshAgent _agent;

    private NavMeshAgent _evadeTarget, _pursuitTarget;

    protected float _wanderRadius = 5;

    protected enum state
    {
        None,
        Wander,
        Pursuit,
        Evade,
        Eat
    }

    [HideInInspector] public enum tagsAgents
    {
        //dinamicos
        Pez,
        Mosca,
        Renacuajo,
        Rana,
        //estaticos
        HuevosRana,
        HuevosPez,
        Plantas

    }
    [SerializeField] private state currentState;
    public void initObject(NavMeshAgent agente)
    {
        _agent = agente;
    }
    // Start is called before the first frame update
    protected virtual void Start()
    {
        hits = new List<RaycastHit>();
        maxPerceptionA = 15;
        maxPerceptionL = 10;

        _agent = GetComponent<NavMeshAgent>();
        SetTags(gameObject.tag);
    }

    private void FixedUpdate()
    {
        Vision();
    }

    private void SetTags(string tag)
    {
        switch (tag)
        {
            case "Pez":
                predatorTag = tagsAgents.Rana.ToString();
                foodTag[0] = tagsAgents.HuevosRana.ToString();
                foodTag[1] = tagsAgents.Renacuajo.ToString();
                break;
            case "Mosca":
                predatorTag = tagsAgents.Rana.ToString();
                foodTag[0] = null;
                break;
            case "Renacuajo":
                predatorTag = tagsAgents.Pez.ToString();
                foodTag[0] = tagsAgents.Plantas.ToString();
                break;
            case "Rana":
                predatorTag = null;
                foodTag[0] = tagsAgents.Mosca.ToString();
                foodTag[1] = tagsAgents.Pez.ToString();
                foodTag[2] = tagsAgents.Plantas.ToString();
                break;
        }
    }
    protected void Vision()
    {//añadir esfera alrededor
        hits.Clear();
        for (int i = -maxPerceptionA; i < maxPerceptionA; i += 1)
        {
            Vector2 v = Quaternion.Euler(0, 0, i) * transform.forward;
            RaycastHit hit;
            Physics.Raycast(transform.position, v, out hit, maxPerceptionL);
            Debug.DrawRay(transform.position, v * maxPerceptionL, hit.collider!=null?Color.red:Color.green);
            hits.Add(hit);
        }

        ChangeState(state.Wander);
        foreach (RaycastHit h in hits)
        {
            if (h.collider != null)
            {
                if (CollisionDetected(h.collider)) return;
            }
        }
    }

    protected bool CollisionDetected (Collider collider)
    {
        if (gameObject.tag == collider.tag) return false;
        if (predatorTag != null && collider.gameObject.CompareTag(predatorTag))
        {
            NavMeshAgent colAgent = collider.gameObject.GetComponent<NavMeshAgent>();
            if (colAgent == null) return false;
            _evadeTarget = colAgent;
            ChangeState(state.Evade);
            //huir
            return true;
        }
        else
        {
            if (!collider.gameObject.CompareTag(gameObject.tag))
            {
                for (int i = 0; i < foodTag.Length; i++)
                {
                    if (foodTag[i] != null && collider.gameObject.CompareTag(foodTag[i]))
                    {
                        NavMeshAgent colAgent = collider.gameObject.GetComponent<NavMeshAgent>();
                        if (colAgent == null) return false;
                        _pursuitTarget = colAgent;
                        ChangeState(state.Pursuit);
                        //perseguir
                        return false;
                    }
                }
            }
        }
        return false;
        
    }
    protected void ChangeState(state nextState) {//habría que comprobar inicialmente en que estado nos encontramos

        if ((currentState == state.Evade && nextState == state.Wander && _evadeTarget != null) || 
        (currentState == state.Pursuit && nextState == state.Wander && _pursuitTarget != null) || 
        (currentState == state.Evade && nextState == state.Pursuit) || 
        currentState == nextState) return;

        StopCoroutine(currentState.ToString());
        currentState = nextState;
        StartCoroutine(currentState.ToString());
    }

    protected virtual void WanderAction()
    {
        _agent.SetDestination(transform.position + transform.forward*_agent.speed + Random.insideUnitSphere*_wanderRadius);
    }
    protected void PursuitAction()
    {
        if (_pursuitTarget == null || _pursuitTarget.gameObject == null || Vector3.Distance(transform.position, _pursuitTarget.transform.position) > maxPerceptionL) 
        {
            _pursuitTarget = null;
            return;
        }
        Vector3 targetPos = _pursuitTarget.gameObject.transform.position + _pursuitTarget.gameObject.transform.forward * _pursuitTarget.speed;
        _agent.SetDestination(targetPos);
    }
    protected void EvadeAction()
    {
        if (_evadeTarget == null || _evadeTarget.gameObject == null || Vector3.Distance(transform.position, _evadeTarget.transform.position) > maxPerceptionL) 
        {
            _evadeTarget = null;
            return;
        }
        Vector3 targetPos = transform.position - _evadeTarget.transform.position + _evadeTarget.transform.forward * _evadeTarget.speed;
         _agent.SetDestination(targetPos);
    }
    protected virtual void EatAction()
    {

    }
    public virtual void DeadAction()
    {        
        Destroy(gameObject);
    }
    //coroutines

    IEnumerator Wander()
    {
        while (currentState.Equals(state.Wander))
        {
            WanderAction();
            yield return 0;//retornamos algo cualquiera
        }
    }

    IEnumerator Pursuit()
    {
        while (currentState.Equals(state.Pursuit))
        {
            PursuitAction();
            yield return 0;//retornamos algo cualquiera
        }
    }

    IEnumerator Evade()
    {
        while (currentState.Equals(state.Evade))
        {
            EvadeAction();
            yield return 0;//retornamos algo cualquiera
        }
    }
    IEnumerator Eat()
    {
        while (currentState.Equals(state.Eat))
        {
            EatAction();
            yield return 0;//retornamos algo cualquiera
        }
    }
}
