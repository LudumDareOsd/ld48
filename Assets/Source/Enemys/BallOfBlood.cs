using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallOfBlood : BaseEnemy, IEnemy
{
	public AudioClip hitSound;
	public GameObject bullet;

	private float shootCooldown = 4f;
	private float transitionTime = 2f;

	private void Start()
	{
	}

	private void Update()
	{
		shootCooldown -= Time.deltaTime;

		if (shootCooldown < 0f)
		{
			shootCooldown = Random.Range(4f, 6f);
			var missile = Instantiate(bullet);
			missile.transform.position = transform.position;
			missile.transform.rotation = Random.rotation;
		}

	}

	public void Spawn()
	{
		Health = 15;
		transform.position = new Vector3(Random.Range(-6.5f, 6.5f), -5.5f, 0f);
		StartRandomMovement(transitionTime, 3f, -3.5f, 0f);
	}

	public void Despawn()
	{
		shootCooldown = float.PositiveInfinity;
		MoveTo(new Vector3(transform.position.x, -5.5f, 0), 3f);
		Destroy(gameObject, 5f);
	}

	public void TakeDamage(int damage)
	{
		AudioManager.Instance.PlaySingle(hitSound, 0.3f);
		Blink();

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
