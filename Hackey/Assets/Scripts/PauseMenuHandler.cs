using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PauseMenuHandler : MonoBehaviour {

	GameObject hud;
	public RectTransform pause;
	public AudioSource coachsCornerAudio;
	public AudioSource backgroundMusic;
	
	bool isPaused;

	void Awake() {
		hud = GameObject.Find ("UI_HUD_Canvas");
		coachsCornerAudio = GetComponent<AudioSource> ();
		backgroundMusic = GameObject.Find ("BackgroundMusicManager").GetComponent<AudioSource> ();
	}

	void Start() {
		pause.gameObject.SetActive (false);
		isPaused = false;
		Cursor.visible = false;
	}

	void Update() {
//		if (Input.GetButton ("Escape") && (pauseToggle == PAUSETOGGLE.PLAY)) {
//			Pause ();
//		} else if (Input.GetButton ("Escape") && (pauseToggle == PAUSETOGGLE.PAUSE)) {
//			Resume();
//		}
		if (Input.GetButton ("Escape") && !isPaused) {
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
		Cursor.visible = false;
	}

	void Pause() {
		Time.timeScale = 0.0f;
		coachsCornerAudio.Play ();
		backgroundMusic.Pause ();
		isPaused = true;
		hud.SetActive (false);
		pause.gameObject.SetActive (true);
		Cursor.visible = true;
	}
}