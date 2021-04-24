using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
	private float height;
	private float startPos;

	public Vector3 pos = Vector3.zero;
	public float parallaxEffect;

    void Start()
    {
		startPos = transform.position.y;
		height = GetComponent<SpriteRenderer>().bounds.size.y;
    }

    void FixedUpdate()
    {
		pos -= new Vector3(0, 0.1f, 0);

		var lastPos = pos.y * (1 - parallaxEffect);
		var dist = pos.y * parallaxEffect;
		transform.position = new Vector3(transform.position.x, startPos + dist, transform.position.z);

		if (lastPos > startPos + height) {
			startPos = 0f;
			pos = Vector3.zero;
		} 
    }
}
