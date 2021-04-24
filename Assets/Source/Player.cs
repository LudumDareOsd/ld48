using UnityEngine;

public class Player : MonoBehaviour {
	private Vector3 relativePosition = Vector3.zero;
	private float moveSpeed = 8f;
	private float moveSpeedY = 6f;
	private float accelerationSpeed = 3f;
	private Rigidbody2D rb;
	private SpriteRenderer sr;

	public Sprite left;
	public Sprite right;
	public Sprite straight;

	public void Awake() {
		rb = GetComponent<Rigidbody2D>();
		sr = GetComponentInChildren<SpriteRenderer>();
	}

	public void FixedUpdate() {

		var xAxis = Input.GetAxis("Horizontal");
		setSprite(xAxis);

		rb.velocity += new Vector2(accelerationSpeed * xAxis, accelerationSpeed * Input.GetAxis("Vertical"));

		if (rb.velocity.x > moveSpeed) {
			rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
		}

		if (rb.velocity.x < -moveSpeed) {
			rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
		}

		if (rb.velocity.y > moveSpeedY) {
			rb.velocity = new Vector2(rb.velocity.x, moveSpeedY);
		}

		if (rb.velocity.y < -moveSpeedY) {
			rb.velocity = new Vector2(rb.velocity.x, -moveSpeedY);
		}

		rb.velocity = rb.velocity * 0.85f;
	}

	private void setSprite(float xAxis) {

		if (xAxis > 0.2) {
			sr.sprite = right;
		} else if (xAxis < -0.2) {
			sr.sprite = left;
		} else {
			sr.sprite = straight;
		}

	}
}
