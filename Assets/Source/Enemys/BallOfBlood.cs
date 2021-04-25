using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallOfBlood : BaseEnemy, IEnemy
{
	public AudioClip hitSound;

	private void Start()
	{
	}

	private void Update()
	{

	}

	public void Spawn()
	{
		Health = 25;
	}

	public void Despawn()
	{
	}

	public void TakeDamage(int damage)
	{
		AudioManager.Instance.PlaySingle(hitSound, 0.3f);

		Health -= damage;
		if (Health <= 0)
		{
			Destroy(gameObject);
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.name == "Attack")
		{
			collision.gameObject.SetActive(false);
			TakeDamage(5);
		}
	}

}
