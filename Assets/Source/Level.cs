using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
	public GameObject enemy;

	public void Spawn() {
		var spawn = Instantiate(enemy, transform);

		spawn.transform.position = new Vector3(0, 0, 0);
	}
}
