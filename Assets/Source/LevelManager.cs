using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


/**
 * alla levels, souls powerup p� alla
 * 
 * level 1, pagans 
 * level 2, dimma lust pagans
 * level 3, isregn
 * level 4, greed, guldbollar, lite som pagans med lite annan mekanik
 * level 5, anger, dark seraph
 * level 6, heracy, buring heratic lite som regn
 * level 7, violence, blodbollar mekanik??? lite homing?
 * level 8, fraud = darkness
 * level 9, finish game / dyk ner i avgrunden
 * 
 **/

public class LevelManager : Singleton<LevelManager>
{
	public List<Level> levels;
	private Level currentLevel = null;
	private bool lost = false;
	private float lostTimeOut = 1f;

	public void Awake() {
		StartCoroutine(LevelTransition(0));
	}

	public void Update() {
		if (lost) {
			if (lostTimeOut >= 0f) {
				lostTimeOut -= Time.deltaTime;
			} else if (Input.anyKey) {
				SceneManager.LoadScene("main");
			}
			return;
		}

		if (currentLevel == null) return;
		currentLevel.Tick();
		TextManager.Instance.AddScore(1);
	}

	public void Lost() {
		lost = true;
		lostTimeOut = 1f;
		TextManager.Instance.ShowLostText();
	}
	
	private IEnumerator LoadLevel(int i) {
		currentLevel = levels[i];

		yield return new WaitForSeconds(currentLevel.levelDuration);

		StartCoroutine(LevelTransition(++i));
	}

	private IEnumerator LevelTransition(int i)
	{
		Debug.Log("Transition to level " + i);

		switch (i)
		{
			case 0: {
					// transition to level 1
					BackgroundManager.Instance.ToggleFog(true);
				break;
			}
			case 1: {
					// transition to level 2
					BackgroundManager.Instance.ToggleFog(false);
					break;
			}
			case 9: {
				// transition to level  WIN GAME?!?!?
				Debug.Log("YOU WIN!");
				break;
			}
			default:
				Debug.LogError("Unhandled LevelTransition");
				i = 0;
				break;
		}

		yield return new WaitForSeconds(5f);

		StartCoroutine(LoadLevel(i));
	}
}
