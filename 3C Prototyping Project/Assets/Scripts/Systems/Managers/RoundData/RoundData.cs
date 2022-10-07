using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RoundData", menuName = "Difficulty Data/Round", order = 2)]

public class RoundData : ScriptableObject
{
    /// <summary>
    /// Arthur: This script is meant to be a collection of waves, and once those waves were cleared,
    /// would switch to the next round... It has been left mostly unused and unfurnished
    /// </summary>

    public int roundID;
    public List<WaveData> waveDatas;
    
    public List<WaveData> GetWaveDatas()
    {
        return waveDatas;
        
    }
}
