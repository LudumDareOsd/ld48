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

public class LevelManager : Singleton<LevelManager> {
	public List<Level> levels;
	public GameObject lostSoul;
	private Level currentLevel = null;
	private bool lost = false;
	private float lostTimeOut = 1f;
	private Transform enemyContainer;

	public void Awake() {
		StartCoroutine(LevelTransition(0));
		enemyContainer = transform.Find("EnemyContainer");
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

	private void SpawnSoul() {
		var spawn = Instantiate(lostSoul, transform);
		var ienemy = spawn.GetComponent<IEnemy>();
		ienemy.Spawn();
		
		spawn.transform.SetParent(enemyContainer);
	}

	private IEnumerator LoadLevel(int i) {
		Debug.Log("Switching to level " + i);

		currentLevel = levels[i];

		yield return new WaitForSeconds(currentLevel.levelDuration);

		if (++i >= levels.Count) {
			Debug.LogError("Switched to a level not set in LevelManager");
			yield break;
		}

		StartCoroutine(LevelTransition(i));
	}

	private IEnumerator LevelTransition(int i) {
		Debug.Log("Transition to level " + i);

		BackgroundManager.Instance.ToggleFog(levels[i].hasFog);

		switch (i) {
			case 0: {
					TextManager.Instance.ShowText("LIMBO");
					BackgroundManager.Instance.LoadBackGround(1);
					break;
				}
			case 1: {
					TextManager.Instance.ShowText("LUST");
					SpawnSoul();
					break;
				}
			case 2: {
					TextManager.Instance.ShowText("GLUTTONY");
					BackgroundManager.Instance.LoadBackGround(2);
					break;
				}
			case 3: {
					TextManager.Instance.ShowText("GREED");
					BackgroundManager.Instance.LoadBackGround(3);
					BackgroundManager.Instance.UnLoadBackGround(1);
					break;
				}
			case 4: {
					TextManager.Instance.ShowText("ANGER");
					SpawnSoul();
					BackgroundManager.Instance.LoadBackGround(4);
					BackgroundManager.Instance.UnLoadBackGround(2);
					break;
				}
			case 5: {
					TextManager.Instance.ShowText("HERESY");
					BackgroundManager.Instance.LoadBackGround(5);
					BackgroundManager.Instance.UnLoadBackGround(3);
					break;
				}
			case 6: {
					TextManager.Instance.ShowText("VIOLENCE");
					SpawnSoul();
					BackgroundManager.Instance.LoadBackGround(6);
					BackgroundManager.Instance.UnLoadBackGround(4);
					break;
				}
			case 7: {
					TextManager.Instance.ShowText("FRAUD");
					BackgroundManager.Instance.UnLoadBackGround(5);
					BackgroundManager.Instance.UnLoadBackGround(6);
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
