using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
	[SerializeField] private int maxHealth = 100;
	[SerializeField] private int enemyHealth;

	private float hitTime = 1;
	private float hitTimer = 0;
	private bool canHit = true;
	private bool isColliding;


	void Start()
	{
		enemyHealth = maxHealth;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (isColliding)
		{
			return;
		}
		isColliding = true;

		if (other.gameObject.CompareTag("Projectile"))
		{
			TakeDamage(10);
		}

		if (other.gameObject.CompareTag("Death"))
		{
			TakeDamage(100);
		}
	}

	void Update()
	{
		isColliding = false;
		hitTimer += Time.deltaTime;
		if (hitTimer > hitTime)
		{
			canHit = true;
		}
	}

	public void TakeDamage(int damage)
	{
		if (!canHit)
		{
			return;
		}
		else
		{
			enemyHealth = enemyHealth - damage;
			if (enemyHealth <= 0)
			{
				gameObject.SetActive(false);
			}

			hitTimer = 0;
		}
	}
}
