using UnityEngine;
using System.Collections;

public class LostSoul : MonoBehaviour, IEnemy
{
	public int Health { get; set; }
	public float transitionTime = 4.5f;

	private Vector3 velocity = Vector3.zero;

	private void Start()
	{
	}

	private void Update()
	{
		if (transform.position.y > 6f)
		{
			Destroy(gameObject);
		}
	}

	public void Spawn()
	{
		Health = 20;
		transform.position = new Vector3(Random.Range(-6.5f, 6.5f), -5f, 0f);
		StartCoroutine(LerpPosition(new Vector3(Random.Range(-6.5f, 6.5f), Random.Range(-3.5f, 3.5f), 0), transitionTime));
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

	IEnumerator LerpPosition(Vector3 targetPosition, float duration)
	{
		var time = 0f;
		var startPosition = transform.position.x;

		while (time < duration)
		{
			transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, duration);
			time += Time.deltaTime;
			yield return null;
		}
		StartCoroutine(LerpPosition(new Vector3(Random.Range(-6.5f, 6.5f), Random.Range(-3.5f, 3.5f), 0), transitionTime));
	}

}
