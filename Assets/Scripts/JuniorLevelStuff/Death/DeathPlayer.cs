using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class DeathPlayer : Death
{
	public float 	deathTime = 2f;
	private float 	timer = 0f;

	public RectTransform 	deathUI;
	public RectTransform 	winUI;
	[HideInInspector] public bool 	scenarioDeath;
	private void Update()
	{
		if (isDead)
		{
			if (timer == 0f)
			{
//				Time.timeScale = 0;
				// Show death UI
				deathUI.gameObject.SetActive(true);
				
				// To not show UI when dead
//				CharData ourData = GetComponent<CharData>();
//				if (ourData != null)
//					ourData.enabled = false;
				// To not shoot when dead
				WeaponController ourWeapons = GetComponent<WeaponController>();
				if (ourWeapons != null)
					ourWeapons.enabled = false;
				// To not walk when dead
				FirstPersonController ourFPC = GetComponentInChildren<FirstPersonController>();
				if (ourFPC != null)
					ourFPC.enabled = false;
				
			}
			
			if (timer >= deathTime)
			{
				scenarioDeath = true;
			}
			timer += Time.deltaTime;
		}
	}

	
	
	
}
