using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldRock : MonoBehaviour
{
	public int Health { get; set; }

	private float ySpeed = 0f;

	private Rigidbody2D rb;

	private void Start() {
		rb = GetComponent<Rigidbody2D>();
		ySpeed = Random.Range(-2.8f, -3.2f);

		var x = Random.Range(-4f, 4f);
		var y = Random.Range(-4f, 4f);

		var target = new Vector3(x, y, 0);

		var direction = transform.position - target;

		rb.velocity = (direction.normalized * 3);

		Destroy(gameObject, 10f);
	}

	private void Update() {
		var step = ySpeed * Time.deltaTime;
		transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, 6.5f, 0), step);

	}

	public void Spawn() {
		Health = 1;
		transform.position = new Vector3(Random.Range(-6.5f, 6.5f), 5f, 0f);
	}

	public void TakeDamage(int damage) {
		// Invulnerable
	}
}
