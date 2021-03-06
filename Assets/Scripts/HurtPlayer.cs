using UnityEngine;
using System.Collections;

public class HurtPlayer : MonoBehaviour {

	public float knockback = 3.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnCollisionEnter2D(Collision2D collision){
		if (collision.gameObject.tag == "Player") {
			GameObject player = collision.gameObject;
			player.GetComponent<PlayerController>().Damage();

			Vector2 direction = (player.transform.position-transform.position).normalized;
			Rigidbody2D playerBody = player.GetComponent<Rigidbody2D>();
			playerBody.velocity = new Vector2(0,0);
			playerBody.AddForce(direction*knockback, ForceMode2D.Impulse);
		}

	}
}
