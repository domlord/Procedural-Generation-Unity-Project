using System;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int itemsHeld; // Tracks how many items the player is carrying
    public int levelsCompleted; // Tracks how many items the player is carrying
    public int playerHealth; // Tracks the player health

    public enum StatType
    {
        ItemsHeld,
        LevelsCompleted,
        PlayerHealth
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist through scene loads
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate GameManagers
        }
    }

    // Call this when picking up an item
    public void AddStat(StatType statType)
    {
        switch (statType)
        {
            case StatType.ItemsHeld:
                itemsHeld++;
                break;

            case StatType.LevelsCompleted:
                levelsCompleted++;
                break;
            
            case StatType.PlayerHealth:
                playerHealth--;
                break;
        }
    }
}