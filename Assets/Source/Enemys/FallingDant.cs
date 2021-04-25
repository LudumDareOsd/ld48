using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingDant : MonoBehaviour, IEnemy
{
	public int Health { get; set; }

	private float ySpeed = -2f;

	private void Start() {
		ySpeed = Random.Range(-2.8f, -3.2f);
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
		transform.position = new Vector3(Random.Range(-6.5f, 6.5f), 5f, 0f);
	}

	public void TakeDamage(int damage) {
		// Invulnerable
	}
}
