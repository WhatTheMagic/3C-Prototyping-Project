using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform shootingStartPosition;

    private void Update()
    {
        if (!Pause.gameIsPaused)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                GameObject newProjectile = Instantiate(projectilePrefab);
                newProjectile.transform.position = shootingStartPosition.position;
                newProjectile.transform.rotation = shootingStartPosition.rotation;
                newProjectile.GetComponent<Projectile>().Initialize();
            }
        }
    }
}
