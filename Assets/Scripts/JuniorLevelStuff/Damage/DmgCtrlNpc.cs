using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DmgCtrlNpc : DamageController
{
	public RectTransform	hpBar;
	[Tooltip("That for hp slider over head, need for npc, no need for player")]
	public Image 	hpProgress;
	public Canvas canvas;


	protected override void Visualize()
	{
		if (canvas == null)
			return;
		
		// I have removed null checks here because they're expensive
		// That's why this try catch block is here 
		try
		{
			// Refreshes health bar over head
			hpProgress.fillAmount = (float)currentHp / maxHp;
			if (currentHp < maxHp)
				hpBar.gameObject.SetActive(true);
			else
				hpBar.gameObject.SetActive(false);

			// To keep that slider facing to player
			Transform cameraTransf = Camera.main.transform;
			canvas.transform.LookAt(canvas.transform.position + cameraTransf.rotation * Vector3.forward,
				cameraTransf.rotation * Vector3.up);
		}
		catch
		{
			Debug.Log("Something vent wrong");
		}
	}

}
