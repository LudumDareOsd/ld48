using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
	public GameObject follow;
	private bool start = false;

	public void FixedUpdate() {

		if (start) {
			Camera.main.transform.position = new Vector3(follow.transform.position.x, follow.transform.position.y, Camera.main.transform.position.z);
		}
		

	}

	public void StartFollow() {
		start = true;
	}

}
