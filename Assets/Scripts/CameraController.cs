using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

	public GameObject player;

	private Vector3 offset;

	// Use this for initialization
	void Start ()
	{
		offset = transform.position - player.transform.position;
	}
	
	// Runs every frame just like Update, however it only runs after all items have been processed on update.
	void LateUpdate ()
	{
		transform.position = player.transform.position + offset;
	}
}
