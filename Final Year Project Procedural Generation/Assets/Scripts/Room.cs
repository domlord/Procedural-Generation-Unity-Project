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
                c
            }
        }
        
        

        RoomController.Instance.RegisterRoom(this);
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