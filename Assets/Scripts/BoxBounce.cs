using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxBounce : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionExit2D(Collision2D collision)
    {
        if (GameController.instance.started && collision.gameObject.tag == "Ball")
        {
            Vector2 v = BallActions.rb.velocity;
            float vv = (float)System.Math.Sqrt(v.x * v.x + v.y * v.y);
            Vector3 off = collision.gameObject.transform.position - transform.position;
            // print(off.x);
            BallActions.rb.velocity = new Vector2(vv * (float)System.Math.Sin(off.x), vv * (float)System.Math.Cos(off.x));
        }
    }
}
