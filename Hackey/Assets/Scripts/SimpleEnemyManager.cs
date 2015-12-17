using UnityEngine;
using System.Collections;

public class SimpleEnemyManager : MonoBehaviour {

	public HockeyPlayerHealth playerHealth;
	public GameObject[] enemyList;

	float spawnTimer;
	float waveTimer;
	public float spawnTime;

	public Transform[] spawnPoints;

	public static int enemyKillCount;

	// Use this for initialization
	void Start () {
		// start with 3 enemies
		waveTimer = 0.0f;
		enemyKillCount = 0;
	}

	void Update() {
		spawnTimer += Time.deltaTime;
		waveTimer += Time.deltaTime;

		AdjustSpawnTimer (waveTimer);

		// spawn a new enemy every 5 seconds
		if (spawnTimer >= spawnTime) {
			Spawn ();
		}
	}
	
	void Spawn() {
		spawnTimer = 0.0f;

		if (playerHealth.currentHealth <= 0.0f) {
			return;
		}

		Vector3 spawnOffset = new Vector3(Random.Range(0, 3), 0.0f, Random.Range (0, 3));
		int spawnPointIndex = Random.Range (0, spawnPoints.Length);
		int enemyRandom = Random.Range (0, enemyList.Length);
		Instantiate (enemyList[enemyRandom], spawnPoints [spawnPointIndex].position + spawnOffset, spawnPoints [spawnPointIndex].rotation);
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