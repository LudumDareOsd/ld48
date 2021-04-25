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
	public List<AudioClip> music;
	public GameObject lostSoul;
	public SpriteRenderer fadeToBlack;
	private AudioSource audioSource;
	private Level currentLevel = null;
	private bool lost = false;
	private float lostTimeOut = 1f;
	private Transform enemyContainer;
	private bool tickLevel = false;

	public void Start() {
		enemyContainer = transform.Find("EnemyContainer");
		audioSource = GameObject.Find("MusicPlayer").GetComponent<AudioSource>();

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

		if (tickLevel) currentLevel.Tick();
		TextManager.Instance.AddScore(1);
	}

	public void Lost() {
		lost = true;
		lostTimeOut = 1f;
		TextManager.Instance.ShowLostText();
	}

	private IEnumerator FadeToBlack() {
		for (var i = 0f; i <= 1; i += Time.deltaTime/4) {
			fadeToBlack.color = new Color(fadeToBlack.color.r, fadeToBlack.color.g, fadeToBlack.color.b, i);

			yield return null;
		}

		yield return new WaitForSeconds(2);

		SceneManager.LoadScene("final");
	}

	private void SpawnSoul() {
		var spawn = Instantiate(lostSoul, transform);
		var ienemy = spawn.GetComponent<IEnemy>();
		ienemy.Spawn();

		spawn.transform.SetParent(enemyContainer);
	}

	private IEnumerator LoadLevel(int i) {
		if (lost) {
			StopAllCoroutines();
			yield return null;
		}

		Debug.Log("Switching to level " + i);

		currentLevel = levels[i];

		// only tick level inside duration
		tickLevel = true;
		yield return new WaitForSeconds(currentLevel.levelDuration);
		tickLevel = false;

		currentLevel.Despawn();

		if (++i >= levels.Count) {
			Debug.LogError("Switched to a level not set in LevelManager");
			yield break;
		}

		StartCoroutine(LevelTransition(i));
	}

	private IEnumerator LevelTransition(int i) {
		if (lost) {
			StopAllCoroutines();
			yield return null;
		}

		Debug.Log("Transition to level " + i);

		BackgroundManager.Instance.ToggleFog(levels[i].hasFog);

		switch (i) {
			case 0: {
					TextManager.Instance.ShowText("LIMBO");
					audioSource.clip = music[0];
					audioSource.Play();
					SpawnSoul();
					BackgroundManager.Instance.LoadBackGround(1);
					break;
				}
			case 1: {
					TextManager.Instance.ShowText("LUST");
					audioSource.clip = music[2];
					audioSource.Play();
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
					audioSource.clip = music[1];
					audioSource.Play();
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
					audioSource.clip = music[4];
					audioSource.Play();

					TextManager.Instance.DisableScore();
					StartCoroutine(FadeToBlack());

					break;
				}
			case 8: {
					

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
