using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Map : MonoBehaviour
{
    public GameObject tile;
    public List<List<GameObject>> map = new List<List<GameObject>>();

    public int sizeX, sizeY;

    void Start()
    {
        // Create grid of tiles
        for (int x = 0; x < sizeY; x++)
        {
            List<GameObject> row = new List<GameObject>();

            for (int y = 0; y < sizeX; y++)
            {
                var square = Instantiate(tile, new Vector3(x, y, 0), quaternion.Euler(0, 0, 0));
                square.name = "tile (" + x + ", " + y + ")";
                square.transform.parent = gameObject.transform;

                square.GetComponent<Tile>().unWalkable = false;
                square.GetComponent<SpriteRenderer>().color = Color.white;

                row.Add(square);
            }

            map.Add(row);
        }

        // Now assign unwalkable tiles with clustering but preventing full blockage
        for (int x = 0; x < sizeY; x++)
        {
            for (int y = 0; y < sizeX; y++)
            {
                int neighbors = CountUnwalkableNeighbors(x, y);

                // Avoid too dense: skip if surrounded by many unwalkable tiles
                if (neighbors >= 4)
                {
                    continue; // skip to prevent big clusters or isolated blocked tiles
                }

                bool makeUnwalkable = false;

                if (neighbors == 0)
                {
                    // Chance to start new cluster — lower to reduce density
                    makeUnwalkable = UnityEngine.Random.value < 0.15f; // 15%
                }
                else if (neighbors <= 3)
                {
                    // Moderate chance to grow cluster
                    makeUnwalkable = UnityEngine.Random.value < 0.2f; // 50%
                }

                if (makeUnwalkable)
                {
                    map[x][y].GetComponent<Tile>().unWalkable = true;
                    map[x][y].GetComponent<SpriteRenderer>().color = Color.blue;

                    // Check if tile becomes isolated (all neighbors unwalkable)
                    if (CountUnwalkableNeighbors(x, y) == 8)
                    {
                        // Undo it to avoid blocking player
                        map[x][y].GetComponent<Tile>().unWalkable = false;
                        map[x][y].GetComponent<SpriteRenderer>().color = Color.white;
                    }
                }
            }
        }
    }

    int CountUnwalkableNeighbors(int x, int y)
    {
        int count = 0;

        for (int dx = -1; dx <= 1; dx++)
        {
            for (int dy = -1; dy <= 1; dy++)
            {
                if (dx == 0 && dy == 0) continue;

                int nx = x + dx;
                int ny = y + dy;

                if (nx >= 0 && nx < sizeY && ny >= 0 && ny < sizeX)
                {
                    if (map[nx][ny].GetComponent<Tile>().unWalkable)
                        count++;
                }
            }
        }

        return count;
    }

    void Update()
    {
        
    }
}
