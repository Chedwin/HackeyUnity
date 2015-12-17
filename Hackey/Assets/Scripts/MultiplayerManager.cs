using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MultiplayerManager : MonoBehaviour {

	public Text p1Timer;
	public Text p2Timer;

	public float mpTimeLimit = 300.0f;
	public float mpGameTimer;

	// Use this for initialization
	void Start () {
		mpGameTimer = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
		mpGameTimer += Time.deltaTime;
		float timeLeft = mpTimeLimit - mpGameTimer;
		
		int minutes = Mathf.FloorToInt (timeLeft / 60.0f);
		int seconds = Mathf.RoundToInt (timeLeft - minutes * 60.0f);
		
		if (seconds < 10) {
			p1Timer.text = minutes + ":0" + seconds;
			p2Timer.text = minutes + ":0" + seconds;
		} else {
			p1Timer.text = minutes + ":" + seconds;
			p2Timer.text = minutes + ":" + seconds;
		}
	}
}
