using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickThreeTimes : MonoBehaviour {

	public Sprite break1;
	public Sprite break2;

	private int life = 3;
	private SpriteRenderer sr;

	// Use this for initialization
	void Start () {
		sr = GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter2D (Collision2D c) {
		life--;
		if (life == 2) {
			sr.sprite = break1;
		} else if (life == 1) {
			sr.sprite = break2;
		} else {
			GameController.instance.ic.NewItem(transform);
			GameController.instance.DecBrick(gameObject);
		}
	}
}
