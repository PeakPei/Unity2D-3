using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Player : MovingObject
{

	public int wallDamage = 1;
	public int pointsPerFood = 10;
	public int pointsPerSoda = 20;
	public float restartLevelDelay = 1f;

	private Animator animator;
	private int food;

	// Use this for initialization
	protected override void Start ()
	{
		animator = GetComponent<Animator> ();
		food = GameManager.instance.PlayerFoodPoints;
		base.Start ();
	}

	private void onDisable ()
	{
		GameManager.instance.PlayerFoodPoints = food;
	}

	// Update is called once per frame
	void Update ()
	{
		if (!GameManager.instance.playersTurn) {
			GameManager.instance.doUpdate ();
			return;
		}

		int horizontal = 0;
		int vertical = 0;

		horizontal = (int)Input.GetAxisRaw ("Horizontal");
		vertical = (int)Input.GetAxisRaw ("Vertical");

		//prevent player from moving diagonally
		if (horizontal != 0)
			vertical = 0;

		if (horizontal != 0 || vertical != 0) {
			AttemptMove<Wall> (horizontal, vertical);
		}

	}

	protected override void AttemptMove<T> (int xDir, int yDir)
	{
		food--;
		base.AttemptMove<T> (xDir, yDir);
		RaycastHit2D hit;
		checkIfGameOver ();
		GameManager.instance.playersTurn = false;
	}

	private void onTriggerEnter2D (Collider2D other)
	{
		if (other.tag == "exit") {
			Invoke ("restart", restartLevelDelay);
			enabled = false;
		} else if (other.tag == "Food") {
			food += pointsPerFood;
			other.gameObject.SetActive (false);
		} else if (other.tag == "Soda") {
			food += pointsPerSoda;
			other.gameObject.SetActive (false);
		}
	}

	protected override void onCantMove<T> (T component)
	{
		Wall hitWall = component as Wall;
		hitWall.damageWall (wallDamage);
		animator.SetTrigger ("playerChop");
	}

	private void restart ()
	{
		SceneManager.LoadScene (0);
	}

	public void looseFood (int loss)
	{
		animator.SetTrigger ("playerHit");
		food -= loss;
		checkIfGameOver ();
	}

	private void checkIfGameOver ()
	{
		if (food <= 0) {
			GameManager.instance.GameOver ();
		}
	}
}

// (felipemarino : (TODO: change damage scheme. Apply charcacter evolution by monsters killed and then, on level up, increase wallDamage by 1)

