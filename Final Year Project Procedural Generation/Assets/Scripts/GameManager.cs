using System;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int itemsHeld; // Tracks how many items the player is carrying
    public int levelsCompleted; // Tracks how many items the player is carrying

    public enum StatType
    {
        ItemsHeld,
        LevelsCompleted
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
                Debug.Log($"Item picked up! Total items: {itemsHeld}");
                break;

            case StatType.LevelsCompleted:
                levelsCompleted++;
                Debug.Log($"Level completed! Total levels: {levelsCompleted}");
                break;
        }
    }
}