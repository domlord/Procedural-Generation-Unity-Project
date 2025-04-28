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
        var randomIteration = Random.Range(data.spawnerData.minimumSpawnProbability,
            data.spawnerData.maximumSpawnProbability + 1);

        for (var i = 0; i < randomIteration; i++)
        {
            var randomPos = Random.Range(0, grid.availablePoints.Count - 1);
            var item = Instantiate(data.spawnerData.spawnableObject, grid.availablePoints[randomPos],
                Quaternion.identity, transform);
            grid.availablePoints.RemoveAt(randomPos);
            Debug.Log("Spawned Object");
        }
    }
}