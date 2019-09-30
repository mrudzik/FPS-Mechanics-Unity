using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharData : MonoBehaviour
{
	public DamageController damageController;
		//int 	_hp = 10;
	public int 	ammo = 50;

	public Text	hpText;
	public Text	ammoText;
	public Text	pistolText;
	public Text	rifleText;

	
	public bool HavePistol { get; set; }
	public bool HaveRifle { get; set; }


	[HideInInspector]
	public bool changedState = true;

	private void Update()
	{
		UpdateUI();
	}
	
	private void 	UpdateUI()
	{
		if (!changedState)
			return;
		// Optimization
		changedState = false;

		hpText.text = 		"HP:   " + damageController.currentHp;
		ammoText.text = 	"Ammo: " + ammo;

		pistolText.gameObject.SetActive(HavePistol);
		rifleText.gameObject.SetActive(HaveRifle);
		
	}
}
