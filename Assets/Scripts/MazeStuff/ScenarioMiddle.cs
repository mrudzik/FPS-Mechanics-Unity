using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenarioMiddle : MonoBehaviour
{
	[HideInInspector]
	public MenuController	ourMenu;
	public DeathPlayer 		ourPlayer;
	public DmgCtrlPlayer	ourPlayerDamage;
	public MazeGenerator	mazeScript;
	

	public void GenerateMaze(int mazeSize)
	{
		mazeScript.GenerateNewMaze(mazeSize);
	}
	
	

	private bool 	isWin;
	private float 	winTimer;
	public void 	WinScenario()
	{
		if (isWin)
			return;
		isWin = true;
		winTimer = 0f;
		
		ourPlayerDamage.WinSound();
		ourPlayer.winUI.gameObject.SetActive(true);
		
		
	}

	private void Update()
	{
		if (isWin)
		{
			if (winTimer >= 5f)
				ourMenu.KillLevel();
			winTimer += Time.deltaTime;
		}
	}
}
