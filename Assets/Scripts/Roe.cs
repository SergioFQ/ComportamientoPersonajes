using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

/*
 * Roe: clase de los objetos huevas de rana y huevas de pez.
 * Esta clase se encargará de inicializar los valores de ADN 
 * de las diferentes huevas, de su mutación y su destrucción 
 * una vez mueran.
 */
public class Roe : MonoBehaviour
{
    public List<float> dna;
    public List<float> worstDna;
    public List<float> perfectDna;
    public string type;

    #region Functions

    /* 
     * Init: Método encargado de inicializar el ADN de las huevas de peces y ranas. Reciben también
     * los ADN de los mejores y peores valores posibles
     */
    public void Init(List<float> d, List<float> perfect, List<float> worst)
    {
        dna = d;
        perfectDna = perfect;
        worstDna = worst;
    }
    /*
     * Mutate: Método encargado de las mutaciones. Si la se cumple la condición, el nuevo hijo tendrá 
     * características de su ADN diferentes a las de sus progenitores. Las nuevas características vendrán
     * dadas por los valores peores y mejores de su especie.
     */
    public void Mutate()
    {
        for (int i = 0; i < dna.Count - 1; i++)
        {
            if (Random.value < dna[10])
            {
                dna[i] = Random.Range(worstDna[i], perfectDna[i]);
            }
        }
    }
    /*
     * DeadRoe: Método encargado de destruir el objeto hueva cuando sea comida o si no llega a crecer. También se encarga de
     * quitar el foco de la minicamera de la hueva si esta fuese la que estábamos observando. 
     */
    public void DeadRoe()
    {
        if (gameObject.GetComponent<LifeCycle>().isTarget) gameObject.GetComponent<Clickeable>().Dead();
        Destroy(gameObject);
    }
    #endregion Functions
}