using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoving : MonoBehaviour
{
	public float	speed;
	public float	stoppingDistance;
	public float	retreatDistance;
	public float	seeingDistance;

	public 	Rigidbody 	thisRigid;
	
	private GameObject		player;
	private Vector3 		playerPos;

	public 	Weapon 	thisWeapon;

	public Death 	deathScript;
	
	// Update is called once per frame
	void Update()
	{
		// To keep track for player
		playerPos = Camera.main.transform.position;
		// To prevent jumping
		playerPos = new Vector3(playerPos.x, 0, playerPos.z);
		// Check if dead
		if (!deathScript.isDead)
			AI_Logic(Vector3.Distance(transform.position, playerPos));
	}
	
	
	private void AI_Logic(float dist)
	{
		if (dist > seeingDistance)
			return;

		transform.LookAt(playerPos);
		if (dist > stoppingDistance)
		{
			// Coming Closer
			transform.position = Vector3.MoveTowards(transform.position,
				playerPos, speed * Time.deltaTime);
		}
		else if (dist < stoppingDistance && dist > retreatDistance)
		{
			// Standing
			thisWeapon.Shoot();
		}
		else if (dist < retreatDistance)
		{
			// Retreat
			transform.position = Vector3.MoveTowards(transform.position,
				playerPos, -speed * Time.deltaTime);
		}

		DodgeLogic();
		thisRigid.velocity = new Vector3(0f,thisRigid.velocity.y,0f);
		thisRigid.angularVelocity = new Vector3(0f,0f,0f);
	}

	
	
	
	public float 	dodgeSpeed = 10;
	private float 	_dodgeTimer = 0;
	private bool 	_isDodging;
	public Transform _dodgePositionLeft;
	public Transform _dodgePositionRight;
	private Vector3 _dodgePosition;
	
	public void 	AI_SetDodge()
	{
		if (_isDodging)
			return;
		_isDodging = true;
		
		_dodgePosition = _dodgePositionLeft.position;
		if (Random.Range(0, 2) == 0)
			_dodgePosition = _dodgePositionRight.position;
	}
	
	private void 	DodgeLogic()
	{
		if (!_isDodging)
			return;
		
		transform.position = Vector3.MoveTowards(transform.position,
			_dodgePosition, dodgeSpeed * Time.deltaTime);

		_dodgeTimer += Time.deltaTime;
		if (_dodgeTimer >= 1f)
		{
			_isDodging = false;
			_dodgeTimer = 0;
		}
			
	}
}
