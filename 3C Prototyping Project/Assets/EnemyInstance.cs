using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInstance : MonoBehaviour
{
    private void OnEnable()
    {
        EntityManager.instance.enemyCount++;
    }
    private void OnDisable()
    {
        EntityManager.instance.enemyCount--;
    }
}
