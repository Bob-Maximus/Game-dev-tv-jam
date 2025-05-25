using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Map map;

    public int xPos, yPos;
    public bool hasMoved;
    public int visionRadious;

    public Sprite[] walkSprites;
    private SpriteRenderer spriteRenderer;
    private int currentSpriteIndex;
    private float animationTimer;
    public float animationSpeed = 0.15f;

    void Start()
    {
        xPos = map.sizeX / 2;
        yPos = map.sizeY / 2;

        transform.localScale = new Vector3(3f, 3f, 1f);

        transform.position = map.map[xPos][yPos].transform.position;
        map.map[xPos][yPos].GetComponent<Tile>().occupied = true;
        map.map[xPos][yPos].GetComponent<Tile>().occupiedBy = gameObject;

        spriteRenderer = GetComponent<SpriteRenderer>();
        currentSpriteIndex = 0;
        animationTimer = animationSpeed;
        spriteRenderer.sprite = walkSprites[currentSpriteIndex];
    }

    void Update()
    {
        AnimateWalk();
    }

    public void Tick()
    {
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

    void AnimateWalk()
    {
        if (hasMoved)
        {
            animationTimer -= Time.deltaTime;
            if (animationTimer <= 0f)
            {
                animationTimer = animationSpeed;
                currentSpriteIndex = (currentSpriteIndex + 1) % walkSprites.Length;
                spriteRenderer.sprite = walkSprites[currentSpriteIndex];
            }
        }
        else
        {
            currentSpriteIndex = 0;
            spriteRenderer.sprite = walkSprites[currentSpriteIndex];
        }
    }
}
