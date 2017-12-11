using System.Collections;
using System.Collections.Generic;
// using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

	public GameObject MainMenu;
	public GameObject LevelMenu;
	public GameObject StoreMenu;
	public GameObject SettingMenu;
	public GameObject HelpMenu;

	private int scene = 0;

	public void BackToMain() {
		scene = 0;
		MainMenu.SetActive(true);
		LevelMenu.SetActive(false);
		StoreMenu.SetActive(false);
		SettingMenu.SetActive(false);
		HelpMenu.SetActive(false);
	}

	public void LoadLevelMenu() {
		scene = 1;
		LevelMenu.SetActive(true);
	}

	public void LoadSetting() {
		scene = 2;
		SettingMenu.SetActive(true);
	}

	public void LoadStore() {
		scene = 3;
		StoreMenu.SetActive(true);
	}

	public void LoadHelp() {
		scene = 4;
		HelpMenu.SetActive(true);
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
			} else {
				BackToMain();
			}
		}
	}
}
