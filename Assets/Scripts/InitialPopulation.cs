using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialPopulation : MonoBehaviour
{
    // Moscas
    private List<Fly> flyPopulation;
    public int flyPopulationSize;

    // Ranas
   

    // Plantas
    public int plantPopulationSize;

    // Start is called before the first frame update
    void Start()
    {
        GameObject g;
        

        for(int i = 0; i < flyPopulationSize; i++)
        {
            g = new GameObject();
            g.AddComponent<BaseAgent>();
            g.AddComponent<Fly>();
            Fly fly = g.GetComponent<Fly>();

            fly.Init();
            flyPopulation.Add(fly);
        }
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
