using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/**
 * alla levels, souls powerup på alla
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

	public void Awake() {
		StartCoroutine(LoadLevel(0));
	}

	public void Update() {
		if (lost) {
			if (Input.anyKey) {
				SceneManager.LoadScene("main");
			}
			return;
		}

		currentLevel.Tick();
		TextManager.Instance.AddScore(1);

		
	}

	public void Lost() {
		lost = true;
		TextManager.Instance.ShowLostText();
	}

	private IEnumerator LoadLevel(int i) {
		currentLevel = levels[i];

		yield return null;
		// yield return new WaitForSeconds(5);
	}
}
