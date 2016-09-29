using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
	public static GameManager instance = null;
	public BoardManager boardScript;
	public int PlayerFoodPoints;
	[HideInInspector] public bool playersTurn = true;

	private int level = 3;

	// Use this for initialization
	void Awake ()
	{
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy (gameObject);
		}

		DontDestroyOnLoad (gameObject);
		boardScript = GetComponent<BoardManager> ();
		initGame ();
	}

	void initGame ()
	{
		boardScript.setupScene (level);
	}

	public void GameOver ()
	{
		enabled = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
}
