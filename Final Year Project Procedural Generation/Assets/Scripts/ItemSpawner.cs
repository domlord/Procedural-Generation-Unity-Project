using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ItemSpawner : MonoBehaviour
{
    [Serializable]
    public struct Spawnable
    {
        public GameObject gameObject;
        public float probabilityOfSpawn;
    }

    public List<Spawnable> items = new List<Spawnable>();

    float _totalSpawnProbability;

    private void Awake()
    {
        _totalSpawnProbability = 0f;
        foreach (var spawnable in items)
        {
            _totalSpawnProbability += spawnable.probabilityOfSpawn;
        }
    }

    void Start()
    {
        float pick = Random.value * _totalSpawnProbability;
        int chosenIndex = 0;
        float cumulativeWeight = items[0].probabilityOfSpawn;

        while (pick > cumulativeWeight && chosenIndex < items.Count - 1)
        {
            chosenIndex++;
            cumulativeWeight += items[chosenIndex].probabilityOfSpawn;
        }

        GameObject i = Instantiate(items[chosenIndex].gameObject, transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
    }
}