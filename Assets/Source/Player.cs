using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	private Vector3 relativePosition = Vector3.zero;
	private float moveSpeed = 8f;
	private float moveSpeedY = 6f;
	private float accelerationSpeed = 3f;
	private Rigidbody2D rb;
	private int health = 2;
	private float attackTime = 0f;
	private float attackTimeCd = 0f;
	private bool attackPrio = true;
	private float invulnerableTimer = 0f;
	private float blinkTimer = 0f;

	public List<AudioClip> swordSwings;
	public AudioClip death;
	public AudioClip damage;

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

	public Sprite backSprite;

	public GameObject attack;

	public void Awake() {
		rb = GetComponent<Rigidbody2D>();
	}

	public void Update() {
		var xAxis = Input.GetAxis("Horizontal");

		if ((Input.GetButtonDown("Fire1") || Input.GetKeyDown("space")) && attackTimeCd <= 0) {
			attackTime = 0.2f;
			attackTimeCd = 0.5f;

			Attack(xAxis);
		}

		if (attackTimeCd > 0) {
			attackTimeCd -= Time.deltaTime;
		}
	}

	public void FixedUpdate() {

		CheckInvulnerable();

		var xAxis = Input.GetAxis("Horizontal");
		var yAxis = Input.GetAxis("Vertical");

		rb.velocity += new Vector2(accelerationSpeed * xAxis, accelerationSpeed * yAxis);

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
			SetPlayerSprite(xAxis, yAxis);
			SetSwordSprite(health);
		}

		rb.velocity = rb.velocity * 0.85f;
	}

	private void OnTriggerEnter2D(Collider2D collision) {
		if (invulnerableTimer <= 0f) {
			if (collision.tag == "EnemyProjectile" || collision.gameObject.GetComponent<IEnemy>() != null)
			{
				health--;

				AudioManager.Instance.PlaySingle(damage, 0.2f);
				
				invulnerableTimer = 1f;
				srPlayer.enabled = false;
				srSword.enabled = false;

				if (health <= 0)
				{
					LevelManager.Instance.Lost();
					AudioManager.Instance.PlaySingle(death, 0.1f);
					Destroy(this);
				}
			}
		}
	}

	public void AddHealth(int health) {
		this.health += health;

		if (this.health >= 3) {
			this.health = 3;
		}
	}

	private void Attack(float xAxis) {

		if (invulnerableTimer >= 0f) {
			return;
		}

		AudioManager.Instance.PlayRandom(swordSwings, 0.3f);

		if (xAxis > 0) {
			AttackRight();
			attackPrio = !attackPrio;
		} else if (xAxis < 0) {
			AttackLeft();
			attackPrio = !attackPrio;
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

	private void CheckInvulnerable() {
		if (invulnerableTimer >= 0) {
			invulnerableTimer -= Time.deltaTime;
			blinkTimer -= Time.deltaTime;

			if (blinkTimer <= 0) {
				blinkTimer = 0.1f;
				srPlayer.enabled = !srPlayer.enabled;
			}
		} else {
			srPlayer.enabled = true;
			srSword.enabled = true;
		}
	}

	private void AttackLeft() {

		var collider = attack.GetComponent<BoxCollider2D>();

		if (health == 1) {
			srSword.sprite = swordShortLeft;

			collider.offset = new Vector2(-0.6f, -0.8f);
			collider.size = new Vector2(1f, 0.5f);
		} else if (health == 2) {
			srSword.sprite = swordMiddleLeft;

			collider.offset = new Vector2(-0.7f, -1.0f);
			collider.size = new Vector2(1.2f, 1f);
		} else if (health > 2) {
			srSword.sprite = swordLongLeft;

			collider.offset = new Vector2(-0.8f, -1.2f);
			collider.size = new Vector2(1.5f, 1.4f);
		}

		srPlayer.sprite = leftAttack;

		attack.SetActive(true);

	}

	private void AttackRight() {

		var collider = attack.GetComponent<BoxCollider2D>();


		if (health == 1) {
			srSword.sprite = swordShortRight;

			collider.offset = new Vector2(0.6f, -0.8f);
			collider.size = new Vector2(1, 0.5f);
		} else if (health == 2) {
			srSword.sprite = swordMiddleRight;

			collider.offset = new Vector2(0.7f, -1.0f);
			collider.size = new Vector2(1.2f, 1f);
		} else if (health > 2) {
			srSword.sprite = swordLongRight;

			collider.offset = new Vector2(0.8f, -1.2f);
			collider.size = new Vector2(1.5f, 1.4f);
		}

		srPlayer.sprite = rightAttack;

		attack.SetActive(true);

	}

	private void SetPlayerSprite(float xAxis, float yAxis) {

		if (xAxis > 0.2) {
			srPlayer.sprite = right;
		} else if (xAxis < -0.2) {
			srPlayer.sprite = left;
		} else if (yAxis > 0.2) {
			srPlayer.sprite = backSprite;
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
