using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RoundData", menuName = "Difficulty Data/Round", order = 2)]

public class RoundData : ScriptableObject
{
    public int roundID;
    public List<WaveData> waveDatas;
    
    public List<WaveData> GetWaveDatas()
    {
        return waveDatas;
        
    }
}
