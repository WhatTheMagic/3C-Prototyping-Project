using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoTarget : MonoBehaviour
{

    public string tagToDetect = "Enemy";
    public GameObject[] allEnemies;
    public GameObject currentEnemy;
    public float lookSpeed = 200;
    public LayerMask layerMask;
    void Start()
    {
        allEnemies = GameObject.FindGameObjectsWithTag(tagToDetect);
    }

    
    void Update()
    {
        currentEnemy = CurrentEnemy();
        Vector3 direction = currentEnemy.transform.position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        Quaternion lookAt = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * lookSpeed);
        transform.rotation = lookAt;
    }

    GameObject CurrentEnemy()
    {
        GameObject targetHere = gameObject;
        Collider[] targets = Physics.OverlapSphere(transform.position, 15f, layerMask); 
        foreach (var enemy in targets)
        {

        }

        return targetHere;
    }
}
