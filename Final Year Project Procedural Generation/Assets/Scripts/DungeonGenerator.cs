using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    public DungeonGenerationData dungeonGenerationData;


    private void Start()
    {
        var dungeonRooms = DungeonCrawlerController.GenerateDungeon(dungeonGenerationData);
        SpawnRooms(dungeonRooms);
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