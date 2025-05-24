using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    public Map map;

    public int xPos, yPos;

    public bool hasMoved;

    public int visionRadious;

    //private float timer = 0;
    //public float timeBetweenMoves;

    public void Tick()
    {
        /*
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) ||
            Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow) ||
            Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow) ||
            Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            timer = timeBetweenMoves; // Make it act like cooldown has passed
        }
        */

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

    public void Movement()
    {
        int nXPos = xPos;
        int nYPos = yPos;

        if (yPos < map.sizeY - 1 && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)))
        {
            hasMoved = true;
            nYPos += 1;
        }

        if (yPos > 0 && (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)))
        {
            hasMoved = true;
            nYPos -= 1;
        }

        if (xPos > 0 && (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)))
        {
            hasMoved = true;
            nXPos -= 1;
        }

        if (xPos < map.sizeX - 1 && (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)))
        {
            hasMoved = true;
            nXPos += 1;
        }

        if (!map.map[nXPos][nYPos].GetComponent<Tile>().unWalkable)
        {
            xPos = nXPos;
            yPos = nYPos;
        }
    }
}
