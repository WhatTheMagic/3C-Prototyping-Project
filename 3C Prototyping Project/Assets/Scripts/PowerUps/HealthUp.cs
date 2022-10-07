using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HealthUp", menuName = "PowerUps/Health Up", order = 1)]
public class HealthUp : PowerUp
{
    public int healAmount;

    public override void Apply(GameObject target)
    {
        PlayerHealth playerHealth = target.GetComponent<PlayerHealth>();
        playerHealth.playerHealth = (int)Mathf.Clamp(playerHealth.playerHealth + healAmount, 0f, playerHealth.maxHealth ) ;
    }
}
