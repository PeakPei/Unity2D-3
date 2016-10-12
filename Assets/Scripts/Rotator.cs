using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{



	
	// Update is called once per frame; used in this case cause we are not using forces
	void Update ()
	{
		transform.Rotate (new Vector3 (0, 0, 45) * Time.deltaTime);
	}
}
