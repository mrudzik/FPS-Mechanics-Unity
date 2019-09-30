using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
	public Camera     		menuCamera;
	public RectTransform 	mainMenu;

	//     I see the opportunity that we can add
	// an array of levels here but then i'll need
	// to code another solution
	
	[Tooltip("Insert junior level here")]
	public GameObject levelJunior;
	[Tooltip("Insert maze level here")]
	public GameObject levelMiddle;
	
	private GameObject currentLevel;
	
	[Tooltip("This is to keep objects organized")]
	public Transform levelHolder;
	
	public Button 	continueButton;
	public MusicController	musicScript;

	
	
	// Only Load button
	public void 	LoadJuniorLevel()
	{
		if (currentLevel != null)
			Destroy(currentLevel);
		
		TurnMainMenu(false);
		currentLevel = Instantiate(levelJunior, levelHolder);
		// Set menu reference in map
		ScenarioJunior currScenario = currentLevel.GetComponent<ScenarioJunior>();
		currScenario.ourMenu = this;

	}

	
	
	
	
	
	
	
	
	
	
	private int 	_mazeSize = 10;
	public Text 	mazeText;
	
	public void 	RefreshMazeText()
	{
		mazeText.text = "Maze Size is: " + _mazeSize;
	}
	
	public void 	IncreaseMazeSize()
	{
		if (_mazeSize >= 100)
			return;
		_mazeSize++;
		RefreshMazeText();
	}
	public void 	DecreaseMazeSize()
	{
		if (_mazeSize <= 5)
			return;
		_mazeSize--;
		RefreshMazeText();
	}
	
	public void 	LoadMiddleLevel()
	{
		if (currentLevel != null)
			Destroy(currentLevel);
		
		TurnMainMenu(false);
		currentLevel = Instantiate(levelMiddle, levelHolder);
		ScenarioMiddle currScenario = currentLevel.GetComponent<ScenarioMiddle>();
		currScenario.ourMenu = this;
		currScenario.GenerateMaze(_mazeSize);
	}

	
	
	
	
	
	
	
	
	
	
	
	// If pressed Esc
	// alternative if pressed Continue UI Button
	public void  	OpenMenu(bool state)
	{
		if (currentLevel != null)
			currentLevel.gameObject.SetActive(!state);
		TurnMainMenu(state);
		if (state)
		{
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		}
		else
			Cursor.lockState = CursorLockMode.Locked;
		continueButton.interactable = currentLevel != null;
	}
	
	// Turns on\off UI elements
	private void 	TurnMainMenu(bool state)
	{
		musicScript.PlayMenu(state);
		menuCamera.gameObject.SetActive(state);
		mainMenu.gameObject.SetActive(state);
	}
	public void 	KillLevel()
	{
		Destroy(currentLevel);
		TurnMainMenu(true);
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
		continueButton.interactable = false;
	}
	
	public void 	Exit()
	{
		Application.Quit();
	}
	
	private void Update()
	{
		if (Input.GetKey(KeyCode.Escape))
		{
			OpenMenu(true);
		}
	}
	
	private void Start()
	{
		OpenMenu(true);
		RefreshMazeText();
	}
}
