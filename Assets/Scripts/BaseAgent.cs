using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAgent : MonoBehaviour
{
    public List<RaycastHit2D> hits;
    public float maxPerceptionL;
    public int maxPerceptionA;
    private string predatorTag;
    private string[] foodTag = new string[3];

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
    state currentState;

    // Start is called before the first frame update
    void Start()
    {
        hits = new List<RaycastHit2D>();
        maxPerceptionA = 15;
        maxPerceptionL = 10;
        SetTags(gameObject.tag);
    }

    // Update is called once per frame
    void Update()
    {
        
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
        for (int i = -maxPerceptionA; i < maxPerceptionA; i += 4)
        {
            Vector2 v = Quaternion.Euler(0, 0, i) * transform.up;
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

    protected void CollisionDetected (Collider2D collider)
    {
        if (collider.gameObject.CompareTag(predatorTag))
        {
            ChangeState(state.Evade);
            //huir
        }
        else
        {
            if (!collider.gameObject.CompareTag(gameObject.tag))
            {
                for (int i = 0; i < foodTag.Length; i++)
                {
                    if (collider.gameObject.CompareTag(foodTag[i]))
                    {
                        ChangeState(state.Pursuit);
                        //perseguir
                        return;
                    }
                }
                ChangeState(state.Wander);
            }
        }

        
    }
    protected void ChangeState(state nextState) {//habría que comprobar inicialmente en que estado nos encontramos
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
