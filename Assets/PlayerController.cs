using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float speed = 4.0f;
	public float air = 5.0f;
	public float airMax = 5.0f;
	public float airMultiplier = 2.0f;
	float counter = 0.0f;
	public float cooldown = 0.3f;
	public int health = 3;

	float flickerTime = 0.0f;
	int frames = 5;
	bool clicked = false;

	public GameObject airPulse;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if(clicked){
			airPulse.GetComponent<PushThings> ().PushEnemies();
			clicked=false;
		}


		air += (airMultiplier * Time.deltaTime);
		if (air > airMax) {
			air = airMax;
		}
		counter -= Time.deltaTime;

		Rigidbody2D playerBody = GetComponent<Rigidbody2D>();
		Vector2 playerPosition = transform.position;

		if (Input.GetMouseButton (0)) {
			Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Vector2 direction = (playerPosition-mousePosition).normalized;
			float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

			airPulse.transform.rotation = Quaternion.AngleAxis(angle-90, Vector3.forward);

			if(air >= 1.0f && counter <= 0.0f){
				playerBody.AddForce(direction*speed, ForceMode2D.Impulse);
				air -= 1.0f;
				counter = cooldown;
				clicked=true;
			}

		}

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
		else if (playerPosScreen.y < offBottom){
			GameOver();
			//playerBody.velocity = new Vector2(playerBody.velocity.x, Mathf.Abs(playerBody.velocity.y));
		}

		Flicker ();
		playerBody.gravityScale = 1 - health * 0.2f;
	}

	public void Damage(){
		if (health > 0 && flickerTime <= 0) {
			health -= 1;
		}
		flickerTime = 1.0f;
	}

	void Flicker(){
		if (flickerTime > 0) {
			frames--;
			flickerTime -= Time.deltaTime;

			if (frames <= 0) {
				GetComponent<SpriteRenderer> ().enabled = !GetComponent<SpriteRenderer> ().enabled;
				airPulse.GetComponent<SpriteRenderer> ().enabled = GetComponent<SpriteRenderer> ().enabled;
				frames = 5;
			}
		} else {
			GetComponent<SpriteRenderer> ().enabled = true;
			airPulse.GetComponent<SpriteRenderer> ().enabled = true;
		}
	}

	void GameOver(){
		Debug.Log("Has perdido");
	}
}
