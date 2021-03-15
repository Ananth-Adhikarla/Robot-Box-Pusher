using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
    [Header("Tilemaps")]
    [Space]
    [SerializeField] private Tilemap wallTilemap = null;
    [Space]
    [SerializeField] private Tilemap floorTilemap = null;
    [Space]
    [SerializeField] private Tilemap finishTilemap = null;
    [Space]
    [SerializeField] private Tilemap crateTilemap = null;
    [Space]
    [SerializeField] private TileBase woodenBox = null;
    [Space]
    [SerializeField] private TileBase woodenBoxFinish = null;
    List<Vector3Int> finishSpot = new List<Vector3Int>();

    Vector3Int movePos;
    Vector3Int currentPos;
    Animator anim;

    LevelManager lm;

    bool isLevelComplete = false;

    private Touch theTouch;
    private Vector2 touchStartPosition, touchEndPosition;
    private string direction;

    bool hasMoved = false;

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        lm = FindObjectOfType<LevelManager>();
        GetFinishSpotLocation();
    }

    private void Update()
    {
        if (isLevelComplete) { return; }
        Move();
        CheckCratePosition();
        ChangeSprites();
    }
    private String TouchDirection()
    {
        if (Input.touchCount > 0)
        {
            theTouch = Input.GetTouch(0);

            if (theTouch.phase == TouchPhase.Began)
            {
                touchStartPosition = theTouch.position;
            }

            else if (theTouch.phase == TouchPhase.Moved || theTouch.phase == TouchPhase.Ended)
            {
                touchEndPosition = theTouch.position;
                float x = touchEndPosition.x - touchStartPosition.x;
                float y = touchEndPosition.y - touchStartPosition.y;

                if (Mathf.Abs(x) == 0 && Mathf.Abs(y) == 0)
                {
                    direction = "Tapped";
                }

                else if (Mathf.Abs(x) > Mathf.Abs(y))
                {
                    direction = x > 0 ? "Right" : "Left";
                }

                else
                {
                    direction = y > 0 ? "Up" : "Down";
                }
            }
        }
        return direction;
    }

    private void Move()
    {
        Vector2 direction;
        var axis = TouchDirection();
        direction = VectorDirection(axis);
        //print(hasMoved);
        //print(theTouch.phase);
        if (hasMoved == false && theTouch.phase == TouchPhase.Moved)
        {
            if (CanMove(direction) || CanPush(direction))
            {
                transform.position += (Vector3)direction;
                anim.SetFloat("Horizontal", direction.x);
                anim.SetFloat("Vertical", direction.y);
                hasMoved = true;
            }
        }
        if (theTouch.phase == TouchPhase.Ended)
        {
            hasMoved = false;
        }

    }

    private static Vector2 VectorDirection(string axis)
    {
        Vector2 direction;
        if (axis == "Left")
        {
            direction = Vector2.left;
        }
        else if (axis == "Right")
        {
            direction = Vector2.right;
        }
        else if (axis == "Up")
        {
            direction = Vector2.up;
        }
        else if (axis == "Down")
        {
            direction = Vector2.down;
        }
        else
        {
            direction = Vector2.zero;
        }

        return direction;
    }

    private bool CanMove(Vector2 direction)
    {
        Vector3Int gridPosition = floorTilemap.WorldToCell(transform.position + (Vector3)direction);
        if (!floorTilemap.HasTile(gridPosition) || wallTilemap.HasTile(gridPosition) || crateTilemap.HasTile(gridPosition)) { return false; }
        return true;
    }

    private bool CanPush(Vector2 direction)
    {
        Vector3Int gridPosition = crateTilemap.WorldToCell(transform.position + (Vector3)direction);
        Vector3Int newGridPosition = crateTilemap.WorldToCell(transform.position + (Vector3)direction * 2);

        if (crateTilemap.HasTile(gridPosition) && !crateTilemap.HasTile(newGridPosition) && !wallTilemap.HasTile(newGridPosition))
        {
            var myTile = crateTilemap.GetTile(gridPosition);
            crateTilemap.SetTile(gridPosition, null);
            crateTilemap.SetTile(newGridPosition, myTile);
            movePos = newGridPosition;
            currentPos = gridPosition;
            return true;
        }
        else
        {
            return false;
        }
    }

    private void GetFinishSpotLocation()
    {
        var amount = 0;
        BoundsInt bounds = finishTilemap.cellBounds;
        Vector3Int gridPosition1 = crateTilemap.WorldToCell(transform.position);
        foreach (Vector3Int pos in bounds.allPositionsWithin)
        {
            Tile tile = finishTilemap.GetTile<Tile>(pos);
            if (tile != null)
            {
                amount += 1;
                finishSpot.Add(pos);
            }
        }
        //Debug.Log("Finish " +finishSpot[0]);
        //Debug.Log("Finish " +finishSpot[1]);
        //Debug.Log("Total number of Finish Spots in the Level : " + amount);
    }

    private void CheckCratePosition()
    {
        int count = 0;
        foreach (Vector3Int position in finishSpot)
        {
            if (crateTilemap.HasTile(position))
            {
                count++;
                crateTilemap.SetTile(position, woodenBoxFinish);
            }
            else
            {
                count--;
            }
        }
        if (count == finishSpot.Count)
        {
            //print("Completed");
            lm.LevelFinish();
            isLevelComplete = true;
        }

        else
        {
            //print("Nope");
            return;
        }
    }

    private void ChangeSprites()
    {
        if(finishSpot.Contains(movePos) && crateTilemap.HasTile(movePos))
        {
            crateTilemap.SetTile(movePos, woodenBoxFinish);
        }
        else if(!finishSpot.Contains(movePos) && crateTilemap.HasTile(movePos))
        {
            crateTilemap.SetTile(movePos, woodenBox);
        }
    }

}
