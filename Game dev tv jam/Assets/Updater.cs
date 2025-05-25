using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Timeline;
using UnityEngine;

public class Updater : MonoBehaviour
{
    public float tickSpeed;

    public PlayerMovement player;
    public List<EnemyMovement> enemies;

    public GameObject map;

    void Update()
    {

        enemies = new List<EnemyMovement>();
        for (int i = 0; i < GameObject.FindGameObjectsWithTag("Enemy").Length; i++)
        {
            enemies.Add(GameObject.FindGameObjectsWithTag("Enemy")[i].GetComponent<EnemyMovement>());
        }

        if (Input.anyKeyDown)
        {
            UpdateGame();
        }
    }

    public void UpdateGame()
{
    player.Tick();

    foreach (EnemyMovement enemy in enemies)
    {
        enemy.MoveTowardsPlayer();

        // Darken enemy if outside player's vision
        float dist = Vector2.Distance(new Vector2(enemy.xPos, enemy.yPos), new Vector2(player.xPos, player.yPos));
        SpriteRenderer enemySprite = enemy.GetComponent<SpriteRenderer>();

        if (dist > player.visionRadious)
        {
            enemySprite.color = Color.black; // or semi-transparent, e.g. new Color(1,1,1,0.3f)
        }
        else
        {
            enemySprite.color = Color.red; // normal visible color
        }
    }

    for (int xPos = 0; xPos < map.GetComponent<Map>().sizeX; xPos++)
    {
        for (int yPos = 0; yPos < map.GetComponent<Map>().sizeY; yPos++)
        {
            DarkOrNot(xPos, yPos);
        }
    }
}


    public void DarkOrNot(int posX, int posY)
    {
        //float pDist = Mathf.Sqrt(((posX - player.xPos) ^ 2) + ((posY - player.yPos) ^ 2));
        if (player.visionRadious < Vector2.Distance(new Vector2(posX, posY), new Vector2(player.xPos, player.yPos)))
        {
            map.GetComponent<Map>().map[posX][posY].GetComponent<Tile>().dark = true;
        }
        else
        {
            map.GetComponent<Map>().map[posX][posY].GetComponent<Tile>().dark = false;
        }
    }
    
}
