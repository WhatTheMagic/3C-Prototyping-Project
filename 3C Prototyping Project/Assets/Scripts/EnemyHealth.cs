using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
	[SerializeField] private int maxHealth = 100;
	[SerializeField] private int enemyHealth;


	void Start()
	{
		enemyHealth = maxHealth;
	}

	private void OnTriggerEnter(Collider other)
	{

		if (other.gameObject.CompareTag("Projectile"))
		{
			TakeDamage(34);
		}

		if (other.gameObject.CompareTag("Death"))
		{
			TakeDamage(100);
		}
	}


	public void TakeDamage(int damage)
	{
		enemyHealth = enemyHealth - damage;
		if (enemyHealth <= 0)
		{
			gameObject.SetActive(false);
		}
	}
}
