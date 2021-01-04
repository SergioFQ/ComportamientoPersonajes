using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly : BaseAgent
{

    int lifespan;
    int maxLifespan = 10;
    int minLifespan = 5;
    public void Init()
    {
        lifespan = Random.Range(minLifespan, maxLifespan);
        
    }

    /*// Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }*/
}
