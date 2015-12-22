using UnityEngine;
using System.Collections;

public class Car_Enemy_Health : MonoBehaviour {

	HockeyPlayerAttack playerAttack;
	public int startingCarHealth = 1500;
	public int currentCarHealth;
	bool isDestroyed;

	public Transform explosion;

	LevelProgress levelProgress;

	void Awake() {
		playerAttack = GameObject.Find ("Player").GetComponent<HockeyPlayerAttack> ();
		levelProgress = GameObject.Find ("LevelProgressionSystem").GetComponent<LevelProgress> ();
	}

	// Use this for initialization
	void Start () {
		currentCarHealth = startingCarHealth;
		isDestroyed = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision c) {
		if (c.gameObject.tag == "Projectile") {
			if (playerAttack.puckState == PuckState.NORMAL) {
				DamageCar ((int)AttackDamageValue.PUCK);
			} else if (playerAttack.puckState == PuckState.FLAMING) {
				int fp = ((int)(AttackDamageValue.FLAMINGPUCK) * 2);
				DamageCar (fp);
			}
		}

		if (c.gameObject.tag == "EnemyCar") {
			Explode();
		}
	}

	public void DamageCar(int amount) {
		if (isDestroyed) {
			return;
		}

		currentCarHealth -= amount;

		if (currentCarHealth < (startingCarHealth / 3)) {
			gameObject.GetComponent<ParticleSystem>().enableEmission = true;
		}

		if (currentCarHealth <= 0) {
			Explode ();
		}
	}

	void Explode() {
		isDestroyed = true;

		Instantiate (explosion, transform.position, Quaternion.identity);
		if (gameObject.tag == "EnemyCar") {
			ScoreSystem.playerScore += 2000;
			levelProgress.gameStage = GAMESTAGE.FINISH;
		}
		Destroy (gameObject);
	}
}
