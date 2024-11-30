using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    up = 0,
    left = 1,
    down = 2,
    right = 3
};


public class DungeonCrawlerController : MonoBehaviour
{
    public static List<Vector2Int> _positionsVisited = new List<Vector2Int>();

    private static readonly Dictionary<Direction, Vector2Int> _directionMovementMap =
        new Dictionary<Direction, Vector2Int>
        {
            { Direction.up, Vector2Int.up },
            { Direction.left, Vector2Int.left },
            { Direction.down, Vector2Int.down },
            { Direction.right, Vector2Int.right }
        };
}