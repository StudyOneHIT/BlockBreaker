using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

	public GameObject MainMenu;
	public GameObject LevelSl;
	public GameObject SettingObj;

	private int scene = 0;

	public void GoBack() {
		scene = 0;
		MainMenu.SetActive(true);
		LevelSl.SetActive(false);
	}

	public void StartGame() {
		scene = 1;
		MainMenu.SetActive(false);
		LevelSl.SetActive(true);
	}

	public void Settings() {
		scene = 2;
		SettingObj.SetActive(true);
	}

	public void GoBackFromSet() {
		scene = 0;
		SettingObj.SetActive(false);
	}

	public void ExitAsk() {
		Application.Quit();
	}

	public void SelectLevel(int level) {
		SceneManager.LoadScene("L" + level.ToString());
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Application.platform == RuntimePlatform.Android &&
			(Input.GetKeyDown(KeyCode.Escape))) {
			if (scene == 0) {
				ExitAsk();
			} else if (scene == 1) {
				GoBack();
			} else if (scene == 2) {
				GoBackFromSet();
			}
		}
	}
}
