using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObjectRoomSpawner : MonoBehaviour
{
    [Serializable]
    public struct RandomSpawner
    {
        public string name;
        public SpawnerData spawnerData;
    }

    public GridController grid;
    public RandomSpawner[] spawnerData;

    private void Start()
    {
        grid = GetComponentInChildren<GridController>();
    }

    public void InitialiseObjectSpawner()
    {
        foreach (var randomSpawner in spawnerData)
        {
            SpawnObjects(randomSpawner);
        }
    }

    void SpawnObjects(RandomSpawner data)
    {
        int randomIteration = Random.Range(data.spawnerData.minSpawn, data.spawnerData.maxSpawn + 1);

        for (int i = 0; i < randomIteration; i++)
        {
            int randomPos = Random.Range(0, grid.availablePoints.Count - 1);
            GameObject game = Instantiate(data.spawnerData.itemToSpawn, grid.availablePoints[randomPos],
                Quaternion.identity, transform);
            grid.availablePoints.RemoveAt(randomPos);
            Debug.Log("Spawned Object");
        }
    }
}