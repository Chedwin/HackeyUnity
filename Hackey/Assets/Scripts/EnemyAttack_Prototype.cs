using UnityEngine;
using System.Collections;

/*public enum ENEMYSTATE {
	CHASE,
	ATTACK,
	DEAD
}*/

public class EnemyAttack_Prototype : MonoBehaviour {

	const float timeBetweenAttacks = 1.0f;
	public int attackDamage = 5;

	public float speed = 6.0f;

	Animator anim;
	Transform player;
	NavMeshAgent navEnemy;

	PlayerHealth_Prototype playerHealth;
	EnemyHealth_Prototype enemyHealth;

	public ENEMYSTATE enemyState;

	bool playerInRange;
	float timer = 0.0f;

	void Awake() {
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		playerHealth = player.GetComponent<PlayerHealth_Prototype> ();
		enemyHealth = GetComponent<EnemyHealth_Prototype> ();
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
		Debug.Log (timer);
		if (!playerInRange) {
			navEnemy.SetDestination (player.position);
			anim.SetFloat ("Speed", speed);	
		} 

		if (playerHealth.currentHealth <= 0) {
			anim.SetBool ("PlayerDead", true);
		} else {
			anim.SetBool ("PlayerDead", false);
		}
	}

	/*void OnTriggerEnter(Collider c) {

		if (c.gameObject.tag == "Player" && timer >= timeBetweenAttacks) {
			transform.LookAt (c.gameObject.transform.position);
			Attack ();
		}
	}*/
	//void OnTriggerExit() {}

	void OnTriggerStay(Collider c) {
		if (enemyState != ENEMYSTATE.DEAD) {
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
