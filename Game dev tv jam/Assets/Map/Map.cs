using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Map : MonoBehaviour
{
    public GameObject tile;
    //public List<GameObject> xPos;
    public List<List<GameObject>> map = new List<List<GameObject>>();

    public int sizeX, sizeY;


    // Start is called before the first frame update
    void Start()
    {
        for (int x = 0; x < sizeY; x++)
        {
            List<GameObject> yPos = new List<GameObject>();

            for (int y = 0; y < sizeX; y++)
            {
                var square = Instantiate(tile, new Vector3(x, y, 0), quaternion.Euler(0, 0, 0));

                square.name = "tile (" + x + ", " + y + ")";
                square.transform.parent = gameObject.transform;

                yPos.Add(square);
            }

            map.Add(yPos);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
