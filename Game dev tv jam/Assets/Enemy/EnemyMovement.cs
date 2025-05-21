using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    // Start is called before the first frame update

    public Map map;
    public PlayerMovement player;

    private bool canMove = false;

    private int xPos, yPos;

    public int ranNum;
    void Start()
    {
        xPos = 5;
        yPos = 5;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Enemy Position: " + xPos + ", " + yPos);
        
        canMove = player.hasMoved;


        if (canMove)
        {
            Movement();
            transform.position = map.map[xPos][yPos].transform.position;
            canMove = false;
            player.hasMoved = false;
        }



    }

    private void Movement()
    {
        ranNum = Random.Range(0, 2);
        if (ranNum == 0)
        {
            xPos += Random.Range(-1, 2);
        }
        else
        {
            yPos += Random.Range(-1, 2);
        }


    }

}
