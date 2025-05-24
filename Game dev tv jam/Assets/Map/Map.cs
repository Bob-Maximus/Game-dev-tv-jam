using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Map : MonoBehaviour
{
    public GameObject tile;
    //public List<GameObject> xPos;
    public List<List<GameObject>> map = new List<List<GameObject>>();

    public int sizeX, sizeY;

    public float messyness;
    public float messynessAdd;


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

        for (int xPos = 0; xPos < map.Count; xPos++)
        {
            for (int yPos = 0; yPos < map.Count; yPos++)
            {
                int numOfUnwalk = 0;

                for (int i = 0; i < 8; i++)
                {
                    if (sizeX > xPos + i - 1 && xPos + i - 1 > 0 && sizeY > yPos + 1 && yPos + 1 > 0 && i < 4 && map[xPos + i - 1][yPos + 1].GetComponent<Tile>().unWalkable)
                    {
                        numOfUnwalk += 1;
                    }
                    else if (sizeX > xPos + (2 * i) - 11 && xPos + (2 * i) - 11 > 0 &&  i < 6 && map[xPos + (2 * i) - 11][yPos].GetComponent<Tile>().unWalkable)
                    {
                        numOfUnwalk += 1;
                    }
                    else if (sizeX > xPos + i - 8 && xPos + i - 8 > 0 && sizeY > yPos - 1 && yPos - 1 > 0 && i > 5 && map[xPos + i - 8][yPos - 1].GetComponent<Tile>().unWalkable)
                    {
                        numOfUnwalk += 1;
                    }
                }

                float walkableNum = UnityEngine.Random.Range(-1, (numOfUnwalk * messyness) + messynessAdd);
                if (walkableNum < 0)
                {
                    map[xPos][yPos].GetComponent<Tile>().unWalkable = true;
                    map[xPos][yPos].GetComponent<SpriteRenderer>().color = Color.blue;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
