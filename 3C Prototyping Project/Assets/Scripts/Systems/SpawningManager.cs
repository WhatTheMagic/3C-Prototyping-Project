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
    [SerializeField] private List<WaveData> _waveDataChanges = new List<WaveData>();

    List<HoldingData> _lerpTimes = new List<HoldingData>();
    private RoundData _currentRoundOriginal;

    float timer;


    void Awake()
    {
        int lowestRound = 1000;
        foreach (var item in spawnedRounds)
        {
            if (item.roundID < lowestRound) _currentRoundOriginal = item;
        }
        timer = Time.time;
        SetRound();
        
    }


    void SetRound()
    {
        _lerpTimes.Clear();
        currentRound = Instantiate(_currentRoundOriginal);
        foreach (var item in currentRound.GetWaveDatas())
        {
            _waveDataChanges.Add(Instantiate(item));
            _lerpTimes.Add(new HoldingData());
        }
        //Debug.LogWarning(waveDataChanges.Count);

        for (int i = 0; i < _lerpTimes.Count; i++)
        {
            _lerpTimes[i].startTime = timer + _waveDataChanges[i].startDelay;
            _lerpTimes[i].lerpRatio = 0f;
            _lerpTimes[i].currentTime = 0f;
            _lerpTimes[i].endTime = _lerpTimes[i].currentTime + _waveDataChanges[i].timeEnd;
            _lerpTimes[i].spawnRate = _waveDataChanges[i].spawnRateStart;
            StartCoroutine( SpawnDelay(_lerpTimes[i].spawnRate + _waveDataChanges[i].startDelay, _waveDataChanges[i], i));
        }

    }

 
    IEnumerator SpawnDelay(float waitTime, WaveData waveData, int id)
    {
        //Debug.LogWarning("Yes");
        yield return new WaitForSeconds(waitTime);
        //Debug.LogWarning("No");

        enemySpawns.Spawn(waveData.enemyPrefab);
        _lerpTimes[id].currentTime = Time.time - _lerpTimes[id].startTime;
        _lerpTimes[id].spawnRate = Mathf.Lerp(_waveDataChanges[id].spawnRateStart, _waveDataChanges[id].spawnRateEnd,  _lerpTimes[id].currentTime /_lerpTimes[id].endTime );
        if (_lerpTimes[id].currentTime < _lerpTimes[id].endTime)
        {
            //Debug.LogWarning(lerpTimes[id].currentTime +"    No   shit   " + lerpTimes[id].spawnRate);

            StartCoroutine(SpawnDelay(_lerpTimes[id].spawnRate, waveData, id));
        }else 
        {
            yield return null;
        }
    }
}

