using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    // Start is called before the first frame update

    public Map map;
    public PlayerMovement player;

    private int xPos, yPos;

    void Start()
    {
        xPos = Random.Range(0, map.sizeX - 1);
        yPos = Random.Range(0, map.sizeY - 1);
    }

    // Update is called once per frame
    void Update()
    {        
        transform.position = map.map[xPos][yPos].transform.position;
    }

    public void Movement()
    {
        if (Random.Range(0, 2) == 0)
        {
            xPos += Random.Range(-1, 2);
        }
        else
        {
            yPos += Random.Range(-1, 2);
        }

        xPos = Mathf.Clamp(xPos, 0, map.sizeX - 1);
        yPos = Mathf.Clamp(yPos, 0, map.sizeY - 1);
    }
}
