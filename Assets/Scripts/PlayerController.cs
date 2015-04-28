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

	public Texture2D healthBarEmpty;
	public Texture2D healthBarFull;

	public GameObject balloon1;
	public GameObject balloon2;
	public GameObject balloon3;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

		switch (health) {
		case 0:
			balloon1.SetActive(false);
			balloon2.SetActive(false);
			balloon3.SetActive(false);
			break;
		case 1:
			balloon1.SetActive(true);
			balloon2.SetActive(false);
			balloon3.SetActive(false);
			break;
		case 2:
			balloon1.SetActive(true);
			balloon2.SetActive(true);
			balloon3.SetActive(false);
			break;
		case 3:
			balloon1.SetActive(true);
			balloon2.SetActive(true);
			balloon3.SetActive(true);
			break;
		}

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

		if (Input.GetMouseButton (0) && health > 0) {
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
		//float offset = 40;
		float offset = 0;//GetComponent<SpriteRenderer> ().bounds.size.y;
		//Debug.Log (offset);
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
		else if (playerPosScreen.y < -60){
			GameOver();
			//playerBody.velocity = new Vector2(playerBody.velocity.x, Mathf.Abs(playerBody.velocity.y));
		}

		Flicker ();
		playerBody.gravityScale = 1 - health * 0.3f;
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
		//Debug.Log("Has perdido");
		GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<GameFlow> ().LoseGame ();
	}

	void OnGUI(){
		float progress = air / airMax;
		Debug.Log (progress);
		Vector2 pos = new Vector2 (10, 10);
		int width = Screen.width*healthBarEmpty.width/1500;
		int height = Screen.width*healthBarEmpty.height/1500;

		GUI.DrawTexture(new Rect(pos.x, pos.y, width, height), healthBarEmpty);
		GUI.DrawTexture(new Rect(pos.x, pos.y, (width * Mathf.Clamp01(progress)), height), healthBarFull);
	}
}
