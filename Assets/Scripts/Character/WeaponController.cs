using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
	public CharData	ourCharacter;
	public Weapon 	pistolObject;
	public Weapon 	rifleObject;

	private Weapon _currentWeapon;
	public 	DodgeRay 	dodgeLogic;
	
	private void 	HideAllWeapons()
	{
		_currentWeapon = null;
		pistolObject.gameObject.SetActive(false);
		rifleObject.gameObject.SetActive(false);
		ourCharacter.changedState = true;
	}
	
	private void 	UnholsterWeapon(Weapon currGun)
	{
		currGun.gameObject.SetActive(true);
		// TODO Make animation here
		_currentWeapon = currGun;
	}
	
	private void 	ChangeWeapon()
	{
		if (Input.GetKey("1"))
		{
			HideAllWeapons();
		}	
		if (Input.GetKey("2") && ourCharacter.HavePistol)
		{
			HideAllWeapons();
			UnholsterWeapon(pistolObject);
		}
		if (Input.GetKey("3") && ourCharacter.HaveRifle)
		{
			HideAllWeapons();
			UnholsterWeapon(rifleObject);
		}
	}
	
	private void 	ShootLogic()
	{
		if (Input.GetMouseButtonDown(0))
		{
			if (_currentWeapon != null)
			{
				_currentWeapon.Shoot();
				dodgeLogic.Shoot();
			}
		}
	}
	
	private void Update()
	{	
		ChangeWeapon();
		ShootLogic();
	}
	
	private void Start()
	{
		HideAllWeapons();
	}
}
