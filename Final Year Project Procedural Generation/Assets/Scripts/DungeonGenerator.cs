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
            RoomController.Instance.LoadRoom(RoomController.Instance.GetRandomRoomName(), roomLocation.x,
                roomLocation.y);
        }
    }
}