using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponItem : Item
{
	public bool	unlockPistol;
	public bool	unlockRifle;
	public int	giveAmmo = 10;
	public CharData ourCharacter;
	
	// We can toss character data to Action from Interact script
	// with that, we can have multiple characters witch can pick items
	public override void Action()
	{
		ourCharacter.changedState = true;
		if (unlockPistol)
			ourCharacter.HavePistol = true;
		if (unlockRifle)
			ourCharacter.HaveRifle = true;
		if (giveAmmo > 0)
			ourCharacter.ammo += giveAmmo;
		
		Destroy(gameObject);
	}
}
