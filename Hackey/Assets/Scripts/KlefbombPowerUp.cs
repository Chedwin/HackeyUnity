using UnityEngine;
using System.Collections;

public class KlefbombPowerUp : MonoBehaviour {
	float turnSpeed = 50.0f;
	HockeyPlayerAttack playerAttack;

	// Use this for initialization
	void Start () {
		playerAttack = GameObject.Find ("Player").GetComponent<HockeyPlayerAttack> ();
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (Vector3.one, turnSpeed * Time.deltaTime);
	}

	void OnTriggerEnter(Collider c) {
		if (c.gameObject.tag == "Player" && playerAttack.puckState != PuckState.KLEFBOMB) {
			playerAttack.puckState = PuckState.KLEFBOMB;
			Destroy (gameObject);
		}
	}

}
