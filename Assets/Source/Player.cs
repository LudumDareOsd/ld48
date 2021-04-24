using UnityEngine;

public class Player : MonoBehaviour {
	private Vector3 relativePosition = Vector3.zero;
	private float moveSpeed = 8f;
	private float moveSpeedY = 6f;
	private float accelerationSpeed = 3f;
	private Rigidbody2D rb;
	private int health = 1;
	
	public SpriteRenderer srPlayer;
	public Sprite left;
	public Sprite right;
	public Sprite straight;
	
	public SpriteRenderer srSword;
	public Sprite swordShort;
	public Sprite swordMiddle;
	public Sprite swordLong;


	public void Awake() {
		rb = GetComponent<Rigidbody2D>();
		SetHealth(1);
	}

	public void FixedUpdate() {

		var xAxis = Input.GetAxis("Horizontal");
		SetPlayerSprite(xAxis);

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

	public void SetHealth(int health) {
		this.health = health;
		SetSwordSprite(health);
	}

	private void SetPlayerSprite(float xAxis) {

		if (xAxis > 0.2) {
			srPlayer.sprite = right;
		} else if (xAxis < -0.2) {
			srPlayer.sprite = left;
		} else {
			srPlayer.sprite = straight;
		}
	}

	private void SetSwordSprite(int health) {
		if (health == 1) {
			srSword.sprite = swordShort;
		} else if (health == 2) {
			srSword.sprite = swordMiddle;
		} else if (health > 2) {
			srSword.sprite = swordLong;
		}
	}
}
