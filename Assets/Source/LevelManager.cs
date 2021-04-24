using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
	public List<Level> levels;
	private Level currentLevel = null;

	public void Awake() {
		StartCoroutine(LoadLevel(0));
	}

	public void Update() {
		currentLevel.Tick();
	}

	private IEnumerator LoadLevel(int i) {
		currentLevel = levels[0];

		yield return null;
		// yield return new WaitForSeconds(5);
	}
}
