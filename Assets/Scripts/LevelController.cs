using UnityEngine;
using System.Collections;

public class LevelController : MonoBehaviour {

	public int lastLevel = 0;

	void Awake() {
		if (GameObject.FindGameObjectsWithTag ("LevelControl").Length > 1) {
			Destroy (gameObject);
		}
		else DontDestroyOnLoad(transform.gameObject);
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
