using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameOverMenu : MonoBehaviour {

	public Text scoreText;

	// Use this for initialization
	void Start () {
		scoreText.text = "Your score: " + ScoreSystem.playerScore;
	}
	
	public void BackToTitle() {
		Application.LoadLevel ("Title-Menu");
	}
}
