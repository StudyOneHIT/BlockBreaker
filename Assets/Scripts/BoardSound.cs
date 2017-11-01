using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardSound : MonoBehaviour
{

    private AudioSource asrc;

    // Use this for initialization
    void Start()
    {
        asrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D c)
    {
        if (!GameController.instance.Mute && !c.gameObject.GetComponent<BallActions>().Sticked)
        {
            asrc.Play();
        }
    }

}
