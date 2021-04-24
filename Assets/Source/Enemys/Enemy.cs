using UnityEngine;
using System.Collections;

public interface IEnemy {

	int Health { get; set; }

	void Spawn(Vector3 pos);
	void TakeDamage(int damage);
}
