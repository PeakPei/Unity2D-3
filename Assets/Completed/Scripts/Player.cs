using UnityEngine;
using System.Collections;

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
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}
