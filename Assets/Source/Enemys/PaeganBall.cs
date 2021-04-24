using UnityEngine;
using System.Collections;

public class PaeganBall : MonoBehaviour, IEnemy {

	public int Health { get; set; }
	public AudioClip hitSound;
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
		//float step = xSpeed * Time.deltaTime; // calculate distance to move
		var step = ySpeed * Time.deltaTime;
		transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, 6.5f, 0), step);
		var spinStep = spinSpeed * Time.deltaTime;
		transform.Rotate(Vector3.forward * spinSpeed);

		if (transform.position.y > 6f) {
			Destroy(gameObject);
		}
	}

	public void Spawn(Vector3 pos)
	{
		Health = 10;
		transform.position = pos;
		StartCoroutine(LerpXPosition(Random.Range(-6.5f, 6.5f), xTransitionTime));
	}

	public void TakeDamage(int damage)
	{
		AudioManager.Instance.PlaySingle(hitSound, 0.3f);

		Health -= damage;
		if (Health <= 0)
		{
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
