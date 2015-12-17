using UnityEngine;
using System.Collections;

public class FlamingPuckPowerUp : MonoBehaviour {

	HockeyPlayerAttack playerAttack;
	float turnSpeed = 50.0f;

	// Use this for initialization
	void Start () {
		playerAttack = GameObject.Find ("Player").GetComponent<HockeyPlayerAttack> ();
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (Vector3.one, turnSpeed * Time.deltaTime);
	}

	void OnTriggerEnter(Collider c) {
		if (c.gameObject.tag == "Player") {
			if (playerAttack.puckState == PuckState.NORMAL) {
				playerAttack.puckState = PuckState.FLAMING;
				Destroy (gameObject);
			}
		}
	}
}
