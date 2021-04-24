using UnityEngine;
using TMPro;
using System.Collections;

public class TextManager : MonoBehaviour
{
	public TextMeshProUGUI main;
	public TextMeshProUGUI scoreTxt;
	private int score = 0;

	public void Awake() {
		scoreTxt.text = "100";
		StartCoroutine(FadeInMain());
	}

	public void AddScore(int points) {
		score += points;
		scoreTxt.text = score.ToString();
	}

	private void Update() {
		AddScore(1);
	}

	private IEnumerator FadeInMain() {
		
		for (var i = 0f; i <= 1; i += Time.deltaTime) {
			main.color = new Color(main.color.r, main.color.g, main.color.b, i);

			yield return null;
		}

		yield return new WaitForSeconds(2);

		yield return FadeOutMain();
	}

	private IEnumerator FadeOutMain() {
		for (var i = 1f; i >= 0; i -= Time.deltaTime) {
			main.color = new Color(main.color.r, main.color.g, main.color.b, i);

			yield return null;
		}
	}
}
