using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpData : MonoBehaviour
{
    public PowerUp powerUp;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            powerUp.Apply(other.gameObject);
            Destroy(gameObject);
        }
    }
}
