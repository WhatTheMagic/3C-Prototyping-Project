using Palmmedia.ReportGenerator.Core.Parser.Analysis;
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
    [SerializeField] List<float> colliderArea = new List<float>();

    #region Vector3 collider areas 
    Vector3 spawnPoint;
    #endregion

    // The total area the colliders covers, relative to the (X,Z)-Axis
    float spawnTotalArea;

    void Start()
    {
        colliders = GetComponentsInChildren<Collider>();
        Debug.LogAssertion(colliders[0].bounds.center);
        Debug.LogAssertion(colliders[0].bounds.size);

        OutputData();
    }

    private void OutputData()
    {

        spawnTotalArea = 0;
        foreach(var collider in colliders)
        {
            float size = 0;
            switch (collider)
            {
                case BoxCollider:
                    size = collider.bounds.size.x * collider.bounds.size.z;
                    break;
                case SphereCollider:
                    SphereCollider col = collider as SphereCollider;
                    size = Mathf.Pow(col.radius * CompareLocalScale(col.transform.localScale), 2f) * Mathf.PI;
                    break;
                default:
                    break;
            }
 
            spawnTotalArea += size;
            colliderArea.Add(size);
            Debug.Log(size);
        }
        Debug.LogAssertion(spawnTotalArea);

        
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

    float CompareLocalScale(Vector3 localScale)
    {
        float x = Mathf.Abs(localScale.x);
        float y = Mathf.Abs(localScale.y);
        float z = Mathf.Abs(localScale.z);
        if (x >= y && x >= z)
        {
            return x;
        } else if (y >= x && y >= z) 
        {
            return y;
        } else
        {
            return z;
        }
    }
    
    [ContextMenu("Poop")]
    void Spawn()
    {
        float random = UnityEngine.Random.Range(0, spawnTotalArea);
        float collect = 0;
        Collider col = null;
        for (int i = 0; i < colliderArea.Count; i++)
        {
            float size = colliderArea[i];
            collect += size;
            if (collect >= random)
            {
                col = colliders[i];
                Debug.Log(i);
                break;
            }

        }
        
        switch (col)
        {
            case BoxCollider:
                 spawnPoint = new Vector3(UnityEngine.Random.Range(-col.bounds.size.x, col.bounds.size.z) / 2 , 0f, UnityEngine.Random.Range(-col.bounds.size.z, col.bounds.size.z) / 2) + col.bounds.center;

                break;

            case SphereCollider:
                SphereCollider collider = (SphereCollider)col;
                spawnPoint = UnityEngine.Random.Range(0f, collider.radius *  CompareLocalScale(col.transform.localScale)) * new Vector3(UnityEngine.Random.Range(-10f, 10f), 0f, UnityEngine.Random.Range(-10f, 10f)).normalized + col.bounds.center;

                break;
            default:
                break;
        }
        RaycastHit hit;
        if (Physics.Raycast(spawnPoint, Vector3.down, out hit, Mathf.Infinity))
        {
            Instantiate(spawnItem, hit.point, Quaternion.identity);

        }
    }
     void Spawn(GameObject spawnObject)
    {
        float random = UnityEngine.Random.Range(0, spawnTotalArea);
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
