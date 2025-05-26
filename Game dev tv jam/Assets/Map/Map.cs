using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Map : MonoBehaviour
{
    public PlayerMovement player;

    public int numOfEnemies;
    public GameObject enemy;

    public GameObject tile;
    public List<List<GameObject>> map = new List<List<GameObject>>();

    public int sizeX, sizeY;

    public Sprite[] tileSprites;    // 0..2 walkable tiles, 3 = unwalkable tile sprite
    public Sprite chestSprite;      // assign chest sprite in inspector

    private GameObject chest;       // reference to spawned chest

    public int chestX { get; private set; }
    public int chestY { get; private set; }

    public void Awake()
    {
        //sizeX = UnityEngine.Random.Range(30, 100);
        //sizeY = UnityEngine.Random.Range(30, 100);

        var tiles = GameObject.FindGameObjectsWithTag("Tile");
        for (int i = 0; i < tiles.Length; i++)
        {
            Destroy(tiles[i]);
        }

        for (int x = 0; x < sizeY; x++)
        {
            List<GameObject> row = new List<GameObject>();

            for (int y = 0; y < sizeX; y++)
            {
                var square = Instantiate(tile, new Vector3(x, y, 0), quaternion.identity);
                square.name = "tile (" + x + ", " + y + ")";
                square.transform.parent = gameObject.transform;

                square.GetComponent<Tile>().unWalkable = false;

                SpriteRenderer sr = square.GetComponent<SpriteRenderer>();
                sr.sprite = tileSprites[UnityEngine.Random.Range(0, 3)];
                sr.color = Color.white;

                row.Add(square);
            }

            map.Add(row);
        }

        for (int x = 0; x < sizeY; x++)
        {
            for (int y = 0; y < sizeX; y++)
            {
                int neighbors = CountUnwalkableNeighbors(x, y);

                if (neighbors >= 4)
                    continue;

                bool makeUnwalkable = false;

                if (neighbors == 0)
                    makeUnwalkable = UnityEngine.Random.value < 0.15f;
                else if (neighbors <= 3)
                    makeUnwalkable = UnityEngine.Random.value < 0.2f;

                if (makeUnwalkable)
                {
                    map[x][y].GetComponent<Tile>().unWalkable = true;
                    map[x][y].GetComponent<SpriteRenderer>().sprite = tileSprites[3]; // unwalkable tile sprite
                    map[x][y].transform.localScale = new Vector3(0.89f, 0.89f, 1f);
                }

                if (CountUnwalkableNeighbors(x, y) == 8)
                {
                    map[x][y].GetComponent<Tile>().unWalkable = false;
                    map[x][y].GetComponent<SpriteRenderer>().color = Color.white;
                }
            }
        }

        for (int i = 0; i < numOfEnemies; i++)
        {
            Instantiate(enemy);
        }

        SpawnChest();
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

    public Sprite chestskin;
    void SpawnChest()
    {
        int x, y;

        // Find random walkable and unoccupied tile
        do
        {
            x = UnityEngine.Random.Range(0, sizeY);
            y = UnityEngine.Random.Range(0, sizeX);

        }
        while (map[x][y].GetComponent<Tile>().unWalkable || map[x][y].GetComponent<Tile>().occupied);

        chestX = x;
        chestY = y;

        chest = new GameObject("Chest");
        chest.tag = "chest";
        chest.transform.position = map[x][y].transform.position;
        chest.transform.localScale = new Vector3(5, 5, 5);

        SpriteRenderer sr = chest.AddComponent<SpriteRenderer>();
        sr.sprite = chestSprite;
        sr.sortingOrder = 1;

        OpenChest chestS = chest.AddComponent<OpenChest>();
        chestS.player = GameObject.FindGameObjectWithTag("player").GetComponent<PlayerMovement>();
        chestS.map = GameObject.FindGameObjectWithTag("map").GetComponent<Map>();

        map[x][y].GetComponent<Tile>().occupied = true;
        map[x][y].GetComponent<Tile>().occupiedBy = chest;
    }
}
