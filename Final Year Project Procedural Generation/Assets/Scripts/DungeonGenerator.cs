using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    public DungeonGenerationData dungeonGenerationData;

    private List<Vector2Int> _dungeonRooms;

    private void Start()
    {
        _dungeonRooms = DungeonCrawlerController.GenerateDungeon(dungeonGenerationData);
        SpawnRooms(_dungeonRooms);
    }

    private void SpawnRooms(IEnumerable<Vector2Int> rooms)
    {
        RoomController.Instance.LoadRoom("Start", 0, 0);
        foreach (var roomLocation in rooms)
        {
            if (roomLocation == _dungeonRooms[^1] && roomLocation != Vector2Int.zero)
            {
                RoomController.Instance.LoadRoom("End", roomLocation.x, roomLocation.y);
            }
            else
            {
                RoomController.Instance.LoadRoom("Empty", roomLocation.x, roomLocation.y);
  
            }
        }
    }
}
