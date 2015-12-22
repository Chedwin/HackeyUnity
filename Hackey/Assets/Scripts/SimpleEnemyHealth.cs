using UnityEngine;
using System.Collections;

public class SimpleEnemyHealth : MonoBehaviour {
	private int startingHealth = 30;
	public int currentHealth;


	public GameObject healthDrop;
	public GameObject gpDrop;
	public GameObject flamingPuckDrop;
	public GameObject klefbombDrop;

	public static int scoreValue = 10;
	HockeyPlayerAttack playerAttack;
	LevelProgress levelProgress;

	SimpleEnemyAttack healthEnemyState;

	Animator anim;
	CapsuleCollider capsuleCollider;

	bool isDead;
	bool isSinking;

	// Use this for initialization
	void Awake () {
		anim = GetComponent<Animator> ();
		capsuleCollider = GetComponent<CapsuleCollider> ();

		healthEnemyState = GetComponent<SimpleEnemyAttack> ();

		playerAttack = GameObject.Find ("Player").GetComponent<HockeyPlayerAttack> ();
		levelProgress = GameObject.Find ("LevelProgressionSystem").GetComponent<LevelProgress> ();
	}

	void Start() {
		currentHealth = startingHealth; 
	}
	
	// Update is called once per frame
	void Update () {
		if (levelProgress.gameStage != GAMESTAGE.ENEMIES) {
			Destroy (gameObject);
		}
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

		ScoreSystem.playerScore += scoreValue;
		SimpleEnemyManager.enemyKillCount++;
		Debug.Log (SimpleEnemyManager.enemyKillCount);

		StartCoroutine ("Drop");
		Destroy (gameObject, 2.0f);
	}

	void OnCollisionEnter(Collision c) {
		if (c.gameObject.tag == "Projectile") {
			anim.SetTrigger("Hit");

			if (playerAttack.puckState == PuckState.NORMAL) {
				TakeDamage((int)AttackDamageValue.PUCK);
			} else if (playerAttack.puckState == PuckState.FLAMING) {
				TakeDamage((int)AttackDamageValue.FLAMINGPUCK);
			}
		}

		if (c.gameObject.tag == "Melee") {
			anim.SetTrigger("Hit");
			TakeDamage((int)AttackDamageValue.MELEE);
		}

		if (c.gameObject.tag == "EnemyCar") {
			Death ();
		}
	}

	IEnumerator Drop() {
		int random = Random.Range (0, 100);
		Vector3 heightOffset = new Vector3 (0, 2.0f, 0.0f);

		if ((0 <= random) && (random <= 20)) {
			Instantiate (healthDrop, transform.position + heightOffset, Quaternion.identity);
		} else if ((30 < random) && (random <= 40)) {
			Instantiate (flamingPuckDrop, transform.position + heightOffset, Quaternion.identity);
		} else if ((50 < random) && (random <= 60)) {
			Instantiate (gpDrop, transform.position + heightOffset, Quaternion.identity);
		} else if ((70 < random) && (random <= 75)) {
			Instantiate (klefbombDrop, transform.position + heightOffset, Quaternion.identity);
		}

		yield return new WaitForSeconds (2);
	}
}
