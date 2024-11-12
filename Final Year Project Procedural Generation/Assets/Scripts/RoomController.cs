using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    Queue<RoomInfo> _loadRoomQueue = new Queue<RoomInfo>();

    public List<Room> loadedRooms = new List<Room>();

    private bool _isLoadingRoom;

    private void Awake()
    {
        Instance = this;
    }

    public void LoadRoom(string roomName, int x, int y)
    {
        RoomInfo newRoomData = new RoomInfo();
        newRoomData.RoomName = roomName;
        newRoomData.X = x;
        newRoomData.Y = y;

        _loadRoomQueue.Enqueue(newRoomData);
    }

    IEnumerator LoadRoomRoutine(RoomInfo info)
    {
        string roomName = CurrentRoomName + info.RoomName;

        AsyncOperation loadRoom = SceneManager.LoadSceneAsync(roomName, LoadSceneMode.Additive);

        while (loadRoom.isDone == false)
        {
            yield return null;
        }
    }

    public void RegisterRoom(Room room)
    {
        room.transform.position = new Vector3(
            CurrentLoadRoomData.X * room.Width,
            CurrentLoadRoomData.Y * room.Height, 0);

        room.X = CurrentLoadRoomData.X;
        room.Y = CurrentLoadRoomData.Y;
        room.name = CurrentRoomName + "-" + CurrentLoadRoomData.RoomName + "-" + room.X + ", " + room.Y;
        room.transform.parent = transform;
    }

    public bool DoesRoomExist(int x, int y)
    {
        return loadedRooms.Find(item => item.X == x && item.Y == y) != null;
    }
}