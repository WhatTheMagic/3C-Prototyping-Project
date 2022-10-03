using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HealthUp", menuName = "PowerUps/Health Up", order = 1)]
public class HealthUp : PowerUp
{
    public float healAmount;

    public override void Apply(GameObject target)
    {
        target.GetComponent<PT_PlayerHealth>().health += healAmount;
    }
}
