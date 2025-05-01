using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "Spawner.asset", menuName = "Spawners/Spawner")]
public class SpawnerData : ScriptableObject
{
    public GameObject spawnableObject;
    [FormerlySerializedAs("minimumSpawnProbability")] public int minNumberOfItemsToSpawn;
    [FormerlySerializedAs("maximumSpawnProbability")] public int maxNumberOfItemsToSpawn;
}