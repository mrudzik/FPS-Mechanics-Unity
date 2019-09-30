using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
	public float	lifeTime = 2f;
	private float	lifeTimer = 0f;
	
	private void 	DieWithEffects()
	{
		// TODO Effects
		Destroy(gameObject);
	}
	
	private void OnTriggerEnter(Collider other)
	{
		// TODO Important DAMAGE
		HitBox hit = other.GetComponent<HitBox>();
		if (hit != null)
			hit.RecieveDamage();
		
		DieWithEffects();	
	}

	private void Update()
	{
		if (lifeTimer >= lifeTime)
			DieWithEffects();
		
		lifeTimer += Time.deltaTime;
	}
}
