using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MultiplayerManager : MonoBehaviour {

	public Text gameTimeText;
	public float gameTime;

	public float gameOverTimeLimit = 181.0f;

	public GameObject[] players;

	public static int p1Score;
	public static int p2Score;
	public static int p3Score;
	public static int p4Score;

	public int[] scores;



	// Use this for initialization
	void Start () {
        Time.timeScale = 1.0f;
		scores = new int[]{ p1Score, p2Score, p3Score, p4Score };
		gameTime = 0;
	}
	
	// Update is called once per frame
	void Update () {
		gameTime += Time.deltaTime;

		int i = (int)gameTime + 1;

		gameTimeText.text = "" + i;

		float timeLeft = gameOverTimeLimit - gameTime;
		int minutes = Mathf.FloorToInt (timeLeft / 61.0f);
		int seconds = Mathf.RoundToInt (timeLeft - minutes * 60.0f);

		if (seconds < 10) {
			gameTimeText.text = minutes + ":0" + seconds;
		} else {
			gameTimeText.text = minutes + ":" + seconds;
		}

		if (gameTime <= 0.0f) {
			GameOver ();
		}
	}

	void GameOver() {
		MaxValue (scores);
	}

	int MaxValue(int[] i) {
		int max = scores [0];
		for (int j = 0; j < scores.Length; j++) {
			if (scores [j] > max) {
				max = scores [j];
			}
		}
		return max;
	}

	void WinnerCount() {
		int zeroCount = 0;
		for (int j = 0; j < scores.Length; j++) {
			if (scores [j] == 0) {
				zeroCount++;
			}
			if (zeroCount == 3) {
				return;
			}
		}
	} // end WinnerCount()
}
