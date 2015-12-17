using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreSystem : MonoBehaviour {

	public static int playerScore;
	private Text scoreText;

	public static int gpCounter;
	public Text gpText;

	// Use this for initialization
	void Start () {
		scoreText = GetComponent<Text> ();
		playerScore = 0;
		gpCounter = 0;
	}
	
	// Update is called once per frame
	void Update () {
		scoreText.text = "Score: " + playerScore;
		gpText.text = "x " + gpCounter;
	}
}
