using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public enum HealthState {
	NORMAL,
	DAMAGED,
	HEALED,
	DEAD
}

public class MultiplayerHealth : MonoBehaviour {
	public Text scoreText;
	string thisName;
	public int score;


	const int startingHealth = 200;
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

	public HealthState playerHealthState;

	Transform respawnPoint;

	public bool updateScore;
	public bool isDead;
	bool damaged;
	bool healed;

	void CheckWhichPlayer() {
		thisName = gameObject.transform.name;
		if (thisName == "Player1") {
			MultiplayerManager.p1Score = score;
		} 
		else if (thisName == "Player2") {
			MultiplayerManager.p2Score = score;
		} 
		else if (thisName == "Player3") {
			MultiplayerManager.p3Score = score;
		} 
		else if (thisName == "Player4") {
			MultiplayerManager.p4Score = score;
		}  
	}

	void Awake() {
		anim = GetComponent<Animator> ();
		//currentHealth = startingHealth;
		redZone = startingHealth / 4;

		score = 3;
		scoreText.text = "LIVES: " + score;
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
		if (isDead != true) {
			damaged = true;
			currentHealth -= amount;

			healthSlider.value = currentHealth;

			if (currentHealth <= 0) {
				Death ();
			}

			anim.SetTrigger ("Damaged");
			if (currentHealth <= redZone) {
				healthSliderFill.color = Color.red;
			}
		}
	}

	public void Heal(int amount) {
		Debug.Log ("bam");
		healed = true;
		currentHealth += amount;
		healthSlider.value = currentHealth;

		if (currentHealth > startingHealth) {
			currentHealth = startingHealth;
		}

		if (currentHealth > redZone) {
			healthSliderFill.color = Color.green;
		}
	}

	void Death() {
		isDead = true;
		TogglePlayer (false);
		score--;
		scoreText.text = "SCORE: " + score;
		CheckWhichPlayer ();

		if (score > 0) {
			StartCoroutine ("Respawn");
		}
	}

	IEnumerator Respawn() {
		updateScore = false;
		yield return new WaitForSeconds (3.0f);
		TogglePlayer (true);
		isDead = false;
		Heal (startingHealth);
	}

	void TogglePlayer(bool b) {
		player.SetActive (b);
		hipBone.SetActive (b);
		mouseLook.enabled = b;
		hockeyplayer.enabled = b;
		playerAttack.enabled = b;
	}

	void OnCollisionEnter(Collision c) {
		if (c.gameObject.tag == "Projectile") {
			int eP = (int)(AttackDamageValue.PUCK);
			TakeDamage (eP);
		}
	}
}
