using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenacuajosInstantiator : MonoBehaviour
{

    public GameObject renacuajoPrefab;

    //Campo para indicar el número de renacuajos del flock
    [SerializeField]
    public int numeroRenacuajos;

    // Start is called before the first frame update
    private void Start()
    {
        //Instancia un renacuajo por cada unidad indicada en el campo
        for (int i = 0; i < numeroRenacuajos; i++)
        {
            Instantiate(renacuajoPrefab, Vector3.zero, Quaternion.identity);
        }
    }
}
