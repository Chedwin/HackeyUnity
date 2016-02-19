using UnityEngine;
using System.Collections;

public class KlefbombExplosion : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Destroy (gameObject, 3.0f);
	}
	
	void OnTriggerStay(Collider c) 
	{
		if (c.gameObject.tag == "Enemy") 
		{
			ScoreSystem.playerScore += SimpleEnemyHealth.scoreValue;
			SimpleEnemyManager.enemyKillCount++;
			Destroy (c.gameObject);
		} 
		else if (c.gameObject.tag == "Montreal_Enemy") 
		{
			ScoreSystem.playerScore += Montreal_Enemy_Health.scoreValue;
			Montreal_Enemy_Manager.mtlPlayerCount--;
			Destroy (c.gameObject);
		} 
	} // end OnTriggerEnter()
}
