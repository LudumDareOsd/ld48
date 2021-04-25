using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
	public int Health { get; set; }

	protected IEnumerator moveCoroutine;
	protected IEnumerator handleCoroutine;

	protected Vector3 velocity = Vector3.zero;

	public void MoveTo(Vector3 point, float duration)
	{
		StopMoving();
		moveCoroutine = LerpPosition(point, duration);
		StartCoroutine(moveCoroutine);
	}

	public void StartRandomMovement(float duration, float waitTime, float yMin = -3.5f, float yMax = 3.5f)
	{
		handleCoroutine = LerpRandomPosition(duration, waitTime, yMin, yMax);
		StartCoroutine(handleCoroutine);
	}

	public void StopMoving()
	{
		if (handleCoroutine != null) StopCoroutine(handleCoroutine);
		if (moveCoroutine != null) StopCoroutine(moveCoroutine);
	}

	private IEnumerator LerpPosition(Vector3 targetPosition, float duration)
	{
		var time = 0f;
		var startPosition = transform.position;
		while (time < duration)
		{
			time += Time.deltaTime;
			var t = time / duration;
			transform.position = Vector3.Lerp(startPosition, targetPosition, Mathf.SmoothStep(0.0f, 1.0f, t));
			yield return null;
		}
		yield break;
	}

	IEnumerator LerpRandomPosition(float duration, float waitTime, float yMin = -3.5f, float yMax = 3.5f)
	{
		while (true)
		{
			yield return new WaitForSeconds(waitTime);
			moveCoroutine = LerpPosition(new Vector3(Random.Range(-6.5f, 6.5f), Random.Range(yMin, yMax), 0), duration);
			StartCoroutine(moveCoroutine);
			yield return null;
		}
	}
}
