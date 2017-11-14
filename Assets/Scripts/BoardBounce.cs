using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardBounce : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionExit2D(Collision2D collision)
    {
		BallActions ba = collision.gameObject.GetComponent<BallActions> ();
		if (!ba.sticked && collision.gameObject.tag == "Ball")
        {
			Vector2 v = ba.rb.velocity;
            float vv = (float)System.Math.Sqrt(v.x * v.x + v.y * v.y);
            Vector3 off = collision.gameObject.transform.position - transform.position;
            // print(off.x);
            ba.rb.velocity = new Vector2(vv * (float)System.Math.Sin(off.x), vv * (float)System.Math.Cos(off.x));
        }
    }
}
