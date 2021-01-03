using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase
{
    public string name;
    public float duration;
    public float probabilityOfSucces;

    public Phase()
    {
        name = "Null Phase";
        duration = 0f;
        probabilityOfSucces = 1f;
    }
    public Phase(string name, float duration, float probabilityOfSucces)
    {
        this.name = name;
        this.duration = duration;
        this.probabilityOfSucces = probabilityOfSucces;
    }
}

public class LifeCycle : MonoBehaviour
{
    enum AgentType 
    {
        RANA,
        PEZ,
        MOSCA,
        PLANTA
    }
    [SerializeField] private AgentType type;
    public List<Phase> fullCycle = new List<Phase>();
    private int stage = 0;

    public void Start()
    {
        CreateFullCycle();
        StartCoroutine(StartPhase(fullCycle[stage]));
    }

    private void CreateFullCycle()
    {
        switch (type) 
        {
            case AgentType.RANA:
                fullCycle.Add(new Phase("Huevo de rana",1,1));
                fullCycle.Add(new Phase("Renacuajo", 1, 1));
                fullCycle.Add(new Phase("Rana", 1, 1));
                break;
            case AgentType.PEZ:
                fullCycle.Add(new Phase("Huevo de pez", 1, 1));
                fullCycle.Add(new Phase("Pez joven", 1, 1));
                fullCycle.Add(new Phase("Pez adulto", 1, 1));
                break;
            case AgentType.MOSCA:
                fullCycle.Add(new Phase("Mosca joven", 1, 1));
                fullCycle.Add(new Phase("Mosca adulta", 1, 1));
                break;
            case AgentType.PLANTA:
                fullCycle.Add(new Phase("Planta", 1, 1));
                break;
        }
    }

    public void NewPhase() 
    {
        if (stage + 1 >= fullCycle.Count) 
        {
            Destroy(gameObject);
            return;
        }
        stage++;
        StartCoroutine(StartPhase(fullCycle[stage]));
    }

    IEnumerator StartPhase(Phase ph)
    {
        yield return new WaitForSeconds(ph.duration);
        //Temas de probabilidad
        NewPhase();
    }

}
