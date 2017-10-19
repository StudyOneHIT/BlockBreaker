using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallActions : MonoBehaviour {

    public GameObject board;
    public float distBallBoard;
    public float ShootSpeed;

    public static Rigidbody2D rb;

	private AudioSource audio;


    // Use this for initialization
    void Start () {
		audio = GetComponent<AudioSource> ();
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		if (!GameController.instance.started)
        {
            transform.position = board.transform.position + new Vector3(0, distBallBoard, 0);
            if (Input.GetKey("space"))
            {
                rb.velocity = new Vector3(0, ShootSpeed, 0);
                GameController.instance.started = true;
            }
        }
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if (GameController.instance.started && collision.gameObject.tag == "Board") 
		{
			audio.Play ();
		}
	}

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Bottom")
        {
            GameController.instance.started = false;
        }
    }

}
