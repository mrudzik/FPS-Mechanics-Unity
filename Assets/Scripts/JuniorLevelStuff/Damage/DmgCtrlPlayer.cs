using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DmgCtrlPlayer : DamageController
{
	public CharData     ourPlayer;
	public AudioClip 	win;
	
	public override void TakeDamage(int damage)
	{
		base.TakeDamage(damage);
		ourPlayer.changedState = true;
		
	}

	public void 	WinSound()
	{
		soundSource.clip = win;
		soundSource.Play();
	}
}
