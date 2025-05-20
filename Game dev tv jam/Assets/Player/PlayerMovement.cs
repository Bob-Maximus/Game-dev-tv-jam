using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    public Map map;

    public int xPos, yPos;

    private bool hasMoved;

    private float timer = 0;
    public float timeBetweenMoves;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= timeBetweenMoves)
        {
            timer = 0;

            if (hasMoved)
            {
                map.map[xPos][yPos].GetComponent<Tile>().occupied = false;
                map.map[xPos][yPos].GetComponent<Tile>().occupiedBy = null;

                hasMoved = false;
            }

            Movement();

            transform.position = map.map[xPos][yPos].transform.position;

            map.map[xPos][yPos].GetComponent<Tile>().occupied = true;
            map.map[xPos][yPos].GetComponent<Tile>().occupiedBy = gameObject;
        }
    }

    private void Movement()
    {
        if (yPos < map.sizeY - 1 && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)))
        {
            hasMoved = true;
            yPos += 1;
            return; 
        }
        
        if (yPos > 0 && (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)))
        {
            hasMoved = true;
            yPos -= 1;
            return; 
        }

        if (xPos > 0 && (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)))
        {
            hasMoved = true;
            xPos -= 1;
            return; 
        }

        if (xPos < map.sizeX - 1 && (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)))
        {
            hasMoved = true;
            xPos += 1;
            return; 
        }
    }
}
