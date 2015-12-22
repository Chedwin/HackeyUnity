using UnityEngine;
using System.Collections;

public class KlefbombExplosion : MonoBehaviour {

	//Car_Enemy_Health carHealth;

	// Use this for initialization
	void Start () {
		//carHealth = GameObject.FindGameObjectWithTag ("EnemyCar").GetComponent<Car_Enemy_Health> ();
		Destroy (gameObject, 3.0f);
	}
	
	void OnTriggerEnter(Collider c) {
		if (c.gameObject.tag == "Enemy") {
			ScoreSystem.playerScore += SimpleEnemyHealth.scoreValue;
			SimpleEnemyManager.enemyKillCount++;
			Destroy (c.gameObject);
		} else if (c.gameObject.tag == "Montreal_Enemy") {
			ScoreSystem.playerScore += Montreal_Enemy_Health.scoreValue;
			Montreal_Enemy_Manager.mtlPlayerCount--;
			Destroy (c.gameObject);
		} else if (c.gameObject.tag == "EnemyCar") {
			//carHealth.currentCarHealth -= (carHealth.startingCarHealth / 3);
		}
	} // end OnTriggerEnter()
}
