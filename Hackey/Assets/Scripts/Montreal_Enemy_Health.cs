using UnityEngine;
using System.Collections;


public enum MTLHEALTHSTATE {
	ALIVE,
	DEAD
};



public class Montreal_Enemy_Health : MonoBehaviour {

	public int mtlStartingHealth = 1000;
	public int mtlCurrentHealth;
	public static int scoreValue = 2000;

	MTLHEALTHSTATE mtlHealthState;
	HockeyPlayerAttack playerAttack;
	Animator playerAnim;
	Animator mtlAnim;
	public GameObject rewardDrop;

	// Use this for initialization
	void Start () {
		mtlHealthState = MTLHEALTHSTATE.ALIVE;
		playerAttack = GameObject.Find ("Player").GetComponent<HockeyPlayerAttack> ();
		playerAnim = GameObject.Find ("Player").GetComponent<Animator> ();
		mtlAnim = GetComponent<Animator> ();
		mtlCurrentHealth = mtlStartingHealth;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void TakeDamage(int amount) {
		if (mtlHealthState == MTLHEALTHSTATE.DEAD) {
			return;
		}

		mtlCurrentHealth -= amount;
		mtlAnim.SetTrigger ("Damaged");

		if (mtlCurrentHealth <= 0) {
			Death ();
		}
	}


	void Death() {
		mtlHealthState = MTLHEALTHSTATE.DEAD;

		playerAnim.SetTrigger ("Celebrate");
		StartCoroutine ("Drop");
		Montreal_Enemy_Manager.mtlPlayerCount--;
		ScoreSystem.playerScore += scoreValue;
		Destroy (gameObject);
	}

	void OnCollisionEnter(Collision c) {
		if (c.gameObject.tag == "Projectile") {
			if (playerAttack.puckState == PuckState.NORMAL) {
				TakeDamage((int)AttackDamageValue.PUCK);
			} else if (playerAttack.puckState == PuckState.FLAMING) {
				TakeDamage((int)AttackDamageValue.FLAMINGPUCK);
			}
		}

		if (c.gameObject.tag == "Melee") {
			TakeDamage((int)AttackDamageValue.MELEE);
		}
	}

	IEnumerator Drop() {
		Vector3 heightOffset = new Vector3 (0, 1.2f, 0.0f);
	
		Instantiate (rewardDrop, transform.position + heightOffset, Quaternion.identity);
		yield return new WaitForSeconds (2);
	}
} // end class
