﻿using UnityEngine;
using System.Collections;

public enum MTLSTATE {
	CHASE,
	ATTACK,
	CELEBRATE
};

public class Montreal_Enemy_Attack : MonoBehaviour {
	
	GameObject player;
	Animator anim;
	NavMeshAgent mtlNav;

	public float timeBetweenPucks = 3.0f;
	float timer;

	public Transform puckSpawnPoint;
	GameObject playerShootTarget;
	public GameObject puck;

	HockeyPlayerHealth playerHealth;

	public float maxRange;
	public float minRange = 2.0f;

	public float speed = 6.0f;

	Vector3 playerTarget;

	MTLSTATE mtlState;
	public bool isMTL = true;

	// Use this for initialization
	void Start () {
		//nav = GetComponent<NavMeshAgent> ();
		anim = GetComponent<Animator> ();
		mtlNav = GetComponent<NavMeshAgent> ();

		mtlState = MTLSTATE.CHASE;
		player = GameObject.FindGameObjectWithTag ("Player");

		playerShootTarget = GameObject.Find ("EnemyPuckShootingTarget");
		playerHealth = GameObject.Find ("Player").GetComponent<HockeyPlayerHealth> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (playerTarget == null) 
			return;

		transform.LookAt(player.transform);
//		float distance = Vector3.Distance(transform.position, playerTarget);
//		bool tooClose = distance < minRange;
//		Vector3 direction = tooClose ? Vector3.back : Vector3.forward;
//		transform.Translate(direction * Time.deltaTime);
//		anim.SetFloat ("Speed", speed);
		timer += Time.deltaTime;

		switch (mtlState) {
		case MTLSTATE.CHASE:
			Chase ();
			return;
		case MTLSTATE.ATTACK:
			Attack ();
			return;
		case MTLSTATE.CELEBRATE:
			Celebrate();
			return;
		}
	}

	void Chase() {
		mtlNav.SetDestination (player.transform.position);
		anim.SetFloat ("Speed", speed);
	}

	void Attack() {
		anim.SetFloat ("Speed", 0.0f);

		if (playerHealth.currentHealth >= 0) {
			if (timer >= timeBetweenPucks) {
				Shoot ();
			}
		} else {
			mtlState = MTLSTATE.CELEBRATE;
		}

	}

	void Shoot() {
		timer = 0.0f;
		anim.SetTrigger ("Shoot");
	}

	void Celebrate() {
		anim.SetBool ("BeatGame", true);
		mtlNav.speed = 0.0f;
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Player") {
			mtlState = MTLSTATE.ATTACK;
		}
	}
	
	void OnTriggerExit(Collider other) {
		if (other.tag == "Player") {
			mtlState = MTLSTATE.CHASE;
		}
	}

	void ShootPuck() {
		Vector3 shootDestination = (playerShootTarget.transform.position - puckSpawnPoint.position).normalized;
		GameObject tempPuck = Instantiate (puck, puckSpawnPoint.position, transform.rotation) as GameObject;
		tempPuck.GetComponent<Rigidbody> ().AddForce (shootDestination * 40.0f, ForceMode.Impulse);
	}
}
