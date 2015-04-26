using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour {

	public float RotationSpeed = 100.0f;
	private Rigidbody2D rigidBody2D;

	// Use this for initialization
	void Start () {
		rigidBody2D = GetComponent<Rigidbody2D> ();
		rigidBody2D.centerOfMass = new Vector2 (100.0f, 100.0f);/*transform.position;*/
	}

	void Update(){
	}
	
	// Update is called once per frame
	void FixedUpdate () {
//		rigidBody2D.rotation = rigidBody2D.rotation + RotationSpeed * Time.deltaTime;
		rigidBody2D.MoveRotation(rigidBody2D.rotation + RotationSpeed * Time.deltaTime);
	}
}
