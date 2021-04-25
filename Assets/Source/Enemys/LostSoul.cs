using UnityEngine;
using System.Collections;

public class LostSoul : BaseEnemy, IEnemy
{
	public float transitionTime = 4.5f;
	public AudioClip hitSound;

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
		//StartCoroutine(LerpPosition(new Vector3(Random.Range(-6.5f, 6.5f), Random.Range(-3.5f, 3.5f), 0), transitionTime));
		StartRandomMovement(transitionTime, 3f);
	}

	public void Despawn()
	{
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


}
