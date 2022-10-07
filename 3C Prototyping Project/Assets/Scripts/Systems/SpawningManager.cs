using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningManager : MonoBehaviour
{
    /// <summary>
    /// Arthur: This script combines spawning fields and wave data together
    /// </summary>
    [Header("Spawning Data")]
    public SpawnerScript enemySpawns;
    public List<RoundData> spawnedRounds;
    
    [Space(12f)]
    public RoundData currentRound;
    [SerializeField] private List<WaveData> waveDataChanges = new List<WaveData>();

    List<HoldingData> lerpTimes = new List<HoldingData>();
    private RoundData currentRoundOriginal;

    float timer;


    void Awake()
    {
        int lowestRound = 1000;
        foreach (var item in spawnedRounds)
        {
            if (item.roundID < lowestRound) currentRoundOriginal = item;
        }
        timer = Time.time;
        SetRound();
        
    }


    void SetRound()
    {
        lerpTimes.Clear();
        currentRound = Instantiate(currentRoundOriginal);
        foreach (var item in currentRound.GetWaveDatas())
        {
            waveDataChanges.Add(Instantiate(item));
            lerpTimes.Add(new HoldingData());
        }
        Debug.LogWarning(waveDataChanges.Count);

        for (int i = 0; i < lerpTimes.Count; i++)
        {
            lerpTimes[i].startTime = timer + waveDataChanges[i].startDelay;
            lerpTimes[i].lerpRatio = 0f;
            lerpTimes[i].currentTime = 0f;
            lerpTimes[i].endTime = lerpTimes[i].currentTime + waveDataChanges[i].timeEnd;
            lerpTimes[i].spawnRate = waveDataChanges[i].spawnRateStart;
            StartCoroutine( SpawnDelay(lerpTimes[i].spawnRate + waveDataChanges[i].startDelay, waveDataChanges[i], i));
        }

    }

 
    IEnumerator SpawnDelay(float waitTime, WaveData waveData, int id)
    {
        Debug.LogWarning("Yes");
        yield return new WaitForSeconds(waitTime);
        Debug.LogWarning("No");

        enemySpawns.Spawn(waveData.enemyPrefab);
        lerpTimes[id].currentTime = Time.time - lerpTimes[id].startTime;
        lerpTimes[id].spawnRate = Mathf.Lerp(waveDataChanges[id].spawnRateStart, waveDataChanges[id].spawnRateEnd,  lerpTimes[id].currentTime /lerpTimes[id].endTime );
        if (lerpTimes[id].currentTime < lerpTimes[id].endTime)
        {
            Debug.LogWarning(lerpTimes[id].currentTime +"    No   shit   " + lerpTimes[id].spawnRate);

            StartCoroutine(SpawnDelay(lerpTimes[id].spawnRate, waveData, id));
        }else 
        {
            yield return null;
        }
    }
}

