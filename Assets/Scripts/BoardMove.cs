using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardMove : MonoBehaviour {

	public float MoveSpeed;
	public bool sticky = false;
	public GameObject leftCircle;
	public GameObject box;
	public GameObject rightCircle;

	[HideInInspector] public bool opposite = false;

	private Resolution[] res;
	private Rigidbody2D rb;
	private float BorderPos;
	private GameController gc;

	public void MyScale(float scale) {
		box.transform.localScale += new Vector3(scale * 0.1F, 0, 0);
		leftCircle.transform.localPosition -= new Vector3(scale * 0.4F, 0, 0);
		rightCircle.transform.localPosition += new Vector3(scale * 0.4F, 0, 0);
		BorderPos -= scale * 0.35F;
	}

	// Use this for initialization
	void Start() {
		res = Screen.resolutions;
		gc = GameController.instance;
		BorderPos = (gc.maxWidth - GetComponent<Collider2D>().bounds.size.x) / 2.0F;
		MoveSpeed *= 1.2F;
	}

	// Update is called once per frame
	void Update() {
		if (gc.mobile) {
			// Touch screen
			float tx = Input.touches[0].position.x;
			float ix = tx * gc.maxWidth / 1080.0F - gc.maxWidth / 2.0F;
			float ofst = ix - transform.position.x;

			if (!opposite) {
				if (ofst < -0.1) {
					transform.position -= new Vector3(MoveSpeed, 0, 0);
				} else if (ofst > 0.1) {
					transform.position += new Vector3(MoveSpeed, 0, 0);
				}
			} else {
				if (ofst < -0.1) {
					transform.position += new Vector3(MoveSpeed, 0, 0);
				} else if (ofst > 0.1) {
					transform.position -= new Vector3(MoveSpeed, 0, 0);
				}
			}
		} else {
			// Keyboard
			float h = Input.GetAxis("Horizontal");
			if (h != 0)
				//rb.MovePosition(rb.position + Vector2.right * h * MoveSpeed);
				transform.position += new Vector3(h * MoveSpeed, 0, 0);
		}
		
		if (transform.position.x > BorderPos)
			transform.position = new Vector3(BorderPos, transform.position.y, transform.position.z);
		if (transform.position.x < -BorderPos)
			transform.position = new Vector3(-BorderPos, transform.position.y, transform.position.z);
	}

	void OnCollisionEnter2D(Collision2D collision) {
		if (sticky) {
			Vector3 offset = collision.gameObject.transform.position -
							gc.board.transform.position;
			BallActions ba = collision.gameObject.GetComponent<BallActions>();
			ba.offsetX = offset.x;
			ba.sticked = true;
		}
	}

	void OnTriggerEnter2D(Collider2D collider) {
		Destroy(collider.gameObject);
		gc.ic.UseItem(collider.tag);
	}

}
