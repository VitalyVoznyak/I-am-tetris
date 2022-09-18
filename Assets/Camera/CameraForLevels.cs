using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraForLevels : MonoBehaviour
{
    GameObject player;

    public Vector3 distanceToPlayer;
    void Start()
    {
         player = GameObject.FindGameObjectWithTag("Player");
         distanceToPlayer = transform.position - player.transform.position;
    }

    
    void Update()
    {
        transform.position = player.transform.position + distanceToPlayer;
    }
}
