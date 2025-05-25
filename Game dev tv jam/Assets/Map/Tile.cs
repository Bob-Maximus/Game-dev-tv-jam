using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool unWalkable;
    public bool occupied;

    private SpriteRenderer sprite;

    public bool dark;

    public GameObject occupiedBy;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (dark)
        {
            sprite.color = Color.black;
        }
        else
        {
            sprite.color = Color.white;
      
        }
    }

    public bool seen;
}
