using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    Up = 0,
    Left = 1,
    Down = 2,
    Right = 3
};


public class DungeonCrawlerController : MonoBehaviour
{ 
    private static readonly  List<Vector2Int> PositionsVisited = new();

    private static readonly Dictionary<Direction, Vector2Int> DirectionMovementMap = new()
    {
        { Direction.Up, Vector2Int.up },
        { Direction.Left, Vector2Int.left },
        { Direction.Down, Vector2Int.down },
        { Direction.Right, Vector2Int.right }
    };


    public static List<Vector2Int> GenerateDungeon(DungeonGenerationData dungeonGenerationData)
    {
        var dungeonCrawlers = new List<DungeonCrawler>();
        var dungeonCrawlerZero = new GameObject("DungeonCrawlerZero").AddComponent<DungeonCrawler>();
        
        for (var i = 0; i < dungeonGenerationData.numberOfCrawlers; i++) 
        {
           dungeonCrawlers.Add(dungeonCrawlerZero); 
        }
        
        var iterations = Random.Range(dungeonGenerationData.iterationMin, dungeonGenerationData.iterationMax);

        for (var i = 0; i < iterations; i++)
        {
            foreach (var dungeonCrawler in dungeonCrawlers)
            {
                var newPosition = dungeonCrawler.Move(DirectionMovementMap);
                PositionsVisited.Add(newPosition);
            }
        }
        
        return PositionsVisited;
    }
}