using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFirstVelocity : MonoBehaviour
{

    private Rigidbody2D rb;
    public float FirstVelocity;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0, -FirstVelocity);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
