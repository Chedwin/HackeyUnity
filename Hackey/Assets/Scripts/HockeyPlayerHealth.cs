using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HockeyPlayerHealth : MonoBehaviour {

	enum HealthState {
		NORMAL,
		DAMAGED,
		HEALED,
		DEAD
	}

	public int startingHealth = 100;
	public int currentHealth;
	int redZone;

	public Slider healthSlider;
	public Image healthSliderFill;

	public AudioSource nothingtoloseAudio;
	public AudioSource backgroundMusic;

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

	bool isDead;
	bool damaged;
	bool healed;

	void Awake() {
		anim = GetComponent<Animator> ();
		nothingtoloseAudio = GetComponent<AudioSource> ();
		backgroundMusic = GameObject.Find ("BackgroundMusicManager").GetComponent<AudioSource> ();
		currentHealth = startingHealth;
		redZone = startingHealth / 4;
	}

	// Use this for initialization
	void Start () {
		healthSliderFill.color = Color.green;
		mouseLook = GetComponent<MouseLook> ();
		hockeyplayer = GetComponent<HockeyPlayer> ();
		playerAttack = GetComponent<HockeyPlayerAttack> ();

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

		if (currentHealth <= 0 && !isDead) {
			Death ();
		}

		anim.SetTrigger ("Damaged");
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
		DisablePlayer ();
		backgroundMusic.Stop ();
		nothingtoloseAudio.Play ();
		StartCoroutine ("GameOver");
	}

	void DisablePlayer() {
		player.SetActive (false);
		hipBone.SetActive (false);
		mouseLook.enabled = false;
		hockeyplayer.enabled = false;
		playerAttack.enabled = false;
	}

	void OnCollisionEnter(Collision c) {
		if (c.gameObject.tag == "Enemy_Projectile") {
			int eP = (int)(AttackDamageValue.FLAMINGPUCK);
			TakeDamage(eP);
		}

		if (c.gameObject.name == "EnemyCar") {
			TakeDamage(Car_Enemy_AI.carDamageValue);
		}
	}

	IEnumerator GameOver() {
		yield return new WaitForSeconds(14.0f);
		Application.LoadLevel ("GameOver");
	}
}
