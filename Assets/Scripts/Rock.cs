using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite[] sprites;
    
    void Start()
    {
        spriteRenderer.sprite = sprites[Random.Range(0,sprites.Length)];
    }

}
