using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
    public Room room;
    
    [System.Serializable]
    public struct Grid
    {
        public int columns, rows;
        public float verticalOffset, horizontalOffset;
    }
    
    public Grid grid;
    public GameObject gridTile;
    public List<Vector2> availablePoints;

    private void Awake()
    {
        room = GetComponentInParent<Room>();
        grid.columns = room.Width - 2;
        grid.rows = room.Height - 2;
        GenerateGrid();
    }

    private void GenerateGrid()
    {
        grid.verticalOffset += room.transform.localPosition.y;
        grid.horizontalOffset += room.transform.localPosition.x;

        for (int y = 0; y < grid.rows; y++)
        {
            for (int x = 0; x < grid.columns; x++)
            {
                GameObject game = Instantiate(gridTile, transform);
                gameObject.transform.position = new Vector2(x - (grid.columns - grid.horizontalOffset), 
                    y - (grid.rows - grid.verticalOffset));
                gameObject.name =  "X: " + x + ", Y: " + y;
                availablePoints.Add(game.transform.position);
            }
        }
    }
}
