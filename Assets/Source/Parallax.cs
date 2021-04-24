using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
	private float height;
	private float startPos;
	private Vector3 pos;

	public float parallaxEffect;

    void Start()
    {
		pos = transform.position;
		startPos = 0f;
		height = GetComponent<SpriteRenderer>().bounds.size.y;
    }

    void FixedUpdate()
    {
		pos -= new Vector3(0, -0.01f * parallaxEffect, 0);
		transform.position = pos;

		if (transform.position.y > startPos + height) {
			var delta = transform.position.y - (startPos + height);
			pos = new Vector3(0, startPos + delta, 0);
		}
    }
}
