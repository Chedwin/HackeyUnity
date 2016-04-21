using UnityEngine;
using System.Collections;

//[RequireComponent(typeof(CharacterController))]
public class HockeyPlayer : MonoBehaviour {

	public float sprintSpeed = 2.0f;

	public string horizontal;
	public string vertical;
	public string sprinter;

	Animator anim;
	Rigidbody playerBody;

	float acceleration = 1.0f;

	void Awake() {;
		anim = GetComponent<Animator> ();
		playerBody = GetComponent<Rigidbody> ();
	}

	void Start() {
		Cursor.lockState = CursorLockMode.Confined;
		SetCursorLocked (true);
	}

	void SetCursorLocked(bool b) {
		if (b) {
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		} else {
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		}
	}

	void FixedUpdate() {
		float h = Input.GetAxis (horizontal) * 0.5f;
		float v = Input.GetAxis(vertical) * 0.5f;
		bool sprint = Input.GetButton (sprinter);

		Move (h, v, sprint);
		Animating(v, sprint);
	}

	void Move(float h, float v, bool sprint) {
		Vector3 movementPosition = transform.TransformDirection (h, 0.0f, v);

		if (sprint) {
			acceleration += Time.deltaTime;
			Vector3 sprintPosition = transform.TransformDirection (h, 0.0f, v * acceleration);

			playerBody.MovePosition(transform.position + sprintPosition);

			if (acceleration > 2.0f) {
				acceleration = 2.0f;
			}
		} else {
			playerBody.MovePosition (transform.position + movementPosition);
			acceleration = 1.0f;
		}
	}

	void Animating(float v, bool sprint) {
		if (sprint) {
			anim.SetFloat("Speed", 9.0f);
		} else {
			anim.SetFloat ("Speed", v);
		}
	}
}
