using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardMove : MonoBehaviour {

    public float MoveSpeed;
	public bool sticky = false;
	public GameObject leftCircle;
	public GameObject box;
	public GameObject rightCircle;

    private Resolution[] res;    
	private Rigidbody2D rb;
	private float BorderPos;

	void MyScale (float scale) {
		box.transform.localScale += new Vector3(scale * 0.1F, 0, 0);
        leftCircle.transform.localPosition -= new Vector3(scale * 0.4F, 0, 0);
        rightCircle.transform.localPosition += new Vector3(scale * 0.4F, 0, 0);
		BorderPos -= scale * 0.4F;
	}

	// Use this for initialization
	void Start () {
        res = Screen.resolutions;
		BorderPos = 4.31F;
	}
	
	// Update is called once per frame
	void Update () {
        
        // Keyboard
        float h = Input.GetAxis("Horizontal");
        if (h != 0)
            //rb.MovePosition(rb.position + Vector2.right * h * MoveSpeed);
			transform.position += new Vector3(h * MoveSpeed, 0, 0);

        /*
        // Touch screen
        Touch touch = Input.GetTouch(0);
        float ix = touch.position.x;
        if (ix != 0)
        {
            if (ix < res[0].width / 2)
            {
                transform.Translate(Vector3.left * MoveSpeed);
            }
            else
            {
                transform.Translate(Vector3.right * MoveSpeed);
            }
        }
        */
        
		if (transform.position.x > BorderPos)
			transform.position = new Vector3(BorderPos, transform.position.y, transform.position.z);
        if (transform.position.x < -BorderPos)
            transform.position = new Vector3(-BorderPos, transform.position.y, transform.position.z);
    }

	void OnCollisionEnter2D(Collision2D collision)
	{
		if (sticky) {
			Vector3 offset = collision.gameObject.transform.position -
			                GameController.instance.board.transform.position;
			BallActions ba = collision.gameObject.GetComponent<BallActions> ();
			ba.offsetX = offset.x;
			ba.sticked = true;
		}
	}

}
