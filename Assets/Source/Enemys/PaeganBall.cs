using UnityEngine;
using System.Collections;

public class PaeganBall : MonoBehaviour, IEnemy {

	public int Health { get; set; }

	private float ySpeed = 1f;
	private float spinSpeed = 0.03f;
	private float xTransitionTime = 3.5f;

	private void Start()
	{
		ySpeed = Random.Range(0.6f, 1.2f);
		spinSpeed = Random.Range(-0.04f, 0.04f);
	}

	private void Update()
	{
		var step = ySpeed * Time.deltaTime;
		transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, 6.5f, 0), step);
		var spinStep = spinSpeed * Time.deltaTime;
		transform.Rotate(Vector3.forward * spinSpeed);

		if (transform.position.y > 6f) {
			Destroy(gameObject);
		}
	}

	public void Spawn()
	{
		Health = 10;
		transform.position = new Vector3(Random.Range(-6.5f, 6.5f), -5f, 0f);
		StartCoroutine(LerpXPosition(Random.Range(-6.5f, 6.5f), xTransitionTime));
	}

	public void TakeDamage(int damage)
	{
		Health -= damage;
		if (Health <= 0)
		{
			// Do the death things
			Destroy(gameObject);
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		TakeDamage(5);
	}

	IEnumerator LerpXPosition(float targetXPosition, float duration)
	{
		var time = 0f;
		var startTime = Time.time;
		var startPosition = transform.position.x;

		while (time < duration)
		{
			var t = (Time.time - startTime) / duration;
			transform.position = new Vector3(Mathf.SmoothStep(startPosition, targetXPosition, t), transform.position.y, transform.position.z);
			time += Time.deltaTime;
			yield return null;
		}
		StartCoroutine(LerpXPosition(Random.Range(-6.5f, 6.5f), xTransitionTime));

	}

}
