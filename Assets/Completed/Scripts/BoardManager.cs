using UnityEngine;
using System;
using System.Collections.Generic;

//Allows us to use Lists.
using Random = UnityEngine.Random;

//Tells Random to use the Unity Engine random number generator.



public class BoardManager : MonoBehaviour
{
	// Using Serializable allows us to embed a class with sub properties in the inspector.
	[Serializable]
	public class Count
	{
		public int minimum;
		public int maximum;

		//Assignment constructor.
		public Count (int min, int max)
		{
			minimum = min;
			maximum = max;
		}
	}


	public int columns = 8;
	public int rows = 8;
	public Count wallCount = new Count (5, 9);
	//Lower and upper limit for our random number of walls per level.
	public Count foodCount = new Count (1, 5);
	//Lower and upper limit for our random number of food items per level.
	public GameObject exit;
	public GameObject[] floorTiles;
	public GameObject[] wallTiles;
	public GameObject[] foodTiles;
	public GameObject[] enemyTiles;
	public GameObject[] outerWallTiles;

	private Transform boardHolder;
	//A variable to store a reference to the transform of our Board object.
	private List <Vector3> gridPositions = new List <Vector3> ();
	//A list of possible locations to place tiles.


	//Clears our list gridPositions and prepares it to generate a new board.
	void initialiseList ()
	{
		//Clear our list gridPositions.
		gridPositions.Clear ();

		//Loop through x axis (columns).
		for (int x = 1; x < columns - 1; x++) {
			//Within each column, loop through y axis (rows).
			for (int y = 1; y < rows - 1; y++) {
				//At each index add a new Vector3 to our list with the x and y coordinates of that position.
				gridPositions.Add (new Vector3 (x, y, 0f));
			}
		}
	}


	//Sets up the outer walls and floor (background) of the game board.
	void boardSetup ()
	{
		//Instantiate Board and set boardHolder to its transform.
		boardHolder = new GameObject ("Board").transform;

		for (int x = -1; x < columns + 1; x++) {
			for (int y = -1; y < rows + 1; y++) {
				GameObject toInstantiate = floorTiles [Random.Range (0, floorTiles.Length)];

				if (x == -1 || x == columns || y == -1 || y == rows)
					toInstantiate = outerWallTiles [Random.Range (0, outerWallTiles.Length)];

				GameObject instance =
					Instantiate (toInstantiate, new Vector3 (x, y, 0f), Quaternion.identity) as GameObject;

				instance.transform.SetParent (boardHolder);
			}
		}
	}


	//RandomPosition returns a random position from our list gridPositions.
	Vector3 randomPosition ()
	{
		int randomIndex = Random.Range (0, gridPositions.Count);
		Vector3 randomPosition = gridPositions [randomIndex];
		gridPositions.RemoveAt (randomIndex);

		return randomPosition;
	}


	//LayoutObjectAtRandom accepts an array of game objects to choose from along with a minimum and maximum range for the number of objects to create.
	void layoutObjectAtRandom (GameObject[] tileArray, int minimum, int maximum)
	{
		int objectCount = Random.Range (minimum, maximum + 1);

		//Instantiate objects until the randomly chosen limit objectCount is reached
		for (int i = 0; i < objectCount; i++) {
			Vector3 randomPositionVector3 = randomPosition ();

			GameObject tileChoice = tileArray [Random.Range (0, tileArray.Length)];

			Instantiate (tileChoice, randomPositionVector3, Quaternion.identity);
		}
	}


	//SetupScene initializes our level and calls the previous functions to lay out the game board
	public void setupScene (int level)
	{
		boardSetup ();
		initialiseList ();

		layoutObjectAtRandom (wallTiles, wallCount.minimum, wallCount.maximum);

		layoutObjectAtRandom (foodTiles, foodCount.minimum, foodCount.maximum);

		int enemyCount = (int)Mathf.Log (level, 2f);

		layoutObjectAtRandom (enemyTiles, enemyCount, enemyCount);

		Instantiate (exit, new Vector3 (columns - 1, rows - 1, 0f), Quaternion.identity);
	}
}