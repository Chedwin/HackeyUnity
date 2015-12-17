using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public enum AttackDamageValue {
	PUCK = 20,
	MELEE = 20,
	FLAMINGPUCK = 40
};

public enum PuckState {
	NORMAL,
	FLAMING
};

public class HockeyPlayerAttack : MonoBehaviour {

	public Image crosshair;
	public Transform puckSpawnPoint;

	public Transform target;

	public float timeBetweenPucks = 6.0f;
	public float range = 50.0f;

	public GameObject puck;
	public PuckState puckState;

	public Text flamingPuckTimerText;

	float flamingPuckTimer;
	float flamingPuckTimeLimit = 31.0f;

	float timer;
	Animator anim;

	bool aim;
	bool shoot;
	bool melee;

	// Use this for initialization
	void Awake() {
		anim = GetComponentInParent<Animator> ();
		flamingPuckTimerText.text = "";
	}

	void Start() {
		timer = 0.0f;
		puckState = PuckState.NORMAL;
		crosshair.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		aim = Input.GetButton ("Aim");
		shoot = Input.GetButton ("Fire1");
		melee = Input.GetButton ("Fire2");

		if (aim) {
			Aim ();
		} else {
			crosshair.enabled = false;
		}
		
		if (shoot && timer > timeBetweenPucks) {
			Shoot ();
		}
		else if (melee && timer >= timeBetweenPucks) {
			Melee ();
		}

		if (puckState == PuckState.FLAMING) {
			FlamingPuckTimer();
		}
	}

	void FlamingPuckTimer() {
		flamingPuckTimer += Time.deltaTime;
		int timeLeft = (int)(flamingPuckTimeLimit - flamingPuckTimer);
		flamingPuckTimerText.text = timeLeft + " seconds left";

		if (timeLeft <= 10) {
			flamingPuckTimerText.color = Color.red;
		}

		if (flamingPuckTimer >= flamingPuckTimeLimit) {
			puckState = PuckState.NORMAL;
			flamingPuckTimer = 0.0f;
			flamingPuckTimerText.text = "";
			flamingPuckTimerText.color = Color.white;
		}
	}
	
	void Aim() {
		crosshair.enabled = true;
	}
	
	void Melee() {
		timer = 0.0f;
		anim.SetTrigger ("Melee");
	}

	void Shoot() {
		timer = 0.0f;
		anim.SetTrigger ("Shoot");
	}

	void ShootPuck() {
	
		Vector3 shootDestination = (target.position - puckSpawnPoint.position).normalized;
		GameObject tempPuck = Instantiate (puck, puckSpawnPoint.position, transform.rotation) as GameObject;
		tempPuck.GetComponent<Rigidbody> ().AddForce (shootDestination * 40.0f, ForceMode.Impulse);
	
	} // end ShootPuck()

}
