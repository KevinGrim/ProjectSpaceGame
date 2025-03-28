using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
	public float health = 10f;
	public ParticleSystem deathExplosion;

	public void TakeDamage(float amount)
	{
		health -= amount;
		if (health <= 0f)
		{
			Death();
		}
	}

	void Death()
	{
		if (deathExplosion != null)
		{
			ParticleSystem explosion = Instantiate(deathExplosion, transform.position, transform.rotation);
			explosion.transform.localScale = transform.localScale; // Skaliert die Explosion entsprechend dem Enemy
			explosion.Play();
			Destroy(explosion.gameObject, explosion.main.duration + explosion.main.startLifetime.constantMax);
		}

		Destroy(gameObject);
	}
}
