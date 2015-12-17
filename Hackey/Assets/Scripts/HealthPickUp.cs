using UnityEngine;
using System.Collections;

public class HealthPickUp : MonoBehaviour {

	HockeyPlayerHealth playerHealth;
	public int healthUpgrade = 50;

	Transform player;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		playerHealth = player.GetComponent<HockeyPlayerHealth> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider c) {

		if (c.gameObject.tag == "Player") { 
			if (playerHealth.currentHealth < playerHealth.startingHealth) {
				if (playerHealth.currentHealth > (playerHealth.startingHealth * 0.75f)) {
					playerHealth.currentHealth = playerHealth.startingHealth;
					playerHealth.healthSlider.value = playerHealth.startingHealth;
				} else {
					playerHealth.Heal(healthUpgrade);
				}

				Destroy (gameObject);
			}
		}

	} // end OnTriggerEnter()
}
