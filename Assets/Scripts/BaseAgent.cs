using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAgent : MonoBehaviour
{
    public List<RaycastHit2D> hits;
    public float maxPerceptionL;
    public int maxPerceptionA;

    // Start is called before the first frame update
    void Start()
    {
        hits = new List<RaycastHit2D>();
        maxPerceptionA = 15;
        maxPerceptionL = 10;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        hits.Clear();
        for(int i = - maxPerceptionA ; i < maxPerceptionA; i += 4)
        {
            Vector2 v = Quaternion.Euler(0, 0, i) * transform.up;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, v, maxPerceptionL);
            Debug.DrawRay(transform.position, v * maxPerceptionL);
            hits.Add(hit);
        }

        foreach(RaycastHit2D h in hits)
        {
            if(h.collider != null)
            {
                CollisionDetected(h.collider);
            }
        }

    }

    // Gestionado por cada agente
    protected void CollisionDetected (Collider2D collider)
    {

    }
}
