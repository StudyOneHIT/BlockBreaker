using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBoard : MonoBehaviour {

    public float MoveSpeed;
    //public float border;
	public GameObject leftCircle;
	public GameObject box;
	public GameObject rightCircle;
    private Resolution[] res;
    private Rigidbody2D rb;

	void ScaleLonger () {
		box.transform.localScale += new Vector3(0.1F, 0, 0);
        leftCircle.transform.localPosition -= new Vector3(0.4F, 0, 0);
        rightCircle.transform.localPosition += new Vector3(0.4F, 0, 0);
	}

	// Use this for initialization
	void Start () {
        res = Screen.resolutions;
        rb = GetComponent<Rigidbody2D>();

	}
	
	// Update is called once per frame
	void Update () {
        
        // Keyboard
        float h = Input.GetAxis("Horizontal");
        if (h != 0)
            rb.MovePosition(rb.position + Vector2.right * h * MoveSpeed);

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
        
        /*
        if (transform.position.x > border)
            transform.position = new Vector3(border, transform.position.y, transform.position.z);
        if (transform.position.x < -border)
            transform.position = new Vector3(-border, transform.position.y, transform.position.z);
            */
    }
}
