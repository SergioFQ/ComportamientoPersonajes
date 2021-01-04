using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Roe : MonoBehaviour
{
    // Start is called before the first frame update
    public string type;
    public List<float> dna;
    public List<float> worstDna;
    public List<float> perfectDna;

    public void Init(List<float> d, List<float> perfect, List<float> worst, string t)
    {
        dna = d;
        perfectDna = perfect;
        worstDna = worst;
        type = t; // Como los huevos de pez y rana tienen los mismos atributos con type se controlaria a que especie pertenecen para luego crecer
    }

   
    public void Mutate()
    {
        for (int i = 0; i < dna.Count-1; i++)
        {
            if (Random.value < dna[10])
            {
                dna[i] = Random.Range(worstDna[i], perfectDna[i]);
            }
        }
    }

    public void DeadRoe()
    {
        if (gameObject.GetComponent<LifeCycle>().isTarget) gameObject.GetComponent<Clickeable>().Dead();
        Destroy(gameObject);
    }
    /*
    public void Grow()
    {
        GameObject g = new GameObject();
        g.AddComponent<Fish>();
        g.AddComponent<BaseAgent>();
        Fish fish = g.GetComponent<Fish>();

        fish.Init(dna,random, mutationRate, perfectFish, worstFish);
        Destroy(this.gameObject);

    }*/
}
