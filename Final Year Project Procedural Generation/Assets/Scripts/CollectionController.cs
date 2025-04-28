using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[Serializable]
public class Item
{
    public string name;
    public string description;
    public Sprite itemImage;
}


public class CollectionController : MonoBehaviour
{
    public Item Item;

    private void Start()
    {
        GetComponent<SpriteRenderer>().sprite = Item.itemImage;
        Destroy(GetComponent<PolygonCollider2D>());
        gameObject.AddComponent<PolygonCollider2D>();
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.AddStat(GameManager.StatType.ItemsHeld);
            Destroy(gameObject);
        }
    }
}