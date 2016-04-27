using UnityEngine;
using System.Collections;

public class MultiplayerPause : MonoBehaviour {

	public RectTransform pause;
	public AudioSource coachsCornerAudio;
	public AudioSource backgroundMusic;

	bool isPaused;

	void SetCursorLocked(bool b) {
		if (b) {
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		} else {
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		}
	}

	void Awake() {
		coachsCornerAudio = GetComponent<AudioSource> ();
		backgroundMusic = GameObject.Find ("BackgroundMusicManager").GetComponent<AudioSource> ();
	}

	// Use this for initialization
	void Start () {
		pause.gameObject.SetActive (false);
		isPaused = false;
		Cursor.lockState = CursorLockMode.Confined;
		SetCursorLocked (true);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButton ("Escape"))
		{
			Debug.Log ("Pause");
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

		pause.gameObject.SetActive (false);
		isPaused = false;

		SetCursorLocked (true);
	}

	void Pause() {
		Time.timeScale = 0.0f;

		coachsCornerAudio.Play ();
		backgroundMusic.Pause ();

		isPaused = true;
		pause.gameObject.SetActive (true);

		SetCursorLocked (false);
	}
}
