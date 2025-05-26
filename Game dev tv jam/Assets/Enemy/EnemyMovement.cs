using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Map map;
    public PlayerMovement player;

    public int xPos, yPos;

    public Sprite[] animationFrames;
    public float animationSpeed = 0.1f;

    private SpriteRenderer spriteRenderer;
    private int currentFrame = 0;
    private float frameTimer = 0f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("player").GetComponent<PlayerMovement>();
        map = GameObject.FindGameObjectWithTag("map").GetComponent<Map>();


        xPos = Random.Range(0, map.sizeX);
        yPos = Random.Range(0, map.sizeY);

        transform.localScale = new Vector3(3f, 3f, 1f);

        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = animationFrames[0];
    }

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, map.map[xPos][yPos].transform.position, Time.deltaTime*5f);

        frameTimer += Time.deltaTime;
        if (frameTimer >= animationSpeed)
        {
            currentFrame = (currentFrame + 1) % animationFrames.Length;
            spriteRenderer.sprite = animationFrames[currentFrame];
            frameTimer = 0f;
        }

    }

    public void MoveTowardsPlayer()
    {
        Vector2Int start = new Vector2Int(xPos, yPos);
        Vector2Int target = new Vector2Int(player.xPos, player.yPos);

        List<Vector2Int> path = FindShortestPath(start, target);

        if (path.Count > 1)
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
            new Vector2Int(0, 1),
            new Vector2Int(0, -1),
            new Vector2Int(1, 0),
            new Vector2Int(-1, 0)
        };

        Queue<(Vector2Int, List<Vector2Int>)> queue = new Queue<(Vector2Int, List<Vector2Int>)>();
        HashSet<Vector2Int> checkedTiles = new HashSet<Vector2Int>();

        queue.Enqueue((start, new List<Vector2Int> { start }));
        checkedTiles.Add(start);

        while (queue.Count > 0)
        {
            var (currentPos, path) = queue.Dequeue();

            if (currentPos == target)
                return path;

            foreach (var dir in directions)
            {
                int x = currentPos.x + dir.x;
                int y = currentPos.y + dir.y;

                if (x < 0 || x >= map.sizeX || y < 0 || y >= map.sizeY)
                    continue;

                Vector2Int nextPos = new Vector2Int(x, y);

                if (checkedTiles.Contains(nextPos))
                    continue;

                if (map.map[x][y].GetComponent<Tile>().unWalkable)
                    continue;

                checkedTiles.Add(nextPos);

                List<Vector2Int> newPath = new List<Vector2Int>(path) { nextPos };
                queue.Enqueue((nextPos, newPath));
            }
        }

        return new List<Vector2Int> { start };
    }

    private void GameOver()
    {
        GameObject.Find("Updater").GetComponent<Updater>().Lose();        
    }
}
