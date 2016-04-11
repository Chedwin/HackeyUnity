using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour {
	public Toggle muteToggle;
	public Text soundLabel;

	public Slider loadingSlider;
	public Text percentText;

	public RectTransform panelTitle;
	public RectTransform panelOptions;
	public RectTransform panelCredits;
	public RectTransform panelLoading;
	public RectTransform panelGameSelect;

	private int loadingProgress = 0;

	// Use this for initialization
	void Start () {
		muteToggle.onValueChanged.AddListener (Mute);

		panelTitle.gameObject.SetActive (true);

		panelOptions.gameObject.SetActive (false);
		panelCredits.gameObject.SetActive (false);
		panelLoading.gameObject.SetActive (false);
		panelGameSelect.gameObject.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void ShowHideTitle(bool b) {
		panelTitle.gameObject.SetActive (b);
	}

	public void GameSelect() {
		ShowHideTitle (false);
		panelGameSelect.gameObject.SetActive (true);
	}

	public void StartSingleGame() {
		panelGameSelect.gameObject.SetActive (false);
		panelLoading.gameObject.SetActive (true);
		StartCoroutine (LoadScreen("Town"));
	}

	public void StartTwoPlayerGame() {
		panelGameSelect.gameObject.SetActive (false);
		panelLoading.gameObject.SetActive (true);
		StartCoroutine(LoadScreen("Multiplayer"));
	}

	public void Options() {
		ShowHideTitle (false);
		panelOptions.gameObject.SetActive (true);
	}

	public void Credits() {
		ShowHideTitle (false);

		panelCredits.gameObject.SetActive (true);
	}

	public void QuitGame() {
		Debug.Log ("Quit game!");
		Application.Quit ();
	}

	public void BacktoTitle() {
		panelOptions.gameObject.SetActive (false);
		panelCredits.gameObject.SetActive (false);
		panelGameSelect.gameObject.SetActive (false);

		ShowHideTitle (true);
	}

	public void Mute(bool b) {
		if (!b) {
			soundLabel.text = "SOUND OFF";
		} else {
			soundLabel.text = "SOUND ON";
		}
	}


/////////////////////////////////////
	/// 
	/// Hurricane Game Engine Prototype Level Loading
	/// 
	public void SinglePlayerHurricaneLevel() {
		panelGameSelect.gameObject.SetActive (false);
		panelLoading.gameObject.SetActive (true);
		StartCoroutine(LoadScreen("City[Prototype]"));
	}
	public void MultiplayerlayerHurricaneLevel() {
		panelGameSelect.gameObject.SetActive (false);
		panelLoading.gameObject.SetActive (true);
		StartCoroutine(LoadScreen("City[Prototype]"));
	}
////////////////////////////////////


	/// Loading screen stuff
	IEnumerator LoadScreen(string s) {

		AsyncOperation aSync = Application.LoadLevelAsync (s);

		while (!aSync.isDone) {
			loadingSlider.value = loadingProgress;	
			percentText.text = loadingProgress + "%";
			//Debug.Log (loadingProgress);
			loadingProgress = (int)(aSync.progress * 100);


			yield return null;
		}

	}

}
