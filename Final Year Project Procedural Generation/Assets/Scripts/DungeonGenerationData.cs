using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DungeonGenerationData.asset", menuName = "DungeonGenerationData/Dungeon Data")]
public class DungeonGenerationData : ScriptableObject
{
    public int numberOfCrawlers; //number of rooms generated
    public int iterationMin; //minimum number of rooms generated
    public int iterationMax; //maximum number of rooms generated
}
