using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End : MonoBehaviour
{
	public GameObject morningstar;
	public GameObject touchDown;
	public SpriteRenderer holeBg;
	public SpriteRenderer holeFe;
	public SpriteRenderer endScene;
	public Follow follow;

	private bool fall = false;
	private bool stop = false;
	private bool end = false;


	public void Start() {
		holeBg.material.color = new Color(holeBg.material.color.r, holeBg.material.color.g, holeBg.material.color.b, 0);
		holeFe.material.color = new Color(holeFe.material.color.r, holeFe.material.color.g, holeFe.material.color.b, 0);
		endScene.material.color = new Color(endScene.material.color.r, endScene.material.color.g, endScene.material.color.b, 0);

		StartCoroutine(FadeIn());
	}

	public void FixedUpdate() {

		if (fall && !stop) {
			morningstar.transform.position -= new Vector3(0, 4 * Time.deltaTime, 0);

			if (morningstar.transform.position.y < 0) {
				follow.StartFollow();
			}

			if (morningstar.transform.position.y < -15) {
				morningstar.SetActive(false);
				stop = true;
			}
		}

		if (stop && !end) {
			var td = Instantiate(touchDown, transform);
			td.transform.position = morningstar.transform.position;

			end = true;
			StartCoroutine(StartEnd());
		}

	}

	private IEnumerator StartEnd() {
		yield return new WaitForSeconds(3);

		for (var i = 0f; i <= 1; i += (Time.deltaTime / 3)) {

			endScene.material.color = new Color(endScene.material.color.r, endScene.material.color.g, endScene.material.color.b, i);

			yield return null;
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
