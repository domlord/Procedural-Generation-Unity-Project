using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomInfo
{
    public string RoomName { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
}

public class RoomController : MonoBehaviour
{
    public static RoomController Instance { get; private set; }

    [SerializeField] private string baseRoomName = "Basement";

    private RoomInfo _currentRoomToLoad;
    private Room _activeRoom;
    private readonly Queue<RoomInfo> _roomLoadQueue = new Queue<RoomInfo>();
    private bool _isLoadingRoom;
    private bool _bossRoomSpawned;
    private bool _roomsUpdated;

    public List<Room> LoadedRooms { get; } = new List<Room>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Update()
    {
        ProcessRoomLoadQueue();
    }

    private void ProcessRoomLoadQueue()
    {
        if (_isLoadingRoom)
        {
            return;
        }

        if (_roomLoadQueue.Count == 0)
        {
            if (!_bossRoomSpawned)
            {
                StartCoroutine(SpawnBossRoomRoutine());
            }
            else if (!_roomsUpdated)
            {
                foreach (Room room in LoadedRooms)
                {
                    room.RemoveDisconnectedDoors();
                }

                UpdateRoomStates();
                _roomsUpdated = true;
            }

            return;
        }

        _currentRoomToLoad = _roomLoadQueue.Dequeue();
        _isLoadingRoom = true;
        StartCoroutine(LoadRoomRoutine(_currentRoomToLoad));
    }

    private IEnumerator SpawnBossRoomRoutine()
    {
        _bossRoomSpawned = true;
        yield return new WaitForSeconds(0.5f);

        if (_roomLoadQueue.Count == 0 && LoadedRooms.Count > 0)
        {
            var lastRoom = LoadedRooms[LoadedRooms.Count - 1];
            var tempRoom = new Room(lastRoom.X, lastRoom.Y);

            Destroy(lastRoom.gameObject);
            LoadedRooms.Remove(lastRoom);

            LoadRoom("End", tempRoom.X, tempRoom.Y);
        }
    }

    public void LoadRoom(string roomName, int x, int y)
    {
        if (DoesRoomExist(x, y))
        {
            return;
        }

        var newRoomInfo = new RoomInfo
        {
            RoomName = roomName,
            X = x,
            Y = y
        };

        _roomLoadQueue.Enqueue(newRoomInfo);
    }

    private IEnumerator LoadRoomRoutine(RoomInfo info)
    {
        var fullRoomName = baseRoomName + info.RoomName;
        var loadOperation = SceneManager.LoadSceneAsync(fullRoomName, LoadSceneMode.Additive);

        if (loadOperation == null)
        {
            Debug.LogError($"Failed to load room: {fullRoomName}. Check if the scene is in the build settings.");
            yield break; // Exit the coroutine safely
        }

        while (!loadOperation.isDone)
        {
            yield return null;
        }
    }

    public void RegisterRoom(Room room)
    {
        if (!DoesRoomExist(_currentRoomToLoad.X, _currentRoomToLoad.Y))
        {
            room.transform.position = new Vector3(
                _currentRoomToLoad.X * room.Width,
                _currentRoomToLoad.Y * room.Height,
                0f);

            room.X = _currentRoomToLoad.X;
            room.Y = _currentRoomToLoad.Y;
            room.name = $"{baseRoomName}-{_currentRoomToLoad.RoomName}-{room.X},{room.Y}";
            room.transform.parent = transform;

            _isLoadingRoom = false;

            if (LoadedRooms.Count == 0)
            {
                CameraController.Instance.currentRoom = room;
            }

            LoadedRooms.Add(room);
        }
        else
        {
            Destroy(room.gameObject);
            _isLoadingRoom = false;
        }
    }

    public bool DoesRoomExist(int x, int y)
    {
        return LoadedRooms.Any(room => room.X == x && room.Y == y);
    }

    public void OnPlayerEnterRoom(Room room)
    {
        CameraController.Instance.currentRoom = room;
        _activeRoom = room;
        UpdateRoomStates();
    }

    public void UpdateRoomStates()
    {
        if (_activeRoom == null)
        {
            Debug.LogWarning("UpdateRoomStates called but _activeRoom is null.");
            return;
        }

        foreach (var room in LoadedRooms)
        {
            if (room == null)
                continue;

            bool isActiveRoom = room == _activeRoom;

            var enemies = room.GetComponentsInChildren<EnemyController>() ?? System.Array.Empty<EnemyController>();
            var doors = room.GetComponentsInChildren<Door>() ?? System.Array.Empty<Door>();

            foreach (var enemy in enemies)
            {
                if (enemy == null) continue;
                enemy.isOutsideRoom = !isActiveRoom;
            }

            bool shouldActivateDoors = isActiveRoom && enemies.Any();
        
        }
    }


    public Room FindRoom(int x, int y)
    {
        return LoadedRooms.Find(room => room.X == x && room.Y == y);
    }

    public string GetRandomRoomName()
    {
        string[] availableRooms = { "Empty", "Basic1" };
        return availableRooms[Random.Range(0, availableRooms.Length)];
    }
}