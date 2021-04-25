using UnityEngine;
using System.Collections;

public class IceDrop : BaseEnemy, IEnemy
{
	public int Health { get; set; }

	private float ySpeed = -1f;

	private void Start()
	{
		ySpeed = Random.Range(-2.8f, -3.2f);
	}

	private void Update()
	{
		var step = ySpeed * Time.deltaTime;
		transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, 6.5f, 0), step);

		if (transform.position.y < -16f)
		{
			Destroy(gameObject);
		}
	}

	public void Spawn()
	{
		Health = 1;
		transform.position = new Vector3(Random.Range(-6.5f, 6.5f), 5f, 0f);
	}

	public void Despawn()
	{
	}

	public void TakeDamage(int damage)
	{
		//Health -= damage;
		if (Health <= 0)
		{
			// Do the death things
			Destroy(gameObject);
		}
	}

}
