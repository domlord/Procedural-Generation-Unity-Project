using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    private static float health = 10;
    private static int maxHealth = 10;
    private static float moveSpeed = 5f;
    private static float fireRate = 0.5f;
    private static float bulletSize = 0.5f;


    public TMP_Text healthText;

    public static float Health
    {
        get => health;
        set => health = value;
    }

    public static int MaxHealth
    {
        get => maxHealth;
        set => maxHealth = value;
    }

    public static float MoveSpeed
    {
        get => MoveSpeed;
        set => MoveSpeed = value;
    }

    public static float FireRate
    {
        get => fireRate;
        set => fireRate = value;
    }

    public static float BulletSize
    {
        get => bulletSize;
        set => bulletSize = value;
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Update()
    {
        healthText.text = "Health: " + health;
    }

    public void DamagePlayer(int damage)
    {
        Health -= damage;

        if (Health <= 0)
        {
            KillPlayer();
        }
    }

    public static void HealPlayer(float healAmount)
    {
        Health = Mathf.Min(Health, Health + healAmount);
    }

    public static void MoveSpeedChange(float speed)
    {
        moveSpeed += speed;
    }

    public static void FireRateChange(float rate)
    {
        fireRate += rate;
    }

    public static void BulletSizeChange(float size)
    {
        bulletSize += size;
    }

    private static void KillPlayer()
    {
        throw new NotImplementedException();
    }
}