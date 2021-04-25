using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashText : MonoBehaviour {
	public TextMeshProUGUI main;

	public void Awake() {
		StartCoroutine(PulseMain());
	}

	public void Update() {
		if (Input.anyKey) {
			SceneManager.LoadScene("main");
		}
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
}
