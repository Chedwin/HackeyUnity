using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TestSpawn : MonoBehaviour {

	public Transform[] spawnPoints;
	public GameObject cube;
	public Text timeText;

	float time;
	float spawnTimer;
	float waveTimer;
	public float spawnTime = 3.0f;

	// Use this for initialization
	void Start () {
		// start with 3 enemies
		waveTimer = 0.0f;
	}

	void Update() {
		spawnTimer += Time.deltaTime;
		waveTimer += Time.deltaTime;

		//AdjustSpawnTimer (waveTimer);

		// spawn a new enemy every 5 seconds
		if (spawnTimer >= spawnTime) {
			Spawn ();
		}

		time += Time.deltaTime;

		int minutes = Mathf.FloorToInt (time / 60.0f);
		int seconds = Mathf.RoundToInt (time - minutes * 60.0f);

		if (seconds < 10) {
			timeText.text = minutes + ":0" + seconds;
		} else {
			timeText.text = minutes + ":" + seconds;
		}

	}

	void Spawn() {
		spawnTimer = 0.0f;



		Vector3 spawnOffset = new Vector3(Random.Range(0, 3), 0.0f, Random.Range (0, 3));
		int spawnPointIndex = Random.Range (0, spawnPoints.Length);

		Instantiate (cube, spawnPoints [spawnPointIndex].position + spawnOffset, spawnPoints [spawnPointIndex].rotation);
	}

	void AdjustSpawnTimer(float waveTimer) {

		// decrement every 30 seconds
		if (waveTimer <= 30.0f) {
			spawnTime = 5.0f;
		} else if ((waveTimer > 30.0f) && (waveTimer <= 60.0f)) {
			spawnTime = 4.0f;
		} else if ((waveTimer > 60.0f) && (waveTimer <= 90.0f)) {
			spawnTime = 3.5f;
		} else if ((waveTimer > 90.0f) && (waveTimer <= 120.0f)) {
			spawnTime = 3.0f;
		} else {
			spawnTime = 2.5f;
		}
	}
}
