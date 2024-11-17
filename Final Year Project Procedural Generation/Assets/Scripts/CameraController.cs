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
        if (currentRoom == null) return;

        Vector3 cameraTargetPosition = GetCameraTargetPosition();

        transform.position = Vector3.MoveTowards(transform.position, cameraTargetPosition,
            moveSpeedDuringRoomChange * Time.deltaTime);
    }

    private Vector3 GetCameraTargetPosition()
    {
        if (currentRoom == null) return Vector3.zero;

        Vector3 cameraTargetPosition = currentRoom.GetRoomCentre();
        cameraTargetPosition.z = transform.position.z;

        return cameraTargetPosition;
    }

    public bool IsSwitchingScene()
    {
        return transform.position.Equals(GetCameraTargetPosition()) == false;
    }
}