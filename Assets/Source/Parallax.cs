using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
	private float height;
	private float width;
	private Vector3 startPos;
	private Vector3 pos = Vector3.zero;
	private SpriteRenderer[] spriteRenderers;
	private float targetXPosition = 0;
	private float horisontalDuration = 10f;
	private float horisontalTime = 0f;

	private IEnumerator pulseCoroutine;

	public float parallaxEffect;
	public bool enableHorisontalParalax = false;
	public bool enableFadePulse = false;

	private void Awake()
	{
		spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
	}

	void Start()
    {
		startPos = transform.position;
		height = spriteRenderers[0].bounds.size.y;
		width = spriteRenderers[0].bounds.size.y;
		targetXPosition = width;

		if (enableFadePulse)
		{
			//FadeIn(); // Always trigger from elsewhere?
		}
	}

    void FixedUpdate()
    {
		if (enableHorisontalParalax)
		{
			horisontalTime += Time.deltaTime;
			if (horisontalTime >= horisontalDuration)
			{
				horisontalTime = 0f;
				startPos.x = targetXPosition;
				targetXPosition = -targetXPosition;
			}
			var t = horisontalTime / horisontalDuration;
			pos.x = Mathf.SmoothStep(startPos.x, targetXPosition, t);
		}

		pos -= new Vector3(0, -0.01f * parallaxEffect, 0);
		transform.position = pos;

		if (transform.position.y > startPos.y + height) {
			var delta = transform.position.y - (startPos.y + height);
			pos = new Vector3(pos.x, startPos.y + delta, 0);
		}
    }

	public void StartFog()
	{
		Debug.Log("fadein??");
		StartCoroutine(FadeIn());
	}

	public void StopFog()
	{
		Debug.Log("FadeOut??");
		StartCoroutine(FadeOut());
	}

	IEnumerator FadeIn()
	{
		StartCoroutine(FadePulse(3f, 0f, 1f));
		yield return new WaitForSeconds(3f);
		Debug.Log("pulse??");
		pulseCoroutine = FadePulse(3f, 1f, 0.2f);
		StartCoroutine(pulseCoroutine);
	}

	IEnumerator FadeOut()
	{
		StopCoroutine(pulseCoroutine);
		StartCoroutine(FadePulse(3f, spriteRenderers[0].color.a, 0f));
		yield return new WaitForSeconds(3f);
	}

	IEnumerator FadePulse(float duration, float from, float to)
	{
		var time = 0f;
		while (true)
		{
			time = 0f;
			while (time < duration)
			{
				time += Time.deltaTime;
				var t = time / duration;
				var col = new Color(1f, 1f, 1f, Mathf.SmoothStep(from, to, t));
				for (var i = 0; i < spriteRenderers.Length; i++)
				{
					spriteRenderers[i].color = col;
				}
				yield return null;
			}

			// Wait a random time between pulses
			yield return new WaitForSeconds(Random.Range(2f, 5f));

			var temp = to;
			to = from;
			from = temp;
		}

	}

}
