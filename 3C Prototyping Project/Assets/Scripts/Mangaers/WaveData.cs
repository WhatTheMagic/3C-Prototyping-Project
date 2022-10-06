using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WaveData", menuName = "Difficulty Data/Wave", order = 1)]
public class WaveData : ScriptableObject
{
    public float timeEnd;
    public float spawnRateStart;
    public float spawnRateEnd;

    public GameObject enemyPrefab;
    public Characters enemyType;
    
}

public enum Characters
{
    Minion = 0,
    StongerMinion = 1,
    Boss = 2

}