using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PauseMenuHandler : MonoBehaviour {

	GameObject hud;
	public RectTransform pause;
	public AudioSource coachsCornerAudio;
	public AudioSource backgroundMusic;

	public HockeyPlayerHealth playerHealth;
	public LevelProgress levelProgress;
	
	bool isPaused;

	void Awake() {
		hud = GameObject.Find ("UI_HUD_Canvas");
		//coachsCornerAudio = GetComponent<AudioSource> ();
		backgroundMusic = GameObject.Find ("BackgroundMusicManager").GetComponent<AudioSource> ();

		playerHealth = GameObject.FindGameObjectWithTag ("Player").GetComponent<HockeyPlayerHealth> ();
		levelProgress = GameObject.Find ("LevelProgressionSystem").GetComponent<LevelProgress> ();
	}

	void Start() {
		pause.gameObject.SetActive (false);
		isPaused = false;
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

	void Update() {
		if (Input.GetButton ("Escape") && !isPaused && 
			(levelProgress.gameStage != GAMESTAGE.FINISH)   &&  (!playerHealth.isDead) )
		{
			Pause ();
		}
	}
	

	public void QuitGame() {
		Application.LoadLevel ("Title-Menu");
	}

	public void Resume() {
		Time.timeScale = 1.0f;

		coachsCornerAudio.Stop ();
		backgroundMusic.Play ();

		hud.SetActive (true);
		pause.gameObject.SetActive (false);
		isPaused = false;

		SetCursorLocked (true);
	}

	void Pause() {
		Time.timeScale = 0.0f;

		coachsCornerAudio.Play ();
		backgroundMusic.Pause ();

		isPaused = true;
		hud.SetActive (false);
		pause.gameObject.SetActive (true);

		SetCursorLocked (false);
	}
}