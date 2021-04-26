using UnityEngine;

public class GoldRock : BaseEnemy, IEnemy
{
	private Rigidbody2D rb;

	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
		Destroy(gameObject, 10f);
	}

	private void Update() {
	}

	public void Spawn() {
		Health = 1;

		var side = Random.Range(0, 3);

		switch (side) {
			case 0:
				transform.position = new Vector3(Random.Range(-6.5f, 6.5f), 5f, 0f);
				break;
			case 1:
				transform.position = new Vector3(Random.Range(-6.5f, 6.5f), -5f, 0f);
				break;
			case 2:
				transform.position = new Vector3(8f, Random.Range(-6.5f, 6.5f), 0f);
				break;
			case 3:
				transform.position = new Vector3(-8f, Random.Range(-6.5f, 6.5f), 0f);
				break;
		}

		var x = Random.Range(-4f, 4f);
		var y = Random.Range(-4f, 4f);

		var target = new Vector3(x, y, 0);

		var direction = target - transform.position;

		rb.velocity = (direction.normalized * Random.Range(3f, 5f));
		transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, Random.Range(0f, 360f)));
	}

	public void Despawn()
	{
	}

	public void TakeDamage(int damage) {
		// Invulnerable
	}
}
