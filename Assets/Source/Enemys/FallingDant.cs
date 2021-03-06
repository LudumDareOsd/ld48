using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingDant : BaseEnemy, IEnemy
{
	private float ySpeed = -2f;

	private void Start() {
		ySpeed = Random.Range(-2.8f, -3.8f);
	}

	private void Update() {
		var step = ySpeed * Time.deltaTime;
		transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, 6.5f, 0), step);

		if (transform.position.y < -16f) {
			Destroy(gameObject);
		}
	}

	public void Spawn() {
		Health = 1;
		transform.position = new Vector3(Random.Range(-6.5f, 6.5f), 5.9f, 0f);
	}

	public void Despawn()
	{
	}

	public void TakeDamage(int damage) {
		// Invulnerable
	}
}
