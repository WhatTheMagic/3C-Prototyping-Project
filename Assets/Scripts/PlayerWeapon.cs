using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform shootingStartPosition;

    private void OnFire()
    {
        if (!PauseMenu.gameIsPaused)
        {
            GameObject newProjectile = Instantiate(projectilePrefab);
            newProjectile.transform.position = shootingStartPosition.position;
            newProjectile.transform.rotation = shootingStartPosition.rotation;
            newProjectile.GetComponent<Projectile>().Initialize();
        }
    }
}
