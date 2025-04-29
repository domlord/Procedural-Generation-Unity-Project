using System;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text itemsText;
    [SerializeField] TMP_Text numberOfLevelsCompletedText;
    [SerializeField] TMP_Text playerHealthText;

    private void Start()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (GameManager.Instance == null) return;

        itemsText.text = "Items Held: " + GameManager.Instance.itemsHeld;
        numberOfLevelsCompletedText.text = "Number of levels: " + GameManager.Instance.levelsCompleted;
        playerHealthText.text = "Health: " + GameManager.Instance.playerHealth;
    }

    private void Update()
    {
        UpdateUI();
    }
}