using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameFlow : MonoBehaviour {


	// Use this for initialization
	void Start () {
		GameObject.Find("LevelControl").GetComponent<LevelController>().lastLevel = Application.loadedLevel;
	}
	
	// Update is called once per frame
	void Update () {

		if (GameObject.FindGameObjectsWithTag ("Enemy").Length == 0) {
			NextLevel();
		}
	}

	public void LoseGame(){
		Application.LoadLevel ("GameOver");
	}

	public void NextLevel(){
		if (Application.loadedLevel == 5) {
			Application.LoadLevel("GameComplete");
		}
		else Application.LoadLevel ("LevelClear");
	}
}
