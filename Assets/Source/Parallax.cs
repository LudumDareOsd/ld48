using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
	private float height;
	private float startPos;

	public GameObject cam;
	public float parallaxEffect;

    void Start()
    {
		startPos = transform.position.y;
		height = GetComponent<SpriteRenderer>().bounds.size.y;
    }

    void FixedUpdate()
    {
		var lastPos = cam.transform.position.y * (1 - parallaxEffect);
		var dist = cam.transform.position.y * parallaxEffect;
		transform.position = new Vector3(transform.position.x, startPos + dist, transform.position.z);

		if (lastPos > startPos + height) {
			startPos += height;
		} else if (lastPos < startPos - height) {
			startPos -= height;
		}
    }
}
