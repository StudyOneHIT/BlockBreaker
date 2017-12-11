using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuShowStar : MonoBehaviour {

	public Sprite NAimage;
	public GameObject[] Star;
	public int Level;

	// Use this for initialization
	void Start () {
		int score = PlayerPrefs.GetInt("Score_L" + Level, 0);
		if (score >= 1) {
			Star[0].SetActive(true);
		}
		if (score >= 2) {
			Star[1].SetActive(true);
		}
		if (score >= 3) {
			Star[2].SetActive(true);
		}
		if (Level > 1) {
			if (PlayerPrefs.GetInt("Score_L" + (Level - 1).ToString(), 0) == 0) {
				GetComponent<Image>().sprite = NAimage;
				GetComponent<Button>().enabled = false;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
