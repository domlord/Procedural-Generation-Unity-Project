using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public enum DoorType
    {
        Left, Right, Top, Bottom
    }
    public DoorType doorType;

    public GameObject doorCollider;
    
    private GameObject _player;

    private float _widthOffset = 1.75f;

    private void Start()
    { 
        _player = GameObject.FindGameObjectWithTag("Player");
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            switch (doorType)
            {
                case DoorType.Bottom:
                    _player.transform.position = new Vector2(_player.transform.position.x, _player.transform.position.y - _widthOffset);
                    break;
                case DoorType.Left:
                    _player.transform.position = new Vector2(_player.transform.position.x - _widthOffset, _player.transform.position.y);
                    break;
                case DoorType.Right:
                    _player.transform.position = new Vector2(_player.transform.position.x + _widthOffset, _player.transform.position.y);
                    break;
                case DoorType.Top:
                    _player.transform.position = new Vector2(_player.transform.position.x, _player.transform.position.y + _widthOffset);
                    break;
            }
        }
    }
}

