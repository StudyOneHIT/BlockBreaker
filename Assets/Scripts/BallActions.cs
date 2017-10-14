using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallActions : MonoBehaviour {

    public GameObject board;
    public float offset;
    public float ShootSpeed;
    public float move;

    private bool started;
    private Rigidbody2D rb;

    // Use this for initialization
    void Start () {
        started = false;
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		if (!started)
        {
            transform.position = board.transform.position + new Vector3(0, offset, 0);
            if (Input.GetKey("space"))
            {
                rb.velocity = new Vector3(0, ShootSpeed, 0);
                started = true;
            }
        }
	}

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Board")
        {
            Vector3 offset = transform.position - board.transform.position;
            rb.velocity += new Vector2(offset.x * move, 0);
        }
        else if (collision.gameObject.name == "Bottom")
        {
            started = false;
        }
    }

}
