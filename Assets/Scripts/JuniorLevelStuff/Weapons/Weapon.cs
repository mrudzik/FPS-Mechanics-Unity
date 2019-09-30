using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
	public Rigidbody     projectile;
	
	public float	shootSpeed = 2f;
	private float 	coolDownTimer = 0f;
	
	public float	bulletForce = 100f;
	public Transform 	endBarrel;

	public AudioSource		source;
	
	private void     Update()
	{
		if (coolDownTimer < shootSpeed)
			coolDownTimer += Time.deltaTime;
	}
	
	public virtual void 	Shoot()
	{
		if (coolDownTimer >= shootSpeed)
		{
			Rigidbody theBullet = Instantiate(projectile, endBarrel.position, endBarrel.rotation);//transform);
			theBullet.velocity = endBarrel.transform.forward * bulletForce;

			coolDownTimer = 0;
			source.Play();
		}
	}
}
