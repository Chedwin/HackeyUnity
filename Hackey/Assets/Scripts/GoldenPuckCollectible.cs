using UnityEngine;
using System.Collections;

public class GoldenPuckCollectible : MonoBehaviour {

	public int gpScore = 100;
	float turnSpeed = 50.0f;
	
	Animator playerAnim;

	void Awake() {
		playerAnim = GameObject.Find ("Player").GetComponent<Animator> ();
	}
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (Vector3.one, turnSpeed * Time.deltaTime);
	}

	void OnTriggerEnter(Collider c) {
		if (c.gameObject.tag == "Player") {
			ScoreSystem.playerScore += gpScore;
			ScoreSystem.gpCounter++;

			Destroy (gameObject);
			playerAnim.SetTrigger("Celebrate");
		}
	}
}
