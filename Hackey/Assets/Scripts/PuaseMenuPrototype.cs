using UnityEngine;
using System.Collections;

public class PuaseMenuPrototype : MonoBehaviour {

    GameObject hud;
    public RectTransform pause;
    AudioSource coachsCornerAudio;
    AudioSource backgroundMusic;

    public PlayerHealth_Prototype playerHealth;

    public string pauseButton;
    bool isPaused;

    // Use this for initialization
    void Awake ()
    {
        hud = GameObject.Find("UI_HUD_Canvas");
        coachsCornerAudio = GetComponent<AudioSource>();
        backgroundMusic = GameObject.Find("BackgroundMusicManager").GetComponent<AudioSource>();
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth_Prototype>();
    }

    void Start()
    {
        Time.timeScale = 1.0f;
        pause.gameObject.SetActive(false);
        isPaused = false;
        Cursor.lockState = CursorLockMode.Confined;
        SetCursorLocked(true);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetButton(pauseButton) && (!playerHealth.isDead))
        {
            if (!isPaused) {
                Pause();
                return;
            }
            Resume();
        }
    }

    void SetCursorLocked(bool b)
    {
        if (b)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void QuitGame()
    {
        Application.LoadLevel("Title-Menu");
    }

    public void Resume()
    {
        Time.timeScale = 1.0f;

        coachsCornerAudio.Stop();
        backgroundMusic.Play();

        hud.SetActive(true);
        pause.gameObject.SetActive(false);
        isPaused = false;

        SetCursorLocked(true);
    }

    void Pause()
    {
        Time.timeScale = 0.0f;

        coachsCornerAudio.Play();
        backgroundMusic.Pause();

        isPaused = true;
        hud.SetActive(false);
        pause.gameObject.SetActive(true);

        SetCursorLocked(false);
    }
}
