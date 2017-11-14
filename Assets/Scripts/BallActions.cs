using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallActions : MonoBehaviour {
	private GameController gc;

	private GameObject board;
	private float distBoard;

	[HideInInspector] public bool sticked = true;
	[HideInInspector] public float offsetX = 0.0F;  // Offset from board when sticking.
	[HideInInspector] public Rigidbody2D rb;
	[HideInInspector] public float myVelocity;

	private int combocnt = 0;
	
	public void MySetVelocityScale(float scale) {
		rb.velocity = new Vector2(rb.velocity.x * scale, rb.velocity.y * scale);
		myVelocity *= scale;
	}

	public void MyShoot() {
		rb.velocity = new Vector3(0, gc.ShootSpeed, 0);
		sticked = false;
	}

	// Use this for initialization
	void Start() {
		board = GameController.instance.board;
		rb = GetComponent<Rigidbody2D>();
		gc = GameController.instance;
		distBoard = (GetComponent<Collider2D>().bounds.size.y +
			GameController.instance.board.GetComponent<Collider2D>().bounds.size.y) / 2.0F;
		myVelocity = gc.ShootSpeed;
	}

	// Update is called once per frame
	void Update() {
		// Follow the board when beginning or using a sticky board.
		if (sticked) {
			Vector3 bsize = board.GetComponent<Collider2D>().bounds.size;
			float maxoff = (bsize.x - bsize.y) / 2.0F;
			if (offsetX > maxoff) offsetX = maxoff;
			if (offsetX < -maxoff) offsetX = -maxoff;
			transform.position = board.transform.position + new Vector3(offsetX, distBoard, 0);
		}
		Vector2 vv = rb.velocity;
		float vtan = vv.y / vv.x;
		int symx = vv.x > 0 ? 1 : -1;
		int symy = vv.y > 0 ? 1 : -1;
		double theta;
		if (vtan < gc.MinTan && vtan > -gc.MinTan) {
			theta = System.Math.Atan(gc.MinTan);
			rb.velocity = new Vector2(System.Math.Abs(myVelocity * (float)System.Math.Cos(theta)) * symx,
				System.Math.Abs(myVelocity * (float)System.Math.Sin(theta)) * symy);
		} else {
			theta = System.Math.Atan2(vv.y, vv.x);
			rb.velocity = new Vector2(myVelocity * (float)System.Math.Cos(theta),
				myVelocity * (float)System.Math.Sin(theta));
		}
	}

	void OnCollisionEnter2D(Collision2D collision) {
		if (collision.gameObject.tag == "Ball") {
			Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>(), true);
		} else if (collision.gameObject.tag == "Brick") {
			combocnt++;
			if (combocnt >= gc.MaxCombo) {
				gc.Star2 = 1;
			}
		} else if (collision.gameObject.tag == "Board") {
			combocnt = 0;
		}
	}

}
