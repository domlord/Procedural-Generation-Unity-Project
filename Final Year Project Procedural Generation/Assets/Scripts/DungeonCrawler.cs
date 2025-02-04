using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonCrawler : MonoBehaviour
{
    public Vector2Int Position { get; set; }

    public DungeonCrawler(Vector2Int startPosition)
    {
        Position = startPosition;
    }

    public Vector2Int Move(Dictionary<Direction, Vector2Int> directionsMovementMap)
    {
        Direction toMove = (Direction)Random.Range(0, directionsMovementMap.Count);
        Position += directionsMovementMap[toMove];
        return Position;
    }
}
