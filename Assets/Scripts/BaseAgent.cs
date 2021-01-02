using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BaseAgent : MonoBehaviour
{
    public List<RaycastHit2D> hits;
    public float maxPerceptionL;
    public int maxPerceptionA;

    private NavMeshAgent _agent;

    protected enum state
    {
        Seek,
        Wander,
        Pursuit,
        Evade,
        Avoid,
        Eat,
        Dead
    }
    state currentState;

    // Start is called before the first frame update
    void Start()
    {
        hits = new List<RaycastHit2D>();
        maxPerceptionA = 15;
        maxPerceptionL = 10;

        _agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Vision();
    }

    protected void Vision()
    {
        hits.Clear();
        for (int i = -maxPerceptionA; i < maxPerceptionA; i += 4)
        {
            Vector2 v = Quaternion.Euler(0, 0, i) * transform.forward;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, v, maxPerceptionL);
            Debug.DrawRay(transform.position, v * maxPerceptionL);
            hits.Add(hit);
        }

        foreach (RaycastHit2D h in hits)
        {
            if (h.collider != null)
            {
                CollisionDetected(h.collider);
            }
        }
    }

    // Gestionado por cada agente
    protected void CollisionDetected (Collider2D collider)
    {

    }
    protected void ChangeState(state nextState) {
        currentState = nextState;
        StartCoroutine(currentState.ToString());
    }
    protected virtual void SeekAction()
    {

    }

    protected virtual void WanderAction()
    {

    }
    protected virtual void PursuitAction()
    {

    }
    protected virtual void EvadeAction()
    {

    }
    protected virtual void AvoidAction()
    {

    }
    protected virtual void EatAction()
    {

    }
    protected virtual void DeadAction()
    {

    }
    //coroutines
    IEnumerator Seek()
    {
        while (currentState.Equals(state.Seek))
        {
            SeekAction();
            yield return 0;//retornamos algo cualquiera
        }
    }

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

    IEnumerator Avoid()
    {
        while (currentState.Equals(state.Avoid))
        {
            AvoidAction();
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
    IEnumerator Dead()
    {
        while (currentState.Equals(state.Dead))
        {
            DeadAction();
            yield return 0;//retornamos algo cualquiera
        }
    }
}
