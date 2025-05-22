using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Updater : MonoBehaviour
{
    public float tickSpeed;

    public PlayerMovement player;
    public List<EnemyMovement> enemies;

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
            enemy.Movement();
        }
    }
}
