using UnityEngine;

public class Player : MonoBehaviour {
	private Vector3 relativePosition = Vector3.zero;
	private float moveSpeed = 8f;
	private float moveSpeedY = 6f;
	private float accelerationSpeed = 3f;
	private Rigidbody2D rb;
	private int health = 1;
	private float attackTime = 0f;
	private float attackTimeCd = 0f;
	private bool attackPrio = true;

	public SpriteRenderer srPlayer;
	public Sprite left;
	public Sprite leftAttack;
	public Sprite right;
	public Sprite rightAttack;
	public Sprite straight;
	
	public SpriteRenderer srSword;
	public Sprite swordShort;
	public Sprite swordMiddle;
	public Sprite swordLong;

	public Sprite swordShortLeft;
	public Sprite swordMiddleLeft;
	public Sprite swordLongLeft;

	public Sprite swordShortRight;
	public Sprite swordMiddleRight;
	public Sprite swordLongRight;

	public GameObject attack;

	public void Awake() {
		rb = GetComponent<Rigidbody2D>();
	}

	public void Update() {
		var xAxis = Input.GetAxis("Horizontal");

		if (Input.GetButtonDown("Fire1") && attackTimeCd <= 0) {
			attackTime = 0.2f;
			attackTimeCd = 0.5f;

			if (xAxis > 0) {
				AttackRight();
			} else if (xAxis < 0) {
				AttackLeft();
			} else {
				if (attackPrio) {
					attackPrio = !attackPrio;
					AttackLeft();
				} else {
					attackPrio = !attackPrio;
					AttackRight();
				}
			}
		}

		if (attackTimeCd > 0) {
			attackTimeCd -= Time.deltaTime;
		}
	}

	public void FixedUpdate() {

		var xAxis = Input.GetAxis("Horizontal");
		
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

		if (attackTime >= 0) {
			attackTime -= Time.deltaTime;
		} else {
			attack.SetActive(false);
			SetPlayerSprite(xAxis);
			SetSwordSprite(health);
		}

		rb.velocity = rb.velocity * 0.85f;
	}

	public void AddHealth(int health) {
		this.health += health;
	}
	
	private void AttackLeft() {
		srSword.sprite = swordShortLeft;
		srPlayer.sprite = leftAttack;

		attack.SetActive(true);

		var collider = attack.GetComponent<BoxCollider2D>();
		collider.offset = new Vector2(-0.6f, -0.8f);
		collider.size = new Vector2(0.7f, 0.2f);
	}

	private void AttackRight() {
		srSword.sprite = swordShortRight;
		srPlayer.sprite = rightAttack;

		attack.SetActive(true);

		var collider = attack.GetComponent<BoxCollider2D>();
		collider.offset = new Vector2(0.6f, -0.8f);
		collider.size = new Vector2(0.7f, 0.2f);
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
		//Debug.Log(health);

		if (health == 1) {
			srSword.sprite = swordShort;
		} else if (health == 2) {
			srSword.sprite = swordMiddle;
		} else if (health > 2) {
			srSword.sprite = swordLong;
		}
	}
}
