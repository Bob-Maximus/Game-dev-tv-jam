using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Map map;
    public PlayerMovement player;

    public int xPos, yPos;

    void Start()
    {
        xPos = Random.Range(0, map.sizeX);
        yPos = Random.Range(0, map.sizeY);
    }

    void Update()
    {
        transform.position = map.map[xPos][yPos].transform.position;
    }

    public void MoveTowardsPlayer()
    {
        Vector2Int start = new Vector2Int(xPos, yPos);
        Vector2Int target = new Vector2Int(player.xPos, player.yPos);

        List<Vector2Int> path = FindShortestPath(start, target);

        if (path.Count > 1) // path[0] is current position, path[1] is next step
        {
            xPos = path[1].x;
            yPos = path[1].y;

            if (xPos == player.xPos && yPos == player.yPos)
            {
                GameOver();
            }
        }
    }

    private List<Vector2Int> FindShortestPath(Vector2Int start, Vector2Int target)
    {
        Vector2Int[] directions = {
            new Vector2Int(0, 1),  // down
            new Vector2Int(0, -1), // up
            new Vector2Int(1, 0),  // right
            new Vector2Int(-1, 0)  // left
        };

        Queue<(Vector2Int, List<Vector2Int>)> queue = new Queue<(Vector2Int, List<Vector2Int>)>();
        HashSet<Vector2Int> checkedTiles = new HashSet<Vector2Int>();

        queue.Enqueue((start, new List<Vector2Int> { start }));
        checkedTiles.Add(start);

        while (queue.Count > 0)
        {
            var (currentPos, path) = queue.Dequeue();

            if (currentPos == target)
            {
                return path;
            }

            foreach (var dir in directions)
            {
                int x = currentPos.x + dir.x;
                int y = currentPos.y + dir.y;

                // Check bounds
                if (x < 0 || x >= map.sizeX || y < 0 || y >= map.sizeY)
                    continue;

                Vector2Int nextPos = new Vector2Int(x, y);

                if (checkedTiles.Contains(nextPos))
                    continue;

                if (map.map[x][y].GetComponent<Tile>().unWalkable)
                    continue;

                // Valid next position
                checkedTiles.Add(nextPos);

                List<Vector2Int> newPath = new List<Vector2Int>(path) { nextPos };
                queue.Enqueue((nextPos, newPath));
            }
        }

        // If stuck, stay in place
        return new List<Vector2Int> { start };
    }


    private void GameOver()
    {
        Debug.Log("Game Over! Enemy reached the player.");
        
    }
}
