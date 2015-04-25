using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PushThings : MonoBehaviour {

	List<GameObject> colliders;

	public float pushForce = 10.0f;

	// Use this for initialization
	void Start () {
		colliders = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnTriggerEnter2D (Collider2D other){
		colliders.Add (other.gameObject);
	}

	void OnTriggerExit2D (Collider2D other){
		colliders.Remove (other.gameObject);
	}

	public void PushEnemies(){
		foreach (GameObject enemy in colliders) {
			if(enemy.CompareTag("Enemy")){

				Vector2 distance = enemy.transform.position-transform.position;
				Vector2 direction = distance.normalized;
				float speed = pushForce/distance.magnitude;

				enemy.GetComponent<Rigidbody2D>().AddForce(direction*speed, ForceMode2D.Impulse);
				enemy.GetComponent<EnemyController>().SetVulnerable(1.0f);
			}
		}
	}

	public void PushPlayer(){
		
	}
}
