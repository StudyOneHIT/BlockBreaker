using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallActions : MonoBehaviour {

	private GameObject board;
	private float distBoard;
	private bool sticked = true;
	private float offsetX = 0.0F;  // Offset from board when sticking.
	private Rigidbody2D _rb;

	public bool Sticked {
		get { return sticked; }
		set { sticked = value; }
	}

	public float OffsetX {
		get { return offsetX; }
		set { offsetX = value; }
	}
	
	public Rigidbody2D rb {
		get { return _rb; }
		set { _rb = value; }
	}

	public float ShootSpeed;
	public float MinTan;
	
	public void MySetVelocityScale(float scale) {
		rb.velocity = new Vector2(rb.velocity.x * scale, rb.velocity.y * scale);
	}

	public void MyShoot() {
		rb.velocity = new Vector3(0, ShootSpeed, 0);
		sticked = false;
	}

	// Use this for initialization
	void Start() {
		board = GameController.instance.board;
		rb = GetComponent<Rigidbody2D>();
		distBoard = (GetComponent<Collider2D>().bounds.size.y +
			GameController.instance.board.GetComponent<Collider2D>().bounds.size.y) / 2.0F;
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
		if (vtan < MinTan && vtan > -MinTan) {
			double theta = System.Math.Atan2(vv.y, vv.x);
			float vabs = (float)System.Math.Sqrt(vv.x * vv.x + vv.y * vv.y);
			vv.x = vabs * (float)System.Math.Cos(theta);
			vv.y = vabs * (float)System.Math.Sin(theta);
		}
	}

	void OnCollisionEnter2D(Collision2D collision) {
		if (collision.gameObject.tag == "Ball") {
			Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>(), true);
		}
	}

	void OnCollisionExit2D(Collision2D collision) {
		if (collision.gameObject.name == "Bottom") {
			GameController.instance.DecBall(gameObject);
		}
	}

}
