using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathNpc : Death
{
	private float     despawnTimer = 0f;

	[Tooltip("Using this for dismemberment. Put all body parts here")]
	public Rigidbody[] 	allParts;
	
	
	private void Update()
	{
		if (isDead)
		{
			if (despawnTimer == 0f)
			{
				foreach (Rigidbody body in allParts)
				{
					body.isKinematic = false;
					body.constraints = RigidbodyConstraints.None;
					// For funny "rag doll"
					body.AddForce(0f, 200f, 0f);
					body.AddTorque(Random.Range(10f, 200f),
						Random.Range(10f, 200f), Random.Range(10f, 200f));
					
				}
			}
			if (despawnTimer >= 3f)
				Destroy(gameObject);
			
			despawnTimer += Time.deltaTime;
		}
	}
}
