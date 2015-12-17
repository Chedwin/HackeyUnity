using UnityEngine;
using System.Collections;

public enum PLAYERHIT {
	HIT,
	NOT_HIT
};

public class Car_Enemy_AI : MonoBehaviour {

	NavMeshAgent navCar;
	GameObject player;
	
	public static int carDamageValue = 50;

	PLAYERHIT playerHit;
	HockeyPlayerHealth playerHealth;
	float waitTimer;

	// Use this for initialization
	void Start () {
		navCar = GetComponent<NavMeshAgent> ();

		player = GameObject.FindGameObjectWithTag ("Player");
		playerHealth = GameObject.FindGameObjectWithTag ("Player").GetComponent<HockeyPlayerHealth>();

		if (!player) {
			Debug.Log ("player not found!");
		}

		playerHit = PLAYERHIT.NOT_HIT;
		waitTimer = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {

		switch (playerHit) {
		case PLAYERHIT.HIT:
			Wait ();
			return;
		case PLAYERHIT.NOT_HIT:
			Chase ();
			return;
		}

	}

	void Wait() {
		waitTimer += Time.deltaTime;

		if (waitTimer >= 2.0f) {
			playerHit = PLAYERHIT.NOT_HIT;
		}
	}

	void Chase() {
		if (playerHealth.currentHealth > 0) {
			navCar.SetDestination (player.transform.position);
		}
	}

	void OnCollisionEnter(Collision c) {
		if (c.gameObject.tag == "Player") {
			waitTimer = 0.0f;
			playerHit = PLAYERHIT.HIT;
		}
	}
}
