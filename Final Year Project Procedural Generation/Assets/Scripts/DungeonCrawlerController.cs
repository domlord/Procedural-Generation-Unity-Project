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
    public static List<Vector2Int> PositionsVisited = new List<Vector2Int>();

    private static readonly Dictionary<Direction, Vector2Int> DirectionMovementMap =
        new Dictionary<Direction, Vector2Int>
        {
            { Direction.up, Vector2Int.up },
            { Direction.left, Vector2Int.left },
            { Direction.down, Vector2Int.down },
            { Direction.right, Vector2Int.right }
        };

    public static List<Vector2Int> GenerateDungeon(DungeonGenerationData dungeonGenerationData)
    {
        List<DungeonCrawler> dungeonCrawlers = new List<DungeonCrawler>();
        DungeonCrawler dungeonCrawlerZero = new GameObject("DungeonCrawlerZero").AddComponent<DungeonCrawler>();
        
        for (int i = 0; i < dungeonGenerationData.numberOfCrawlers; i++) 
        {
           dungeonCrawlers.Add(dungeonCrawlerZero); 
        }
        
        int iterations = Random.Range(dungeonGenerationData.iterationMin, dungeonGenerationData.iterationMax);

        for (int i = 0; i < iterations; i++)
        {
            foreach (var dungeonCrawler in dungeonCrawlers)
            {
                Vector2Int newPosition = dungeonCrawler.Move(DirectionMovementMap);
                PositionsVisited.Add(newPosition);
            }
        }
        
        return PositionsVisited;
    }
}