using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Store : MonoBehaviour {

	public GameObject Score;
	public GameObject FailMessage;

	public int[] Price;
	public GameObject[] TextPrice;

	public void BuyItem(string item) {
		int score = PlayerPrefs.GetInt("Score", 0);
		int i = ItemController.GetItemIndex(item);
		if (score >= Price[i]) {
			// Success Message
			PlayerPrefs.SetInt("Score", score - Price[i]);
			PlayerPrefs.SetInt(item, PlayerPrefs.GetInt(item, 0) + 1);
			UpdateScoreText();
		} else {
			FailMessage.SetActive(true);
		}
	}
	
	public void CloseFailMessage() {
		FailMessage.SetActive(false);
	}

	void UpdateScoreText() {
		Score.GetComponent<Text>().text = "Star left: " + PlayerPrefs.GetInt("Score", 0).ToString();
	}

	// Use this for initialization
	void Start () {
		UpdateScoreText();
		for (int i = 0; i < ItemController.itemNum; i++) {
			if (TextPrice[i] != null)
				TextPrice[i].GetComponent<Text>().text = Price[i].ToString();
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
