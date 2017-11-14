using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (GlobalControl.BgmOn) {
			GetComponent<AudioSource> ().enabled = true;
		} else {
			GetComponent<AudioSource> ().enabled = false;
		}
	}
}
