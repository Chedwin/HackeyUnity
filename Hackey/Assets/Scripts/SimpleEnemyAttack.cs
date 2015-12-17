using UnityEngine;
using System.Collections;

public enum ENEMYSTATE {
	CHASE,
	ATTACK,
	DEAD
}

public class SimpleEnemyAttack : MonoBehaviour {

	public float timeBetweenAttacks = 10.0f;
	public int attackDamage = 5;

	public float speed = 6.0f;

	Animator anim;
	Transform player;
	NavMeshAgent navEnemy;

	HockeyPlayerHealth playerHealth;
	SimpleEnemyHealth enemyHealth;

	public ENEMYSTATE enemyState;

	bool playerInRange;
	float timer;

	void Awake() {
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		playerHealth = player.GetComponent<HockeyPlayerHealth> ();
		enemyHealth = GetComponent<SimpleEnemyHealth> ();
		navEnemy = GetComponent<NavMeshAgent> ();
		anim = GetComponent<Animator> ();
	}

	// Use this for initialization
	void Start () {
		enemyState = ENEMYSTATE.CHASE;
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;

		if (!playerInRange) {
			navEnemy.SetDestination (player.position);
			anim.SetFloat ("Speed", speed);	
		} 

		if (playerHealth.currentHealth <= 0) {
			anim.SetBool("PlayerDead", true);
		}
	}

	void OnTriggerEnter() {}
	void OnTriggerExit() {}

	void OnTriggerStay(Collider c) {
		if (enemyState != ENEMYSTATE.DEAD) 
		{
			if (c.gameObject.tag == "Player" && timer >= timeBetweenAttacks) {
				transform.LookAt (c.gameObject.transform.position);
				Attack ();
			}
		}

	}

	void Attack() {
		if (playerHealth.currentHealth > 0) {
			timer = 0.0f; 
			anim.SetFloat ("Speed", 0);
			anim.SetTrigger ("Punch");
			playerHealth.TakeDamage (attackDamage);
		} 

	} // end Attack()
}
