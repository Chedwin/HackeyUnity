using UnityEngine;
using System.Collections;


[RequireComponent(typeof(CharacterController))]
public class CivilianMovement : MonoBehaviour {

    Animator anim;
    CharacterController cc;
    public float speed = 6.0f;

	// Use this for initialization
	void Start () {
        cc = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        cc.Move(new Vector3(h, 0.0f, v));

        anim.SetFloat("Speed", v);
        Debug.Log(v);
	}
}
