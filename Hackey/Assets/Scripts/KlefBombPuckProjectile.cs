using UnityEngine;
using System.Collections;

public class KlefBombPuckProjectile : MonoBehaviour {

	public GameObject klefbombExplosion;
	float timer;

	// Use this for initialization
	void Start () {
		timer = 0.0f;
		Destroy (gameObject, 2.0f);
	}

	void Update() {
		timer += Time.deltaTime;
	}

	void OnCollisionEnter(Collision c) {
		if ( (timer >= 0.2f) && (c.gameObject.tag != "Environment") && 
		    (c.gameObject.tag != "Manager") && (c.gameObject.tag != "Player") ) 
		{
			Instantiate (klefbombExplosion, gameObject.transform.position, Quaternion.identity);
			Destroy(gameObject);
		}
	}
}
