using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraForLevels : MonoBehaviour
{
    public GameObject player;

    public Vector3 distanceToPlayer;

    
    public bool levelCompleted;
    void Start()
    {
         player = GameObject.FindGameObjectWithTag("Player");
         distanceToPlayer = transform.position - player.transform.position;
         GameObject.FindGameObjectWithTag("PhaseController").GetComponent<Level_Controller>().endLastPhase += EndLastPhase;
         levelCompleted = false;
    }

    
    void Update()
    {
        if (levelCompleted == false)
        {
           transform.position = player.transform.position + distanceToPlayer;
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position,GameObject.FindGameObjectWithTag("FinalCameraPosition").transform.position,2f);
            transform.rotation = Quaternion.Slerp(transform.rotation,GameObject.FindGameObjectWithTag("FinalCameraPosition").transform.rotation,1f);
        }
        
    }
    void EndLastPhase()
    {
        levelCompleted = true;
    }
}
