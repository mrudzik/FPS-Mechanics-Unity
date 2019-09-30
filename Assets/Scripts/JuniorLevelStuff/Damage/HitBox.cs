using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
	
	public	DamageController myHealth;
	public	int 	zoneDamage = 1;
	
	public void		RecieveDamage()
	{
		if (myHealth != null)
			myHealth.TakeDamage(zoneDamage);
		else
		{
			Debug.Log("Shiiet");
		}
	}


}
