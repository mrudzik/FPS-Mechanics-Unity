using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeRay : MonoBehaviour
{
	public Camera		camera;
	public float 		dodgeRange;
	
	[Tooltip("Must be here Enemy Layer")]
	public LayerMask	layerMask;
	
	
	public void     Shoot()
	{
		Ray ray = camera.ViewportPointToRay(Vector3.one / 2f);

		Debug.DrawRay(ray.origin, ray.direction * dodgeRange, Color.green);

		RaycastHit hitInfo;
		// Will shoot a ray in a world and tries to find certain layer
		// in range of maxDistance
		if (Physics.Raycast(ray, out hitInfo, dodgeRange, layerMask))
		{	
			// Check to find a needed item
			var hitItem = hitInfo.collider.GetComponent<EnemyMoving>();
			if (hitItem != null)
			{
				hitItem.AI_SetDodge();
			}
		}
	}
}
