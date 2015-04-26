using UnityEngine;
using System.Collections;

public class LevelController : MonoBehaviour {

	public int lastLevel = 0;

	void Awake() {
		DontDestroyOnLoad(transform.gameObject);
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
