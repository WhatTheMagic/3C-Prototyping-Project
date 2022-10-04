using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    [Header("CREATE EMPTY GAME OBJECT WITH ONE")]
    [Header("TRIGGER BOX COLLIDER AS CHILDREN")]
    [Space(15)]
    public GameObject spawnItem;
    
    // Store childrens colliders
    Collider[] colliders;

    #region Vector3 collider areas 
    Vector3 spawnPoint;
    Vector3 m_Center;
    Vector3 m_Size, m_Min, m_Max;
    #endregion

    // The total area the colliders covers, relative to the (X,Z)-Axis
    float spawnArea;

    void Start()
    {
        colliders = GetComponentsInChildren<Collider>();
        //Fetch the Collider from the GameObject
        
        //Fetch the center of the Collider volume
        m_Center = colliders[0].bounds.center;
        //Fetch the size of the Collider volume
        m_Size = colliders[0].bounds.size;
        //Fetch the minimum and maximum bounds of the Collider volume
        m_Min = colliders[0].bounds.min;
        m_Max = colliders[0].bounds.max;
        //Output this data into the console
        OutputData();
    }

    private void OutputData()
    {
        //Output to the console the center and size of the Collider volume
        Debug.Log("Collider Center : " + m_Center);
        Debug.Log("Collider Size : " + m_Size);
        Debug.Log("Collider bound Minimum : " + m_Min);
        Debug.Log("Collider bound Maximum : " + m_Max);

        spawnArea = 0;
        foreach(var collider in colliders)
        {
            float size = collider.bounds.size.x * collider.bounds.size.z;
            spawnArea += size;
            Debug.Log(size);
        }
        Debug.LogAssertion(spawnArea);

        
        Spawn(spawnItem);
        StartCoroutine(Poop());
        
    }

    IEnumerator Poop()
    {
        int i = 0;
        while(i < 1000)
        {
            Spawn();
            i++;
            yield return new WaitForFixedUpdate();
        }

    }

    [ContextMenu("Poop")]
    void Spawn()
    {
        float random = UnityEngine.Random.Range(0, spawnArea);
        float collect = 0;
        Collider col = null;
        for (int i = 0; i < colliders.Length; i++)
        {
            float size = colliders[i].bounds.size.x * colliders[i].bounds.size.z;
            collect += size;
            if (collect >= random)
            {
                col = colliders[i];
                Debug.Log(i);
                break;
            }

        }
        spawnPoint = new Vector3(UnityEngine.Random.Range(col.bounds.min.x, col.bounds.max.x), 1f, UnityEngine.Random.Range(col.bounds.min.z, col.bounds.max.z));
        RaycastHit hit;
        if (Physics.Raycast(spawnPoint, Vector3.down, out hit, Mathf.Infinity))
        {
            Instantiate(spawnItem, hit.point, Quaternion.identity);

        }
    }
     void Spawn(GameObject spawnObject)
    {
        float random = UnityEngine.Random.Range(0, spawnArea);
        float collect = 0;
        Collider col = null;
        for (int i = 0; i < colliders.Length; i++)
        {
            float size = colliders[i].bounds.size.x * colliders[i].bounds.size.z;
            collect += size;
            if (collect >= random)
            {
                col = colliders[i];
                Debug.Log(i);
                break;
            }

        }
        spawnPoint = new Vector3(UnityEngine.Random.Range(col.bounds.min.x, col.bounds.max.x), 1f, UnityEngine.Random.Range(col.bounds.min.z, col.bounds.max.z));
        RaycastHit hit;
        if (Physics.Raycast(spawnPoint, Vector3.down, out hit, Mathf.Infinity))
        {
            Instantiate(spawnObject, hit.point, Quaternion.identity);

        }
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(colliders[0]);
        
    }
}
