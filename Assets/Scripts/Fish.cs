using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Fish: clase de los peces que hereda de BaseAgent que guarda el valor juvenile. Este valor se 
 * utilizará para saber si las ranas pueden o no comerse al pez (solo los comerán si son peces jóvenes).
 */
public class Fish : BaseAgent
{
    public bool juvenile;
}
