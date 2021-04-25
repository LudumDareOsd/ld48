using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodBullet : MonoBehaviour
{
	private Transform target;
	private Transform sprite;
	private float speed = 3f;
	private float rotateSpeed = 120f;
	private Rigidbody2D rb;

	void Start()
    {
		rb = GetComponent<Rigidbody2D>();
		target = GameObject.FindObjectOfType<Player>() ? GameObject.FindObjectOfType<Player>().transform : null;
		sprite = transform.GetChild(0);
		
		// mostly so they dont clump up some randomness
		speed = Random.Range(2.5f, 3.5f);
		rotateSpeed = Random.Range(110f, 180f); 
	}

	void FixedUpdate()
	{
		if (target == null)
		{
			Destroy(gameObject);
			return;
		}

		var direction = (Vector2)target.position - rb.position;
		direction.Normalize();

		var rotateAmount = Vector3.Cross(direction, transform.up).z;
		rb.angularVelocity = -rotateAmount * rotateSpeed;
		rb.velocity = transform.up * speed;
		sprite.rotation = Quaternion.identity;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.name == "Attack" || collision.tag == "Player")
		{
			Destroy(gameObject);
		}
	}
}
