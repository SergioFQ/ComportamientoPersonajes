using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roe : MonoBehaviour
{
    // Start is called before the first frame update
    public List<int> dna;
    private System.Random random;
    private float mutationRate;
    private List<int> perfectInd;
    private List<int> worstInd;
    public string type;

    public void Init(System.Random r, float m, List<int> perfect, List<int> worst, string t)
    {
        mutationRate = m;
        random = r;
        perfectInd = perfect;
        worstInd = worst;
        type = t; // Como los huevos de pez y rana tienen los mismos atributos con type se controlaria a que especie pertenecen para luego crecer
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Mutate()
    {
        for (int i = 0; i < dna.Count; i++)
        {
            if (random.NextDouble() < mutationRate)
            {
                dna[i] = random.Next(worstInd[i], perfectInd[i]);
            }
        }
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
