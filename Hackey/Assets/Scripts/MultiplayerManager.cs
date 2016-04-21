using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MultiplayerManager : MonoBehaviour {

	public Text gameTimeText;
	public float gameTime;
	public int maxTime = 3;



	public static int p1Score;
	public static int p2Score;
	public static int p3Score;
	public static int p4Score;


	public int[] scores;
	float t;

	// Use this for initialization
	void Start () {
		scores = new int[]{ p1Score, p2Score, p3Score, p4Score };
		t = (float)(maxTime * 60);
		gameTime = t;
	}
	
	// Update is called once per frame
	void Update () {
		gameTime -= Time.deltaTime;

		int i = (int)gameTime + 1;

		gameTimeText.text = "" + i;

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
