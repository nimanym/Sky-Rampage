using UnityEngine;
using System.Collections;

public class GameFlow : MonoBehaviour {


	// Use this for initialization
	void Start () {
		GameObject.Find("LevelControl").GetComponent<LevelController>().lastLevel = Application.loadedLevel;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void LoseGame(){
		Application.LoadLevel ("GameOver");
	}
}
