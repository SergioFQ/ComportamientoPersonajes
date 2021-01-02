using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishRoe : MonoBehaviour
{
    // Start is called before the first frame update
    public List<int> dna;
    private System.Random random;
    private float mutationRate;
    private List<int> perfectFish;
    private List<int> worstFish;
    public void Init(System.Random r, float m, List<int> perfect, List<int> worst)
    {
        mutationRate = m;
        random = r;
        perfectFish = perfect;
        worstFish = worst;
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
                dna[i] = random.Next(worstFish[i], perfectFish[i]);
            }
        }
    }
}
