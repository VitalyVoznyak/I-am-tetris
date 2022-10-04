using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueCube : Cube
{//
    private Rigidbody playersRb;
    private GameObject player;

    void Start() 
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playersRb = player.GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "HeroCube" || other.gameObject.tag == "CubePart")
        {
            playersRb.AddForce(Vector3.up * 800f);
        } 
    }
}
