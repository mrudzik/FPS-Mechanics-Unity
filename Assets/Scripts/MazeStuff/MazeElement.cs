using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class MazeElement : MonoBehaviour
{
	[HideInInspector] public string     state = "empty";
	[HideInInspector] public int 	posY = -1;
	[HideInInspector] public int 	posX = -1;
	
	[HideInInspector] public MazeElement []     links;
	
	
	public void 	RefreshCoordinates(ref int y, ref int x)
	{
		y = posY;
		x = posX;
	}
	
	
	
	
	
	public MazeElement 	RandForward()
	{
		// Left		0
		// Down		1
		// Right	2
		// Up		3 - forbidden

		// This will prevent redundant using of random
		// and infinite loops
		int [] 	randArr = {0, 0, 0, 1, 2, 2, 2};
		randArr = ShuffleArray(randArr);

		int i = 0;
		while (i < randArr.Length)
		{
			int num = randArr[i];
			if (links[num]?.state == "empty")
			{
				OpenSide(num,true);
				if (num > 1)
					links[num].OpenSide(num - 2, true);
				else
					links[num].OpenSide(num + 2, true);
				return links[num];
			}
			i++;
		}
		
		return null;
	}
	
	public MazeElement 	FinishForward()
	{
		if (links[2]?.state == "empty")
		{
			OpenSide(2,true);
			links[2].OpenSide(0, true);
			return links[2];
		}

		// This should notify the developer that something went wrong
		// immediately!
		throw new Exception("Found obstacle");
	}
	
	
	public MazeElement	RandFiller()
	{
		int [] 	randArr = {0, 1, 2, 3};
		randArr = ShuffleArray(randArr);
		
		int i = 0;
		while (i < 4)
		{
			int num = randArr[i];
			if (links[num]?.state == "empty")
			{
				OpenSide(num,true);
				if (num > 1)
					links[num].OpenSide(num - 2, true);
				else
					links[num].OpenSide(num + 2, true);
				return links[num];
			}
			// This will make connections with fillers and paths
			if (links[num]?.state == "path")
			{
				OpenSide(num,true);
				if (num > 1)
					links[num].OpenSide(num - 2, true);
				else
					links[num].OpenSide(num + 2, true);
				return null;
			}
			
			i++;
		}

		return null;
	}
	
	
	
	
	
	
	
	
	
	
	
	public static 	int[] 	ShuffleArray(int[] array)
	{
		// Knuth shuffle algorithm
		
		int i = 0;
		while (i < array.Length)
		{
			int tmp = array[i];
			int r = Random.Range(i, array.Length);

			array[i] = array[r];
			array[r] = tmp;
			i++;
		}

		return array;
	}



	
	














	public GameObject pathHold;
	public GameObject[] walls;
	

	public void ShowPath(bool pathState)
	{
		if (state == "path")
			pathHold.SetActive(pathState);
	}
	
	public void OpenSide(int side, bool wallState)
	{
		if (side < 0 || side > 3)
			return;
		
		walls[side].gameObject.SetActive(!wallState);
	}
}
