using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
	[SerializeField] private int maxHealth = 100;
	[SerializeField] private int playerHealth;
	[SerializeField] private GameObject gameOverMenu;

	private float hitTime = 1;
	private float hitTimer = 0;
	private bool canHit = true;
	private bool isColliding;
	public Slider slider;


	void Start()
	{
		playerHealth = maxHealth;
	}

	public void SetHealth(int playerHealth)
    {
		slider.value = playerHealth;
    }

	private void OnTriggerEnter(Collider other)
	{
		if (isColliding)
		{
			return;
		}
		isColliding = true;

		if (other.gameObject.CompareTag("Death"))
		{
			TakeDamage(100);
		}

		if (other.gameObject.CompareTag("Pickup") && playerHealth < 100)
		{
			if (playerHealth >= 76)
            {
				playerHealth = 100;
            } 
			else
            {
				playerHealth = playerHealth + 25;
			}
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
			playerHealth = playerHealth - damage;
			SetHealth(playerHealth);
			if (playerHealth <= 0)
			{
				gameObject.SetActive(false);
				gameOverMenu.SetActive(true);
				Cursor.visible = true;
				Time.timeScale = 0f;
			}

			hitTimer = 0;
		}
	}
}
