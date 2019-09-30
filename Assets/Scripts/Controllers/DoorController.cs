using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
	public float 		doorSpeed = 5;
	public Transform	door;

	public Transform	doorOpen;
	public Transform	doorClose;
	public AudioSource 	soundSource;
	
	private bool 	_doorOpened = false;
	private bool 	_doorActive = false;
	
	public void 	Functional()
	{
		DoorFunctional();
	}
	
	private void		DoorFunctional()
	{
		_doorActive = true;
		_doorOpened = !_doorOpened;
		soundSource.Play();
	}
	
	private void 	MoveDoor()
	{
		if (!_doorActive)
			return;
		
		// TODO Make universal door, that could be opened in any directions
		if (_doorOpened)
		{
			door.Translate(1f * Time.deltaTime * doorSpeed, 0 ,0);
			if (door.position.x >= doorOpen.position.x)
			{	// Door is complete open
				_doorActive = false;
				door.position = doorOpen.position;
			}
		}
		else
		{
			door.Translate(-1f * Time.deltaTime * doorSpeed,0,0);
			if (door.position.x < doorClose.position.x)
			{	// Door is complete closed
				_doorActive = false;
				door.position = doorClose.position;
			}	
		}
	}
	
	private void 	Update()
	{
		MoveDoor();
	}
}
