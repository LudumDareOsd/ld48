using UnityEngine;
using TMPro;
using System.Collections;

public class TextManager : Singleton<TextManager>
{
	public TextMeshProUGUI main;
	public TextMeshProUGUI scoreTxt;
	private int score = 0;

	public void Awake() {
		scoreTxt.text = "100";
	}

	public void AddScore(int points) {
		score += points;
		scoreTxt.text = score.ToString();
	}

	public void ShowLostText() {
		main.text = "PRESS ANY KEY TO RESTART";
		StartCoroutine(PulseMain());
	}

	public void ShowText(string text) {
		main.text = text;
		StartCoroutine(FadeInMain());
	}

	private IEnumerator PulseMain() {
		for (var i = 0f; i <= 1; i += Time.deltaTime) {
			main.color = new Color(main.color.r, main.color.g, main.color.b, i);

			yield return null;
		}

		for (var i = 1f; i >= 0; i -= Time.deltaTime) {
			main.color = new Color(main.color.r, main.color.g, main.color.b, i);

			yield return null;
		}

		StartCoroutine(PulseMain());
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
