using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

	public float speed;
	public Text countText;
	public Text winText;

	private Rigidbody2D rb2d;
	private int collectedCount;

	void Start ()
	{
		rb2d = GetComponent<Rigidbody2D> ();
		collectedCount = 0;
		winText.text = "";
		setCountText ();
	}

	//update (called before rendering a frame) or fixedUpdate (before performing any physics calculation)
	void FixedUpdate ()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
		rb2d.AddForce (new Vector2 (moveHorizontal, moveVertical) * speed);
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.gameObject.CompareTag ("PickUp"))
			other.gameObject.SetActive (false);

		collectedCount += 1;
		setCountText ();
	}

	private  void setCountText ()
	{
		countText.text = "Count: " + collectedCount.ToString ();
		if (collectedCount >= 12)
			winText.text = "You win";
	}
}
