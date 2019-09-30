using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCharacter : Weapon
{
	public CharData ourCharacter;

    public override void Shoot()
    {
	    if (ourCharacter.ammo <= 0)
		    return;
	    
	    base.Shoot();
		ourCharacter.ammo--;
		ourCharacter.changedState = true;
    }
}
