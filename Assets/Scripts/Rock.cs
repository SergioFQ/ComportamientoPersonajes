using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Rock: clase encargada en asignar de manera aleatoria el sprite de las rocas. 
 */
public class Rock : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite[] sprites;

    #region Unity Functions
    /*
     * Start: asigna de forma aleatoria el sprite de la roca entre los diferentes
     * sprites de rocas.
     */
    void Start()
    {
        spriteRenderer.sprite = sprites[Random.Range(0,sprites.Length)];
    }
    #endregion Unity Functions

}
