using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public enum GAMESTAGE {
	ENEMIES,
	MTL,
	CAR,
	FINISH,
	GAMEOVER
}

public class LevelProgress : MonoBehaviour {

	public int levelProgressScore = 10000;
	public int minEnemyCount = 50;

	public GameObject enemyCar;
	public Transform carSpawn;

	public GameObject levelTimerImage;
	public Text levelTimerText;

	public AudioSource championsAudio;
	public AudioSource backgroundMusic;

	float gameOverTimer;
	public float gameOverTimeLimit = 180.0f;

	public GAMESTAGE gameStage;

	public bool finishedGame;

	GameObject sEnmManger;
	Montreal_Enemy_Manager mtlManager;

	Car_Enemy_Health enemyCarScriptRef;
	
	Animator playerAnim;

	HockeyPlayer playerMovement;
	MouseLook playerMouseLook;
	HockeyPlayerAttack playerAttack;

	void Awake() {
		sEnmManger = GameObject.Find ("SimpleEnemyManager");
		mtlManager = GetComponent<Montreal_Enemy_Manager> ();
		mtlManager.enabled = false;


		championsAudio = GetComponent<AudioSource> ();
		backgroundMusic = GameObject.Find ("BackgroundMusicManager").GetComponent<AudioSource> ();

		playerAnim = GameObject.Find ("Player").GetComponent<Animator> ();
		playerMovement = GameObject.Find ("Player").GetComponent<HockeyPlayer> ();
		playerMouseLook = GameObject.Find ("Player").GetComponent<MouseLook> ();
		playerAttack = GameObject.Find ("Player").GetComponent<HockeyPlayerAttack> ();
	}

	// Use this for initialization
	void Start () {
		gameStage = GAMESTAGE.ENEMIES;

		sEnmManger.SetActive (true);

		levelTimerImage.SetActive (false);

		finishedGame = false;

		Time.timeScale = 1.0f;
	}
	
	// Update is called once per frame
	void Update () {

		switch (gameStage) {
		case GAMESTAGE.ENEMIES:
			SimpleEnemyStage();
			return;
		case GAMESTAGE.MTL:
			MtlStage();
			return;
		case GAMESTAGE.CAR:
			CarStage ();
			return;
		case GAMESTAGE.FINISH:
			if (!finishedGame) {
				FinishGame();
			}
			return;
		case GAMESTAGE.GAMEOVER:
			GameOver();
			return;
		}

	}

	void SimpleEnemyStage() {
		if ((ScoreSystem.playerScore >= levelProgressScore) && (SimpleEnemyManager.enemyKillCount >= minEnemyCount)){
			sEnmManger.SetActive(false);

			mtlManager.enabled = true;
			gameStage = GAMESTAGE.MTL;
			playerAnim.SetTrigger("Celebrate");
			levelTimerImage.SetActive (true);
		}
	}

	void MtlStage() {

		gameOverTimer += Time.deltaTime;
		float timeLeft = gameOverTimeLimit - gameOverTimer;

		int minutes = Mathf.FloorToInt (timeLeft / 60.0f);
		int seconds = Mathf.RoundToInt (timeLeft - minutes * 60.0f);

		if (seconds < 10) {
			levelTimerText.text = minutes + ":0" + seconds;
		} else {
			levelTimerText.text = minutes + ":" + seconds;
		}

		if (Montreal_Enemy_Manager.mtlPlayerCount == 0) {
			mtlManager.enabled = false;
			//enemyCar.SetActive (true);
			Instantiate (enemyCar, carSpawn.position, Quaternion.identity);

			gameStage = GAMESTAGE.CAR;
			playerAnim.SetTrigger ("Celebrate");
			gameOverTimer = 0.0f;

		} 
		if ((Montreal_Enemy_Manager.mtlPlayerCount > 0) && (timeLeft <= 0.0f) ){
			levelTimerText.text = "0:00";
			StartCoroutine("GameOver");
		}


	}

	void CarStage() {
		//sEnmManger.SetActive (true);
		gameOverTimer += Time.deltaTime;

		float timeLeft = gameOverTimeLimit - gameOverTimer;
		
		int minutes = Mathf.FloorToInt (timeLeft / 60.0f);
		int seconds = Mathf.RoundToInt (timeLeft - minutes * 60.0f);
		
		if (seconds < 10) {
			levelTimerText.text = minutes + ":0" + seconds;
		} else {
			levelTimerText.text = minutes + ":" + seconds;
		}

		if ((enemyCar) && (timeLeft <= 0.0f) ){
			levelTimerText.text = "0:00";
			StartCoroutine("GameOver");
		}
	}

	void FinishGame() {
		playerAnim.SetBool ("BeatGame", true);
		DisablePlayerControl ();
		finishedGame = true;
		backgroundMusic.Stop ();
		championsAudio.Play ();
		StartCoroutine ("YouWin");
	}

	IEnumerator GameOver() {
		DisablePlayerControl ();
		yield return new WaitForSeconds (1.0f);
		Application.LoadLevel ("GameOver");
	}

	IEnumerator YouWin() {
		yield return new WaitForSeconds (16.0f);
		Application.LoadLevel ("WinScreen");
	}

	void DisablePlayerControl() {
		playerMovement.enabled = false;
		playerMouseLook.enabled = false;
		playerAttack.enabled = false;
	}
	
}
