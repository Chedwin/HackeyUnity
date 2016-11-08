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
	FLAMING,
	KLEFBOMB
};

public class HockeyPlayerAttack : MonoBehaviour {

	public Image crosshair;
	public Transform puckSpawnPoint;

	public Transform target;

	public float timeBetweenPucks = 6.0f;
	public float range = 50.0f;

	public GameObject puck;
	public GameObject klefbombPuck;

	public PuckState puckState;

	public Text flamingPuckTimerText;

	float flamingPuckTimer;
	float flamingPuckTimeLimit = 31.0f;

	float klefbombTimer;

	float timer;
	Animator anim;

	bool aim;
	bool shoot;
	bool melee;

	public string fire1;
	public string fire2;
	public string aimmer;


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
		aim = Input.GetButton (aimmer);
		shoot = Input.GetButton (fire1);
		melee = Input.GetButton (fire2);

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
		else if (puckState == PuckState.KLEFBOMB) {
			KlefbombTimer ();
		}
	}

	void FlamingPuckTimer() {
		flamingPuckTimer += Time.deltaTime;
		int timeLeft = (int)(flamingPuckTimeLimit - flamingPuckTimer);
        flamingPuckTimerText.text = timeLeft + " seconds left";
        //Debug.Log(flamingPuckTimerText.text);
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

	void KlefbombTimer() {
		klefbombTimer += Time.deltaTime;
		int timeLeft = (int)(flamingPuckTimeLimit - klefbombTimer);

		//flamingPuckTimerText.text = timeLeft + " UNLEASH THE KLEFBOMB!";
		if (timeLeft <= 10) {
			flamingPuckTimerText.color = Color.red;
		}

		if (klefbombTimer >= flamingPuckTimeLimit) {
			puckState = PuckState.NORMAL;
			klefbombTimer = 0.0f;
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

		if (puckState != PuckState.KLEFBOMB) {
			GameObject tempPuck = Instantiate (puck, puckSpawnPoint.position, transform.rotation) as GameObject;
			tempPuck.GetComponent<Rigidbody> ().AddForce (shootDestination * 40.0f, ForceMode.Impulse);
		} else {
			GameObject tempPuck2 = Instantiate (klefbombPuck, puckSpawnPoint.position, transform.rotation) as GameObject;
			tempPuck2.GetComponent<Rigidbody> ().AddForce (shootDestination * 60.0f, ForceMode.Impulse);
		}
	
	} // end ShootPuck()

}
