using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
	[SerializeField]
	private float 			interactionTime = 2f;
	
	public float 	InteractionTime
	{
		get { return interactionTime; }
	}

	public abstract void Action();

}
