using UnityEngine;
using System.Collections;

public class EnemyHealth_Prototype : MonoBehaviour {

	private int startingHealth = 30;
	public int currentHealth;

	public static int scoreValue = 10;
	HockeyPlayerAttack playerAttack;

	EnemyAttack_Prototype healthEnemyState;

	Animator anim;
	CapsuleCollider capsuleCollider;

	bool isDead;
	bool isSinking;

	// Use this for initialization
	void Awake () {
		anim = GetComponent<Animator> ();
		capsuleCollider = GetComponent<CapsuleCollider> ();

		healthEnemyState = GetComponent<EnemyAttack_Prototype> ();

		playerAttack = GameObject.Find ("Player").GetComponent<HockeyPlayerAttack> ();

	}

	void Start() {
		currentHealth = startingHealth; 
	}

	// Update is called once per frame
	void Update () {

	}

	public void TakeDamage(int amount) {
		if (isDead)
			return;

		currentHealth -= amount;

		if (currentHealth <= 0) {
			Death();
		}
	}

	public void Death() {
		isDead = true;
		capsuleCollider.isTrigger = true;
		anim.SetTrigger ("Dies");

		healthEnemyState.enemyState = ENEMYSTATE.DEAD;

		//ScoreSystem.playerScore += scoreValue;
		//SimpleEnemyManager.enemyKillCount++;
		//Debug.Log (SimpleEnemyManager.enemyKillCount);

		Destroy (gameObject, 2.0f);
	}

	void OnCollisionEnter(Collision c) {
		if (c.gameObject.tag == "Projectile") {
			anim.SetTrigger ("Hit");

			if (playerAttack.puckState == PuckState.NORMAL) {
				TakeDamage ((int)AttackDamageValue.PUCK);
			} else if (playerAttack.puckState == PuckState.FLAMING) {
				TakeDamage ((int)AttackDamageValue.FLAMINGPUCK);
			}
		}

		if (c.gameObject.tag == "Melee") {
			anim.SetTrigger ("Hit");
			TakeDamage ((int)AttackDamageValue.MELEE);
		}
	}
}
