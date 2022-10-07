using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WaveData", menuName = "Difficulty Data/Wave", order = 1)]
public class WaveData : ScriptableObject
{
    /// <summary>
    /// Arthur: This allows you to create a data block that contains information on spawnable waves
    /// </summary>

    // Starts with a delay after the round starts 
    public float startDelay;
    // The length of this wave in seconds
    public float timeEnd; 
    // The initial spawnrate of the entities in this wave
    public float spawnRateStart;
    // The final spawnrate of entities in this wave
    public float spawnRateEnd;

    // The spawned objects prefab
    public GameObject enemyPrefab;
    // : NOT USED : 
    public Characters enemyType;
    
}

public enum Characters
{
    Minion = 0,
    StongerMinion = 1,
    Boss = 2

}