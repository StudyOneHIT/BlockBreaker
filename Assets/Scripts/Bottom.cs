using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bottom : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter2D(Collision2D collision) {
		if (collision.gameObject.tag == "Ball") {
			GameController.instance.DecBall(collision.gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D collider) {
		Destroy(collider.gameObject);
	}
}
