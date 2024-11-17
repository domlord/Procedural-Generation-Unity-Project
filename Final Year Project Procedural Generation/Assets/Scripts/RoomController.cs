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

    private readonly string _currentRoomName = "Basement";

    private RoomInfo _currentLoadRoomData;

    private Room _currentRoom;

    readonly Queue<RoomInfo> _loadRoomQueue = new Queue<RoomInfo>(); //A queue of rooms to be loaded

    public List<Room> loadedRooms = new List<Room>(); //A list of all rooms

    private bool _isLoadingRoom;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        LoadRoom("Start", 0, 0);
        LoadRoom("Empty", 1, 0);
        LoadRoom("Empty", -1, 0);
        LoadRoom("Empty", 0, 1);
        LoadRoom("Empty", 0, -1);
    }

    private void Update()
    {
        UpdateRoomQueue();
    }

    private void UpdateRoomQueue()
    {
        if (_isLoadingRoom || _loadRoomQueue.Count == 0)
        {
            return;
        }

        _currentLoadRoomData = _loadRoomQueue.Dequeue();
        _isLoadingRoom = true;

        StartCoroutine(LoadRoomRoutine(_currentLoadRoomData));
    }

    /*
     * A function that loads a given room, taking in parameters roomName, x and y.
     * Room is loaded at x and y coordinates input into the function
     */
    public void LoadRoom(string roomName, int x, int y)
    {
        if (DoesRoomExist(x, y))
        {
            return;
        }

        RoomInfo newRoomData = new RoomInfo();
        newRoomData.RoomName = roomName;
        newRoomData.X = x;
        newRoomData.Y = y;

        _loadRoomQueue.Enqueue(newRoomData);
    }

    IEnumerator LoadRoomRoutine(RoomInfo info)
    {
        string roomName = _currentRoomName + info.RoomName;

        AsyncOperation loadRoom = SceneManager.LoadSceneAsync(roomName, LoadSceneMode.Additive);

        while (loadRoom.isDone == false)
        {
            yield return null;
        }
    }

    public void RegisterRoom(Room room)
    {
        room.transform.position = new Vector3(
            _currentLoadRoomData.X * room.Width,
            _currentLoadRoomData.Y * room.Height, 0);

        room.X = _currentLoadRoomData.X;
        room.Y = _currentLoadRoomData.Y;
        room.name = _currentRoomName + "-" + _currentLoadRoomData.RoomName + "-" + room.X + ", " + room.Y;
        room.transform.parent = transform;

        _isLoadingRoom = false;

        if (loadedRooms.Count == 0)
        {
            CameraController.Instance.currentRoom = room;
        }

        loadedRooms.Add(room);
    }

    public bool DoesRoomExist(int x, int y)
    {
        return loadedRooms.Find(item => item.X == x && item.Y == y) != null; //Returns a 
    }

    public void OnPlayerEnterRoom(Room room)
    {
        CameraController.Instance.currentRoom = room;
        _currentRoom = room;
    }
}