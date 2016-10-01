using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
	public float turnDelay = .1f;
	public static GameManager instance = null;
	public BoardManager boardScript;
	public int PlayerFoodPoints;
	[HideInInspector] public bool playersTurn = true;

	private int level = 3;
	private List<Enemy> enemies;
	private bool enemiesMoving;

	// Use this for initialization
	void Awake ()
	{
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy (gameObject);
		}

		DontDestroyOnLoad (gameObject);
		enemies = new List<Enemy> ();
		boardScript = GetComponent<BoardManager> ();
		initGame ();
	}

	void initGame ()
	{
		enemies.Clear ();
		boardScript.setupScene (level);
	}

	public void GameOver ()
	{
		enabled = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (playersTurn || enemiesMoving) {
			return;
		}

		StartCoroutine (MoveEnemies ());
	}

	public void doUpdate ()
	{
		if (playersTurn || enemiesMoving) {
			return;
		}

		StartCoroutine (MoveEnemies ());
	}

	public void addEnemyToList (Enemy script)
	{
		enemies.Add (script);
	}

	IEnumerator MoveEnemies ()
	{
		enemiesMoving = true;
		yield return new WaitForSeconds (turnDelay);
		if (enemies.Count == 0) {
			yield return new WaitForSeconds (turnDelay);
		}

		for (int i = 0; i < enemies.Count; i++) {
			enemies [i].moveEnemy ();
			yield return new WaitForSeconds (enemies [i].moveTime);
		}

		playersTurn = true;
		enemiesMoving = false;
	}
}
