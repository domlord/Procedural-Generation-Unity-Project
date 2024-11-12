using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomInfo
{
    public string RoomName;
    public int X;
    public int Y;
}

public class RoomController : MonoBehaviour
{
    public static RoomController Instance;

    private string CurrentRoomName = "Basement";

    private RoomInfo CurrentLoadRoomData;

    Queue<RoomInfo> _roomQueue = new Queue<RoomInfo>();

    public List<Room> loadedRooms = new List<Room>();

    private bool _isLoadingRoom;

    private void Awake()
    {
        Instance = this;
    }

    public bool DoesRoomExist(int x, int y)
    {
        return loadedRooms.Find(item => item.X == x && item.Y == y) != null;
    }
}