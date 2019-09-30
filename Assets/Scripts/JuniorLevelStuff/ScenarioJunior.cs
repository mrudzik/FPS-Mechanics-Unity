using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenarioJunior : MonoBehaviour
{
	[HideInInspector]
	public MenuController	ourMenu;
	public DeathPlayer 		ourPlayer;
	public DmgCtrlPlayer	ourPlayerDamage;

	
	private int 	scenarioStage = 0;

	public Transform 		enemyHolder;
	public GameObject[] 	dummies;
	public GameObject 		aiPrefab;

	public Transform[] 		spawnPositions;
	
	private List<GameObject> aiEnemies;


	private void Start()
	{
		aiEnemies = new List<GameObject>();
	}

	private void Update()
	{
		if (ourPlayer.scenarioDeath)
		{
			ourMenu.KillLevel();
			// Little spageti code for cool effect
			ourMenu.musicScript.PlayDeath();
		}
		ScenarioChecks();
	}
	
	
	
	
	
	
	
	
	
	private bool 	IsBotsAlive(GameObject[] bots)
	{		
		foreach (GameObject dummy in bots)
		{
			if (dummy != null)
				return true;
		}
		return false;
	}
	
	private bool 	IsBotsAlive(List<GameObject> bots)
	{
		foreach (GameObject dummy in bots)
		{
			if (dummy != null)
				return true;
		}
		return false;
	}


	private float winTimer = 0f;
	
	private void ScenarioChecks()
	{	
		switch (scenarioStage)
		{
			case 0: // When dummies alive
				if (!IsBotsAlive(dummies))
				{
					scenarioStage++;
					SpawnAdvancedEnemies(1);
				}
				break;
			case 1: // When spawned 1 moving enemy
				if (!IsBotsAlive(aiEnemies))
				{
					scenarioStage++;
					SpawnAdvancedEnemies(2);
				}
				break;
			case 2:
				if (!IsBotsAlive(aiEnemies))
				{
					scenarioStage++;
					SpawnAdvancedEnemies(3);
				}
				break;
			case 3: // When killed all enemies
				if (!IsBotsAlive(aiEnemies))
				{
					Debug.Log("You Win!");
					ourPlayerDamage.WinSound();
					ourPlayer.winUI.gameObject.SetActive(true);
					scenarioStage++;
				}
				break;
			case 4:
				if (winTimer >= 5f)
					ourMenu.KillLevel();
				winTimer += Time.deltaTime;
				break;
			default:
				Debug.Log("Scenario stage DEBUG");
				break;
		}
	}
	
	private void 	SpawnAdvancedEnemies(int count)
	{
		Debug.Log("Must Spawn Wave of " + count);
		int i = 0;
		while (i < count)
		{
			Transform point = spawnPositions[Random.Range(0, spawnPositions.Length)];
			GameObject tempBot = Instantiate(aiPrefab, point.position, new Quaternion(), enemyHolder);
			
			aiEnemies.Add(tempBot);
			
			i++;
		}
	}
}
