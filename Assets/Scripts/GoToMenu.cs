using UnityEngine;
using System.Collections;

public class GoToMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void LoadLastLevel()
	{
		Application.LoadLevel(GameObject.Find("LevelControl").GetComponent<LevelController>().lastLevel);
	}

	public void LoadNextLevel()
	{
		Application.LoadLevel(GameObject.Find("LevelControl").GetComponent<LevelController>().lastLevel+1);
	}
	
	public void LoadMenuScene()
	{
		Application.LoadLevel ("Main Menu");
	}
}
