using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

	GameObject player;

	public float moveSpeed = 2.0f;
	public int health = 1;
	float vulnerableTime = 0.0f;

	Rigidbody2D playerBody;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		playerBody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		if (health > 0) {
			if (vulnerableTime <= 0) {
				MoveTowardsPlayer ();
			} else
				vulnerableTime -= Time.deltaTime;
		}
		else playerBody.gravityScale = 1;
		Vector2 playerPosition = transform.position;

		float screenX = Screen.width;
		float screenY = Screen.height;
		float offset = 40;
		float offRight = screenX - offset;
		float offLeft = offset;
		float offTop = screenY - offset;
		float offBottom = offset;
		Vector2 playerPosScreen = Camera.main.WorldToScreenPoint(playerPosition);
		
		if(playerPosScreen.x > offRight ){
			playerBody.velocity = new Vector2(-Mathf.Abs(playerBody.velocity.x), playerBody.velocity.y);
		}
		else if (playerPosScreen.x < offLeft){
			playerBody.velocity = new Vector2(Mathf.Abs(playerBody.velocity.x), playerBody.velocity.y);
		}
		
		if(playerPosScreen.y> offTop ){
			playerBody.velocity = new Vector2(playerBody.velocity.x, -Mathf.Abs(playerBody.velocity.y));
		}
		else if (playerPosScreen.y < -offBottom){
			if(health <= 0 || vulnerableTime >0){
				Die();
			}
			//playerBody.velocity = new Vector2(playerBody.velocity.x, Mathf.Abs(playerBody.velocity.y));
		}
	}

	void MoveTowardsPlayer(){
		Vector2 position = transform.position;
		Vector2 distance = player.transform.position - transform.position;
		Vector2 direction = distance.normalized;
		Vector2 auxDirection;

		Debug.DrawRay (transform.position, distance);
		RaycastHit2D obstacle = Physics2D.Raycast(position, direction, distance.magnitude);

		if (obstacle.collider!=null) {
			float width = obstacle.collider.bounds.size.x;
			Vector2 colliderPosition = obstacle.collider.transform.position;

			auxDirection = ((obstacle.point+(new Vector2(direction.y, -direction.x)*(width/2.0f))) - position).normalized;

			RaycastHit2D right = Physics2D.Raycast(position, auxDirection, distance.magnitude);

			Debug.DrawRay (transform.position, auxDirection);

			auxDirection = ((obstacle.point-(new Vector2(direction.y, -direction.x)*(width/2.0f))) - position).normalized;
			
			//RaycastHit2D left = Physics2D.Raycast(position, auxDirection, distance.magnitude);

			Debug.DrawRay (transform.position, auxDirection);

			if (right.collider==null){
				distance = (colliderPosition+(new Vector2(direction.y, -direction.x)*width*2.0f*distance.magnitude)) - position;
				direction = distance.normalized;
				//Debug.Log("Right");
			}
			else{
				distance = (colliderPosition-(new Vector2(direction.y, -direction.x)*width*2.0f*distance.magnitude)) - position;
				direction = distance.normalized;
				//Debug.Log("Left");
			}
		}


		Debug.DrawRay (transform.position, direction);
		//Debug.DrawRay (transform.position, direction);

		playerBody.AddForce(direction*moveSpeed);

		GameObject[] obstaclesArray = GameObject.FindGameObjectsWithTag ("Obstacle");

		foreach (GameObject obstacleI in obstaclesArray) {
			Vector2 obsPos = obstacleI.transform.position;
			float dist = Mathf.Abs((obsPos - position).magnitude);
			if (dist<1.5f){
				Vector2 dir = -(obsPos - position).normalized;
				playerBody.AddForce(dir*3*Mathf.Pow((1.8f-dist), 3.0f));
			}
		}

		obstaclesArray = GameObject.FindGameObjectsWithTag ("Enemy");
		
		foreach (GameObject obstacleI in obstaclesArray) {
			Vector2 obsPos = obstacleI.transform.position;
			float dist = Mathf.Abs((obsPos - position).magnitude);
			if (dist<1.5f){
				Vector2 dir = -(obsPos - position).normalized;
				playerBody.AddForce(dir*3*Mathf.Pow((1.8f-dist), 3.0f));
			}
		}

	}

	public void Damage(){
		if (health > 0) {
			health -= 1;
		}
	}

	public void SetVulnerable(float time){
		vulnerableTime = time;
	}

	public float GetVulnerableTime(){
		return vulnerableTime;
	}

	public void Die(){
		Destroy (gameObject);
	}
	
}
