using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TestSpawn : MonoBehaviour {

	public Transform[] spawnPoints;
	public GameObject[] enemyList;
	public GameObject cube;
	public Text timeText;

	float time;
	float spawnTimer;
	float EspawnTimer;
	float waveTimer;
	public float spawnTime = 3.0f;
	public float EspawnTime = 3.0f;

	// Use this for initialization
	void Start () {
		// start with 3 enemies
		waveTimer = 0.0f;
	}

	void Update() {
		spawnTimer += Time.deltaTime;
		EspawnTimer += Time.deltaTime;
		waveTimer += Time.deltaTime;

		//AdjustSpawnTimer (waveTimer);

		// spawn a new enemy every 5 seconds
		if (spawnTimer >= spawnTime) {
			Spawn ();
		}

		if (EspawnTimer >= EspawnTime) {
			SpawnEnemy ();
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

		Vector3 spawnOffset = new Vector3(Random.Range(0, 3), Random.Range (0, 5), Random.Range (0, 3));
		int spawnPointIndex = Random.Range (0, spawnPoints.Length);

		Instantiate (cube, spawnPoints [spawnPointIndex].position + spawnOffset, spawnPoints [spawnPointIndex].rotation);
	}

	void SpawnEnemy() {
		EspawnTimer = 0.0f;

		Vector3 spawnOffset = new Vector3(Random.Range(0, 3), Random.Range (0, 5), Random.Range (0, 3));
		int spawnPointIndex = Random.Range (0, spawnPoints.Length);

		int enemyRandom = Random.Range (0, enemyList.Length);
		Instantiate (enemyList[enemyRandom], spawnPoints [spawnPointIndex].position + spawnOffset, spawnPoints [spawnPointIndex].rotation);
	}

	void AdjustSpawnTimer(float waveTimer) {

		// decrement every 30 seconds
		if (waveTimer <= 30.0f) {
			spawnTime = 5.0f;
			EspawnTime = 10.0f;
		} else if ((waveTimer > 30.0f) && (waveTimer <= 60.0f)) {
			spawnTime = 4.0f;
			EspawnTime = 9.0f;
		} else if ((waveTimer > 60.0f) && (waveTimer <= 90.0f)) {
			spawnTime = 3.5f;
			EspawnTime = 8.0f;
		} else if ((waveTimer > 90.0f) && (waveTimer <= 120.0f)) {
			spawnTime = 3.0f;
			EspawnTime = 7.0f;
		} else {
			spawnTime = 2.5f;
			EspawnTime = 5.0f;
		}
	}
}
