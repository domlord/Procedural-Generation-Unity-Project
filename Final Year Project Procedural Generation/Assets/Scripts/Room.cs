using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public int Width;
    public int Height;

    public int X; //test
    public int Y;

    public Room(int x, int y)
    {
        X = x;
        Y = y;
    }
    
    public Door leftDoor;
    public Door rightDoor;
    public Door topDoor;
    public Door bottomDoor;
    
    public List<Door> doors = new List<Door>();

    private void Start()
    {
        if (RoomController.Instance == null)
        {
            return;
        }
        
        Door[] doorsChildren = GetComponentsInChildren<Door>();
        foreach (Door door in doorsChildren)
        {
            switch (door.doorType)
            {
                case Door.DoorType.Right :
                    rightDoor = door;
                    doors.Add(rightDoor);
                    break;
                case Door.DoorType.Left :
                    leftDoor = door;
                    doors.Add(leftDoor);
                    break;
                case Door.DoorType.Top :
                    topDoor = door;
                    doors.Add(topDoor);
                    break;
                case Door.DoorType.Bottom :
                    bottomDoor = door;
                    doors.Add(bottomDoor);
                    break;
            }
        }
        
        RoomController.Instance.RegisterRoom(this);
    }

    public void RemoveDisconnectedDoors()
    {
        foreach (var door in doors)
        {
            switch (door.doorType)
            {
                case Door.DoorType.Right :
                    if(GetRight() == null)
                        door.gameObject.SetActive(false);
                    break;
                case Door.DoorType.Left :
                    if(GetLeft() == null)
                        door.gameObject.SetActive(false);
                    break;
                case Door.DoorType.Top :
                    if(GetTop() == null)
                        door.gameObject.SetActive(false);
                    break;
                case Door.DoorType.Bottom : 
                    if(GetBottom() == null)
                        door.gameObject.SetActive(false);
                    break;
            }
        }
    }

    public Room GetRight()
    {
        if (RoomController.Instance.DoesRoomExist(X + 1, Y))
        {
            return RoomController.Instance.FindRoom(X + 1, Y);
        }
        return null;
    }

    public Room GetLeft()
    {
        if (RoomController.Instance.DoesRoomExist(X - 1, Y))
        {
            return RoomController.Instance.FindRoom(X - 1, Y);
        }
        return null;
    }

    public Room GetTop()
    {
        if (RoomController.Instance.DoesRoomExist(X, Y + 1))
        {
            return RoomController.Instance.FindRoom(X, Y + 1);
        }
        return null;
    }

    public Room GetBottom()
    {
        if (RoomController.Instance.DoesRoomExist(X, Y - 1))
        {
            return RoomController.Instance.FindRoom(X, Y - 1);
        }
        return null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(Width, Height, 0));
    }

    public Vector3 GetRoomCentre()
    {
        return new Vector3(X * Width, Y * Height);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Player entered");
            RoomController.Instance.OnPlayerEnterRoom(this);
        }
    }
}