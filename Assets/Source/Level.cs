using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
	public GameObject[] enemies;
	public float levelDuration = 60f;
	public float spawnCooldown = 5f;
	public bool hasFog = false;

	private Transform enemyContainer;
	private float spawnCounter = 0f;

	private void Start()
	{
		enemyContainer = LevelManager.Instance.transform.Find("EnemyContainer");
	}

	public void Tick()
	{
		spawnCounter += Time.deltaTime;
		if (spawnCounter >= spawnCooldown) {
			spawnCounter = 0f;
			Spawn();
		}
	}

	private void Spawn() {
		var enemy = enemies[Random.Range(0, enemies.Length)];
		var spawn = Instantiate(enemy, transform);
		var ienemy = spawn.GetComponent<IEnemy>();
		ienemy.Spawn();
		spawn.transform.SetParent(enemyContainer);
	}
}
