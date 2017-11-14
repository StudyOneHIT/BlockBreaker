using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundButtonCtrl : MonoBehaviour {
	public GameObject DisableSound;
	public GameObject DisableBgm;

	public void SoundSwitch() {
		GlobalControl.SoundOn = !GlobalControl.SoundOn;
	}

	public void BGMSwitch() {
		GlobalControl.BgmOn = !GlobalControl.BgmOn;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (GlobalControl.SoundOn) {
			DisableSound.SetActive(false);
		} else {
			DisableSound.SetActive(true);
		}

		if (GlobalControl.BgmOn) {
			DisableBgm.SetActive(false);
		} else {
			DisableBgm.SetActive(true);
		}
	}
}
