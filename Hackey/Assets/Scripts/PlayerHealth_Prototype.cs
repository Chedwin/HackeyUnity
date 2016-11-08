﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth_Prototype : MonoBehaviour {

	enum HealthState {
		NORMAL,
		DAMAGED,
		HEALED,
		DEAD
	}

	const int startingHealth = 100;
	public int currentHealth;
	int redZone;

	public Slider healthSlider;
	public Image healthSliderFill;


	public Image damageImage;
	public Image healImage;
	public float flashSpeed = 5.0f;
	public Color flashColor = new Color(1.0f, 0.0f, 0.0f, 0.4f);
	public Color healthColor = new Color (0.0f, 1.0f, 0.0f, 0.4f);
	Animator anim;
	public GameObject player;
	public GameObject hipBone;

	MouseLook mouseLook;
	HockeyPlayer hockeyplayer;
	HockeyPlayerAttack playerAttack;

	HealthState playerHealthState;

	public bool isDead;
	bool damaged;
	bool healed;

	void Awake() {
		anim = GetComponent<Animator> ();
		currentHealth = startingHealth;
		redZone = startingHealth / 4;
	}

	// Use this for initialization
	void Start () {
		healthSliderFill.color = Color.green;
		mouseLook = GetComponent<MouseLook> ();
		hockeyplayer = GetComponent<HockeyPlayer> ();
		playerAttack = GetComponent<HockeyPlayerAttack> ();
		currentHealth = startingHealth;
		playerHealthState = HealthState.NORMAL;

	}

	// Update is called once per frame
	void Update () {
		if (damaged) {
			damageImage.color = flashColor;
		} else {
			damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
		}

		damaged = false;

		if (healed) {
			healImage.color = healthColor;
		} else {
			healImage.color = Color.Lerp(healImage.color, Color.clear, flashSpeed * Time.deltaTime);
		}

		healed = false;
	}

	public void TakeDamage(int amount) {
		damaged = true;
		currentHealth -= amount;

		healthSlider.value = currentHealth;
		anim.SetTrigger ("Damaged");

		if (currentHealth <= 0 && !isDead) {
			Death ();
		}

		if (currentHealth <= redZone) {
			healthSliderFill.color = Color.red;
		}
	}

	public void Heal(int amount) {
		healed = true;
		currentHealth += amount;
		healthSlider.value = currentHealth;

		if (currentHealth > redZone) {
			healthSliderFill.color = Color.green;
		}
	}

	void Death() {
		isDead = true;
		TogglePlayer (false);
		StartCoroutine ("Respawn");
	}

	void TogglePlayer(bool b) {
		player.SetActive (b);
		hipBone.SetActive (b);
		mouseLook.enabled = b;
		hockeyplayer.enabled = b;
		playerAttack.enabled = b;
	}

	/*void OnCollisionEnter(Collision c) {
		if (c.gameObject.tag == "Enemy_Projectile") {
			int eP = (int)(AttackDamageValue.FLAMINGPUCK);
			TakeDamage(eP);
		}

		if (c.gameObject.tag == "EnemyCar") {
			TakeDamage(Car_Enemy_AI.carDamageValue);
		}

		if (c.gameObject.tag == "Projectile") {
			int eP = (int)(AttackDamageValue.PUCK);
			TakeDamage (eP);
		}
	}*/

	IEnumerator Respawn() {
		Debug.Log ("Respawning in 5 seconds");
		yield return new WaitForSeconds (5.0f);
		TogglePlayer (true);
		isDead = false;
		playerHealthState = HealthState.NORMAL;
		Heal (startingHealth);
	}
}