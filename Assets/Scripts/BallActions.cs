using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallActions : MonoBehaviour {

    private GameObject board;
    private float distBoard;

    public float ShootSpeed;
	public float MinTan;
    public bool sticked = false;
    public float offsetX; // Offset from board when sticking.
	public Rigidbody2D rb;

    // Use this for initialization
    void Start () {
		board = GameController.instance.board;
        rb = GetComponent<Rigidbody2D>();
        distBoard = (GetComponent<Collider2D>().bounds.size.y + 
            GameController.instance.board.GetComponent<Collider2D>().bounds.size.y) / 2.0F;
	}

	// Update is called once per frame
	void Update () {
		// Follow the board when beginning or using a sticky board.
		if (!GameController.instance.started)
        {
            transform.position = board.transform.position + new Vector3(0, distBoard, 0);
            if (Input.GetKey("space")) // TODO: change to touch-screen operation.
            {
                GameController.instance.started = true;
				rb.velocity = new Vector3(0, ShootSpeed, 0);
            }
        }
        if (sticked)
        {
			Vector3 bsize = board.GetComponent<Collider2D> ().bounds.size;
			float maxoff = (bsize.x - bsize.y) / 2.0F;
			if (offsetX > maxoff) offsetX = maxoff;
			if (offsetX < -maxoff) offsetX = -maxoff;
			transform.position = board.transform.position + new Vector3 (offsetX, distBoard, 0);
            if (Input.GetKey("space")) // TODO: change to touch-screen operation.
            {
				rb.velocity = new Vector3(0, ShootSpeed, 0);
				sticked = false;
            }
        }
		Vector3 vv = rb.velocity;
		float vtan = vv.y / vv.x;
		if (vtan < MinTan && vtan > -MinTan) {
			double theta = System.Math.Atan2 (vv.y, vv.x);
			float vabs = (float)System.Math.Sqrt(vv.x * vv.x + vv.y * vv.y);
			vv.x = vabs * (float) System.Math.Cos (theta);
			vv.y = vabs * (float) System.Math.Sin (theta);
		}
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		
	}

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Bottom")
        {
			GameController.instance.DecBall ();
			Destroy (gameObject);
        }
    }

}
