using UnityEngine;
using System.Collections;

public class PuckProjectile : MonoBehaviour {

	HockeyPlayerAttack playerAttack;

	void Awake() {
		playerAttack = GameObject.Find ("Player").GetComponent<HockeyPlayerAttack> ();
	}

	// Use this for initialization
	void Start () {
		if (playerAttack.puckState == PuckState.FLAMING){
			gameObject.GetComponent<ParticleSystem>().enableEmission = true;

		}
		Destroy (gameObject, 2.0f);
	}
	
}