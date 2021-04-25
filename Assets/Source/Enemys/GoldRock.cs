using UnityEngine;

public class GoldRock : MonoBehaviour {
	public int Health { get; set; }
	
	private Rigidbody2D rb;

	private void Start() {
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
			case 3:
				transform.position = new Vector3(8f, Random.Range(-6.5f, 6.5f), 0f);
				break;
			case 4:
				transform.position = new Vector3(-8f, Random.Range(-6.5f, 6.5f), 0f);
				break;
		}

		var x = Random.Range(-4f, 4f);
		var y = Random.Range(-4f, 4f);

		var target = new Vector3(x, y, 0);

		var direction = transform.position - target;

		rb.velocity = (direction.normalized * Random.Range(3f, 5f));
	}

	public void TakeDamage(int damage) {
		// Invulnerable
	}
}
