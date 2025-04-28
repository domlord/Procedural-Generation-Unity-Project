using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndOfLevelItemScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.AddStat(GameManager.StatType.LevelsCompleted);
        }

        Destroy(gameObject);
        SceneManager.LoadScene("BasementMain");
    }
}