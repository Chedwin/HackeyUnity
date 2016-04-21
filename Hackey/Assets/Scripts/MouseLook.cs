using UnityEngine;
using System.Collections;

/// MouseLook rotates the transform based on the mouse delta.
/// Minimum and Maximum values can be used to constrain the possible rotation

/// To make an FPS style character:
/// - Create a capsule.
/// - Add the MouseLook script to the capsule.
///   -> Set the mouse look to use LookX. (You want to only turn character but not tilt it)
/// - Add FPSInputController script to the capsule
///   -> A CharacterMotor and a CharacterController component will be automatically added.

/// - Create a camera. Make the camera a child of the capsule. Reset it's transform.
/// - Add a MouseLook script to the camera.
///   -> Set the mouse look to use LookY. (You want the camera to tilt up and down like a head. The character already turns.)
[AddComponentMenu("Camera-Control/Mouse Look")]
public class MouseLook : MonoBehaviour {

	public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
	public RotationAxes axes = RotationAxes.MouseXAndY;
	public float sensitivityX = 15F;
	public float sensitivityY = 15F;

	public float joysensitivityX = 3F;
	public float joysensitivityY = 3F;

	public float minimumX = -360F;
	public float maximumX = 360F;

	public float minimumY = -60F;
	public float maximumY = 60F;

	float rotationY = 0F;


	public string joyX;
	public string joyY;
	public string mouseX;
	public string mouseY;

	void Start ()
	{
		// Make the rigid body not change rotation
		if (GetComponent<Rigidbody> ()) {
			GetComponent<Rigidbody> ().freezeRotation = true;
		}
	}

	void FixedUpdate ()
	{
		float Xon = Mathf.Abs (Input.GetAxis (joyX));
		float Yon = Mathf.Abs (Input.GetAxis (joyY));

		if (axes == RotationAxes.MouseXAndY) {
			float rotationX = transform.localEulerAngles.y + Input.GetAxis (mouseX) * sensitivityX;
			
			rotationY += Input.GetAxis (mouseY) * sensitivityY;
			rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);
			
			transform.localEulerAngles = new Vector3 (-rotationY, rotationX, 0);
		}
		else if (axes == RotationAxes.MouseX)
		{
			if (Xon>.05){
				transform.Rotate(0, Input.GetAxis(joyX) * joysensitivityX, 0);
			}
			transform.Rotate(0, Input.GetAxis(mouseX) * sensitivityX, 0);
		}
		else
		{
			if (Yon>.05){
				rotationY += Input.GetAxis(joyY) * joysensitivityY;
			}
			rotationY += Input.GetAxis(mouseY) * sensitivityY;
			rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);
			
			transform.localEulerAngles = new Vector3(-rotationY, transform.localEulerAngles.y, 0);
		}


	}

}