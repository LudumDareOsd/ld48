using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


/**
 * alla levels har souls powerup
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

		if (!currentLevel) return;
		currentLevel.Tick();
		TextManager.Instance.AddScore(1);
	}

	public void Lost() {
		lost = true;
		lostTimeOut = 1f;
		TextManager.Instance.ShowLostText();
	}
	
	private IEnumerator LoadLevel(int i) {
		Debug.Log("Switching to level " + i);

		currentLevel = levels[i];

		yield return new WaitForSeconds(currentLevel.levelDuration);

		if (++i >= levels.Count)
		{
			Debug.LogError("Switched to a level not set in LevelManager");
			yield break;
		}

		StartCoroutine(LevelTransition(i));
	}

	private IEnumerator LevelTransition(int i)
	{
		Debug.Log("Transition to level " + i);

		BackgroundManager.Instance.ToggleFog(levels[i].hasFog);

		switch (i)
		{
			case 0: {
				// transition to first level etc
				break;
			}
			case 1: {
				//
				break;
			}
			case 2: {
				//
				break;
			}
			case 9: {
				// transition to hell
				Debug.Log("YOU WIN!");
				break;
			}
			default:
				Debug.Log("Unhandled LevelTransition: " + i);
				i = 0;
				break;
		}

		yield return new WaitForSeconds(5f);

		StartCoroutine(LoadLevel(i));
	}
}
