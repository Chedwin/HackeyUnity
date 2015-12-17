using UnityEngine;
using System.Collections;

//[RequireComponent(typeof(CharacterController))]
public class HockeyPlayer : MonoBehaviour {

	public float speed = 6.0f;
	public float sprintSpeed = 1.0f;

	Animator anim;
	Rigidbody playerBody;

	void Awake() {;
		anim = GetComponent<Animator> ();
		playerBody = GetComponent<Rigidbody> ();
	}

	void FixedUpdate() {
		float h = Input.GetAxis ("Horizontal") * 0.5f;
		float v = Input.GetAxis("Vertical") * 0.75f;
		bool sprint = Input.GetButton ("Sprint");

		Move (h, v, sprint);
		Animating(v, sprint);
	}

	void Move(float h, float v, bool sprint) {
		Vector3 movementPosition = transform.TransformDirection (h, 0.0f, v);

		if (sprint) {
			Vector3 sprintPosition = transform.TransformDirection (h, 0.0f, v + sprintSpeed);
			playerBody.MovePosition(transform.position + sprintPosition);
		} else {
			playerBody.MovePosition (transform.position + movementPosition);
		}
	}

	void Animating(float v, bool sprint) {
		if (sprint) {
			anim.SetFloat("Speed", sprintSpeed);
		} else {
			anim.SetFloat ("Speed", v);
		}
	}
}
