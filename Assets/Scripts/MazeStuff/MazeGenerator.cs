using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
	public GameObject	prefabCell;

	public float	cellOffset;
	private int		mazeSize;

	public Transform 	mazeHolder;
	private GameObject[][] 	_mazeArray;

	public GameObject 	final;

//	private void Start()
//	{
//		if (mazeSize < 5)
//			mazeSize = 5;
//		else if (mazeSize > 100)
//			mazeSize = 100;
//	}

	public void     GenerateNewMaze(int newMazeSize)
	{
		mazeSize = newMazeSize;
		if (mazeSize < 5)
			mazeSize = 5;
		else if (mazeSize > 100)
			mazeSize = 100;
		
		GenerateMazeSlots();
		SetReferences();
		GeneratePath();
		GenerateFillers();
		
		
		
		foreach (GameObject[] yLine in _mazeArray)
		{
			foreach (GameObject objElem in yLine)
			{
				MazeElement mazeElem = objElem.GetComponent<MazeElement>();
			
				mazeElem.ShowPath(true);
			}
		}
		// Set Final cell area
		final.transform.position = _mazeArray[mazeSize-1][mazeSize-1].transform.position;
	}
	
	
	private void 	GenerateMazeSlots()
	{
//		if (_mazeArray != null)
//		{
//			// Garbage collector should handle this
//		}
		_mazeArray = new GameObject[mazeSize][];
		
		int y = 0;
		while (y < _mazeArray.Length)
		{
			_mazeArray[y] = new GameObject[mazeSize];
			
			int x = 0;
			while (x < _mazeArray[y].Length)
			{
				Vector3 newPos = new Vector3(mazeHolder.position.x - cellOffset * x,
					mazeHolder.position.y, mazeHolder.position.z + cellOffset * y);
				_mazeArray[y][x] = Instantiate(prefabCell, newPos, new Quaternion(), mazeHolder);
				x++;
			}
			y++;
		}
		// Maze should look like this
		
		// y0: x1, x2 ... xN
		// y1: x1, x2 ... xN
		// ...
		// yN: x1, x2 ... xN
	}
	
	
	private void 	SetReferences()
	{	
		int y = 0;
		while (y < _mazeArray.Length)
		{
			int x = 0;
			while (x < _mazeArray[y].Length)
			{
				MazeElement current = _mazeArray[y][x].GetComponent<MazeElement>();
				current.links = new MazeElement[4];
				// Left		0
				// Down		1
				// Right	2
				// Up		3
				
				// Set Left Link
				if (x > 0)
					current.links[0] = _mazeArray[y][x - 1].GetComponent<MazeElement>();
				// Set Down Link
				if (y < mazeSize - 1)
					current.links[1] = _mazeArray[y + 1][x].GetComponent<MazeElement>();
				// Set Right Link
				if (x < mazeSize - 1)
					current.links[2] = _mazeArray[y][x + 1].GetComponent<MazeElement>();
				// Set Up Link
				if (y > 0)
					current.links[3] = _mazeArray[y - 1][x].GetComponent<MazeElement>();

				current.posY = y;
				current.posX = x;
				x++;
			}
			y++;
		}
	}
	
	
	
	private void 	GeneratePath()
	{
		// Initialize at start
		MazeElement curElem = _mazeArray[0][0].GetComponent<MazeElement>();
		
		// Start coordinates of maze
		int y = 0;
		int x = 0;
		curElem.OpenSide(3, true);
		curElem.OpenSide(0, true);
		
		// Random Walk till the last line;
		while (y < mazeSize - 1)
		{
//			Debug.Log("y - " + y + " x - " + x);
			curElem = curElem.RandForward();
			curElem.RefreshCoordinates(ref y, ref x);
			curElem.state = "path";
		}
		
		// When on last line, go to final cell
		while (x < mazeSize - 1)
		{
//			Debug.Log("y - " + y + " x - " + x);
			curElem = curElem.FinishForward();
			curElem.RefreshCoordinates(ref y, ref x);
			curElem.state = "path";
		}
		
		if (x == mazeSize - 1 && y == mazeSize - 1)
		{
			curElem.state = "finish";
			curElem.OpenSide(1, true);
			curElem.OpenSide(2, true);
			return;
		}
		
		throw new Exception("Generator didn't make it to Finish");
	}
	
	
	
	
	
	
	
	
	
	
	
	
	private void	GenerateFillers()
	{
		foreach (GameObject[] yLine in _mazeArray)
		{
			foreach (GameObject objElem in yLine)
			{
				MazeElement mazeElem = objElem.GetComponent<MazeElement>();
			
				if (mazeElem.state == "empty")
				{
					GenFiller(mazeElem);
				}
			}
		}
	}
	private void 	GenFiller(MazeElement current)
	{
		
		while (current != null)
		{
			current.state = "filler";
			current = current.RandFiller();
		}
	}
	
	
	
	
	

	
	
	
	
	
	
	
	
}
