using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TutorialScreen : MonoBehaviour {

	public RectTransform welcomePanel;
	public RectTransform controlPanel;

	GameObject hudUI;
	GameObject pauseUI;

	// Use this for initialization
	void Start () {
		hudUI = GameObject.Find ("UI_HUD_Canvas");
		pauseUI = GameObject.Find ("UI_Pause_Canvas");

		welcomePanel.gameObject.SetActive (true);
		controlPanel.gameObject.SetActive (false);

		hudUI.SetActive (false);
		pauseUI.SetActive (false);
		Time.timeScale = 0.0f;
	}

	// Update is called once per frame
	void Update () {

	}

	public void NextButton() {
		welcomePanel.gameObject.SetActive (false);
		controlPanel.gameObject.SetActive (true);
	}

	public void LetsPlayButton() {
		Time.timeScale = 1.0f;
		controlPanel.gameObject.SetActive (false);
		hudUI.SetActive (true);
	}
}
