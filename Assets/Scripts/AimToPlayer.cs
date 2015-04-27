using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AimToPlayer : MonoBehaviour {

	public GameObject airPulse;
	GameObject player;
	public float cooldown = 3.0f;
	float cool = 0.0f;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 playerPosition = player.transform.position;
		Vector2 myPosition = transform.position;
		Vector2 direction = (myPosition-playerPosition).normalized;
		float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

		airPulse.transform.rotation = Quaternion.Lerp(airPulse.transform.rotation, Quaternion.AngleAxis(angle-90, Vector3.forward), 0.03f);
		if (cool <= 0) {
			PushThings pusher = airPulse.GetComponent<PushThings> ();
			List<GameObject> colliders = pusher.getColliders ();

			foreach (GameObject enemy in colliders) {
				if (enemy.CompareTag ("Player")) {
					pusher.PushPlayer ();
					cool = cooldown;
				}
			}
		} else
			cool -= Time.deltaTime;
	}
}
