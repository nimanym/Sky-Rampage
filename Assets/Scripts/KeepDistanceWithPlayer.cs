using UnityEngine;
using System.Collections;

public class KeepDistanceWithPlayer : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		GameObject player = GameObject.FindGameObjectWithTag ("Player");
		Vector2 position = transform.position;
		Vector2 playerPos = player.transform.position;
		float dist = Mathf.Abs((playerPos - position).magnitude);

		if (dist<4.0f){
			Vector2 dir = -(playerPos - position).normalized;
			GetComponent<Rigidbody2D>().AddForce(dir*2*(4.0f-dist));
		}

	}
}
