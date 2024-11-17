using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance;
    
    public Room currentRoom;
    
    public float moveSpeedDuringRoomChange;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        UpdatePosition();
    }

    private void UpdatePosition()
    {
        if(currentRoom == null) return;
        
        V
    }
}
