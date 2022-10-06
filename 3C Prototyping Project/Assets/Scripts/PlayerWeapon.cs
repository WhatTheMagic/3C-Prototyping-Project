using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform shootingStartPosition;
    [SerializeField] private CharacterController character;
    [SerializeField] List<Collider> colliders;

    private void Awake()
    {
        //List<Collider> collier1 = gameObject.GetComponents<Collider>().ToList();
        colliders = gameObject.GetComponentsInChildren <Collider>().ToList();
    }
    private void OnFire()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/Player/PlayerShoot/PlayerShoot");
        if (!PauseMenu.gameIsPaused)
        {
            GameObject newProjectile = Instantiate(projectilePrefab);
            newProjectile.transform.rotation = shootingStartPosition.rotation;
            newProjectile.transform.position = shootingStartPosition.position; //+ new Vector3(shootingStartPosition.transform.forward.x, 0f, shootingStartPosition.transform.forward.z).normalized * character.velocity.magnitude;
            newProjectile.GetComponent<Projectile>().Initialize(colliders);
        }
    }
}
