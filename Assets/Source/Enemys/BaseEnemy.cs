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

	public void StopMoving()
	{
		if (handleCoroutine != null) StopCoroutine(handleCoroutine);
		if (moveCoroutine != null) StopCoroutine(moveCoroutine);
	}

	public void StartRandomMovement(float duration, float waitTime)
	{
		handleCoroutine = LerpRandomPosition(duration, waitTime);
		StartCoroutine(handleCoroutine);
	}

	private IEnumerator LerpPosition(Vector3 targetPosition, float duration)
	{
		var time = 0f;
		while (time < duration)
		{
			transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, duration);
			time += Time.deltaTime;
			yield return null;
		}
		yield break;
	}

	IEnumerator LerpRandomPosition(float moveDuration, float waitTime)
	{
		while (true)
		{
			yield return new WaitForSeconds(waitTime);
			moveCoroutine = LerpPosition(new Vector3(Random.Range(-6.5f, 6.5f), Random.Range(-3.5f, 3.5f), 0), moveDuration);
			StartCoroutine(moveCoroutine);
			yield return null;
		}
	}
}
