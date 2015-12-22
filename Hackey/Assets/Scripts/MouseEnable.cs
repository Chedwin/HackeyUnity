using UnityEngine;
using System.Collections;

public class MouseEnable : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.None;
	}
}
