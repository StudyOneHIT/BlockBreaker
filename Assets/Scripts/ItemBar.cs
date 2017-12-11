using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemBar : MonoBehaviour {

	public GameObject TextSticky;
	public GameObject TextLargerBoard;
	public GameObject TextSplit;
	public GameObject TextSlowDown;

	public void UseItem(string item) {
		int left = PlayerPrefs.GetInt(item, 0);
		if (left > 0) {
			PlayerPrefs.SetInt(item, left - 1);
			UpdateTexts();
			GameController.instance.ic.UseItem(item);
		}	
	}

	void UpdateTexts() {
		TextSticky.GetComponent<Text>().text = PlayerPrefs.GetInt("Sticky", 0).ToString();
		TextLargerBoard.GetComponent<Text>().text = PlayerPrefs.GetInt("LargerBoard", 0).ToString();
		TextSplit.GetComponent<Text>().text = PlayerPrefs.GetInt("Split", 0).ToString();
		TextSlowDown.GetComponent<Text>().text = PlayerPrefs.GetInt("SlowDown", 0).ToString();
	}

	// Use this for initialization
	void Start () {
		UpdateTexts();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
