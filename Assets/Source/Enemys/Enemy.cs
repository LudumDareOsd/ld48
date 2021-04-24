using UnityEngine;
using System.Collections;

public interface IEnemy {

	int Health { get; set; }

	void Spawn();
	void TakeDamage(int damage);
}
