using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkSerrath : BaseEnemy, IEnemy
{
	public AudioClip hitSound;
	private float transitionTime = 0.75f;

	private void Update() {
		if (transform.position.y > 6f) {
			Destroy(gameObject);
		}
	}

	public void Spawn() {
		Health = 20;
		transform.position = new Vector3(Random.Range(-6.5f, 6.5f), -5.5f, 0f);
		StartRandomMovement(transitionTime, 3.5f);
	}

	public void Despawn()
	{
		MoveTo(new Vector3(transform.position.x, -5.5f, 0), 3f);
		Destroy(gameObject, 5f);
	}

	public void TakeDamage(int damage) {
		AudioManager.Instance.PlaySingle(hitSound, 0.3f);

		Health -= damage;
		if (Health <= 0) {
			// Do the death things
			Destroy(gameObject);
		}
	}

	private void OnTriggerEnter2D(Collider2D collision) {
		if (collision.gameObject.name == "Attack") {
			collision.gameObject.SetActive(false);
			TakeDamage(5);
		}
	}

}
