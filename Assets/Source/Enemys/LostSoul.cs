using UnityEngine;
using System.Collections;

public class LostSoul : MonoBehaviour, IEnemy
{
	public int Health { get; set; }
	public float transitionTime = 4.5f;
	public AudioClip hitSound;
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

	public void Spawn(Vector3 pos)
	{
		Health = 20;
		transform.position = pos;
		StartCoroutine(LerpPosition(new Vector3(Random.Range(-6.5f, 6.5f), Random.Range(-3.5f, 3.5f), 0), transitionTime));
	}

	public void TakeDamage(int damage)
	{
		AudioManager.Instance.PlaySingle(hitSound, 0.3f);

		Health -= damage;
		if (Health <= 0)
		{
			GameObject.Find("Player").GetComponent<Player>().AddHealth(1);
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
