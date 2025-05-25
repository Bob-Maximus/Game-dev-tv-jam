using UnityEngine;

public class OpenChest : MonoBehaviour
{
    public Map map;
    public PlayerMovement player;

    private Vector2Int chestPos;
    private bool chestOpened = false;

    void Start()
    {
        transform.localScale = new Vector3(3f, 3f, 1f);
        SpawnChest();
    }

    void SpawnChest()
    {
        // Find random walkable tile
        do
        {
            chestPos.x = UnityEngine.Random.Range(0, map.sizeY);
            chestPos.y = UnityEngine.Random.Range(0, map.sizeX);
        } 
        while (map.map[chestPos.x][chestPos.y].GetComponent<Tile>().unWalkable);

        // Position chest on the chosen tile
        transform.position = map.map[chestPos.x][chestPos.y].transform.position;
    }

    void Update()
    {
        if (chestOpened) return;

        // Check if player is on the chest tile
        if (player.xPos == chestPos.x && player.yPos == chestPos.y)
        {
            chestOpened = true;
            OpenTheChest();
        }
    }

    void OpenTheChest()
    {
        Debug.Log("Chest opened! You win!");
        // Add more logic here: show UI, end game, reward player, etc.
    }
}
