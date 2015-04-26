using UnityEngine;
using System.Collections;

public class GoToMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void LoadMenuScene()
	{
		Application.LoadLevel ("Main Menu");
	}
}
