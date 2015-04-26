using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour {

	public float VerticalSpeed = 0.0f;
	public float HorizontalSpeed = 2.0f;
	private Rigidbody2D rigidBody;
	private float WorldXLimit;
	private float WorldYLimit;

	// Use this for initialization
	void Start () {
		WorldXLimit = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0.0f, 0.0f)).x;
		WorldYLimit = Camera.main.ScreenToWorldPoint(new Vector3(0.0f, Screen.height, 0.0f)).y;
		rigidBody = GetComponent<Rigidbody2D>();
	}

	void Update(){

	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Vector2 Position = rigidBody.position;
//		Vector2 PosScreen = Camera.main.WorldToScreenPoint (Position);

		if (Position.x >= WorldXLimit) {
			HorizontalSpeed *= -1;
			rigidBody.position = new Vector2(WorldXLimit, Position.y); 
		} 
		else if (Position.x <= -WorldXLimit) {
			HorizontalSpeed *= -1;
			rigidBody.position = new Vector2(-WorldXLimit, Position.y); 
		}

		if (Position.y >= WorldYLimit) {
			VerticalSpeed *= -1;
			rigidBody.position = new Vector2(Position.x, WorldYLimit); 
		} 
		else if (Position.y <= -WorldYLimit) {
			VerticalSpeed *= -1;
			rigidBody.position = new Vector2(Position.x, -WorldYLimit); 
		}
		Position = rigidBody.position;

		Vector2 Movement = (Vector2.right * HorizontalSpeed + Vector2.up * VerticalSpeed) * Time.deltaTime ;
		rigidBody.MovePosition(Position + Movement);


	}
}
