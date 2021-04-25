using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End : MonoBehaviour
{
	public GameObject morningstar;
	public SpriteRenderer holeBg;
	public SpriteRenderer holeFe;
	public Follow follow;

	private bool fall = false;

	public void Start() {
		holeBg.material.color = new Color(holeBg.material.color.r, holeBg.material.color.g, holeBg.material.color.b, 0);
		holeFe.material.color = new Color(holeFe.material.color.r, holeFe.material.color.g, holeFe.material.color.b, 0);

		StartCoroutine(FadeIn());
	}

	public void FixedUpdate() {
		if (fall) {
			morningstar.transform.position -= new Vector3(0, 4 * Time.deltaTime, 0);

			if (morningstar.transform.position.y < 0) {
				follow.StartFollow();
			}
		}

	}

	private IEnumerator FadeIn() {
		yield return new WaitForSeconds(1);

		for (var i = 0f; i <= 1; i += (Time.deltaTime/4)) {
		
			holeBg.material.color = new Color(holeBg.material.color.r, holeBg.material.color.g, holeBg.material.color.b, i);
			
			yield return null;
		}

		holeFe.material.color = new Color(holeFe.material.color.r, holeFe.material.color.g, holeFe.material.color.b, 1);
		fall = true;
	}

}
