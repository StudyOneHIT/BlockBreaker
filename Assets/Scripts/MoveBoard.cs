using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBoard : MonoBehaviour {

    public float MoveSpeed;
    public float border;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        float ix = Input.acceleration.x;
        float h = Input.GetAxis("Horizontal");
        float x = transform.position.x;

        if (h != 0)
            transform.Translate(Vector3.right * h * MoveSpeed);
        if (ix != 0)
            transform.Translate(Vector3.right * ix * MoveSpeed);
        if (transform.position.x > border)
            transform.position = new Vector3(border, transform.position.y, transform.position.z);
        if (transform.position.x < -border)
            transform.position = new Vector3(-border, transform.position.y, transform.position.z);
    }
}
