using UnityEngine;
using System.Collections;

public class KlefbombExplosion : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Destroy (gameObject, 3.0f);
	}
	
	void OnTriggerEnter(Collider c) {
		if ( (c.gameObject.tag != "Environment") && 
		    (c.gameObject.tag != "Manager") && 
		    (c.gameObject.tag != "Player") && 
			(c.gameObject.tag != "Melee") )
		{
			if (c.gameObject.tag == "Enemy") {
				ScoreSystem.playerScore += SimpleEnemyHealth.scoreValue;
				SimpleEnemyManager.enemyKillCount++;
				Destroy(gameObject);
			}
		}
	} // end OnTriggerEnter()
}
