using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
	public AudioClip		menuMusic;
	public AudioClip		deathMusic;
	public AudioSource		source;
	
	
	public void     PlayMenu(bool state)
	{
		if (!state)
		{
			source.Stop();
			return;
		}
		source.clip = menuMusic;
		source.Play();
	}
	
	public void     PlayDeath()
	{
		source.clip = deathMusic;
		source.Play();
	}
	
	
	
}
