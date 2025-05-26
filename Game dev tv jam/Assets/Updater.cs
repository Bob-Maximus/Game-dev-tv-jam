using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Updater : MonoBehaviour
{
    public float tickSpeed;

    public PlayerMovement player;
    public List<EnemyMovement> enemies;
    public GameObject map;

    public GameObject chest;  // Assign in inspector or find dynamically

    void Update()
    {
        // Refresh enemy list each frame
        enemies = new List<EnemyMovement>();
        var enemyObjects = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < enemyObjects.Length; i++)
        {
            enemies.Add(enemyObjects[i].GetComponent<EnemyMovement>());
        }

        if (Input.anyKeyDown)
        {
            UpdateGame();
        }
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void GoBack()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void UpdateGame()
    {
        player.Tick();

        // Update enemies and darken if outside vision
        foreach (EnemyMovement enemy in enemies)
        {
            enemy.MoveTowardsPlayer();

            float dist = Vector2.Distance(new Vector2(enemy.xPos, enemy.yPos), new Vector2(player.xPos, player.yPos));
            SpriteRenderer enemySprite = enemy.GetComponent<SpriteRenderer>();

            if (dist > player.visionRadious)
                enemySprite.color = Color.black;
            else
                enemySprite.color = Color.red;
        }

        // Darken tiles based on distance from player
        for (int xPos = 0; xPos < map.GetComponent<Map>().sizeX; xPos++)
        {
            for (int yPos = 0; yPos < map.GetComponent<Map>().sizeY; yPos++)
            {
                DarkOrNot(xPos, yPos);
            }
        }

        // Darken chest based on distance from player
        if (chest != null)
        {
            float chestDist = Vector2.Distance(new Vector2(chest.transform.position.x, chest.transform.position.y), new Vector2(player.xPos, player.yPos));
            SpriteRenderer chestSprite = chest.GetComponent<SpriteRenderer>();

            if (chestDist > player.visionRadious)
                chestSprite.color = Color.black;
            else
                chestSprite.color = Color.white;
        }
    }

    public void DarkOrNot(int posX, int posY)
    {
        float distance = Vector2.Distance(new Vector2(posX, posY), new Vector2(player.xPos, player.yPos));

        if (distance > player.visionRadious)
        {
            map.GetComponent<Map>().map[posX][posY].GetComponent<Tile>().dark = true;
        }
        else
        {
            map.GetComponent<Map>().map[posX][posY].GetComponent<Tile>().dark = false;
        }
    }


    public GameObject winUi;
    public void Win()
    {
        winUi.SetActive(true);
    }
    
    public GameObject loseUi;

    public void Lose()
    {
        loseUi.SetActive(true);
    }
}
