using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RighrOrLeftButton : MonoBehaviour
{
    public bool isPresed;
    public HeroCube player;
    public string direction;
    void Start()
    {
        isPresed = false;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<HeroCube>();
    }

    
    void Update()
    {
        if (isPresed && direction == "Right") { player.isPressRightButton = true; }
  else  if (isPresed && direction == "Left") { player.isPressLeftButton = true; }
    }

    public void ButtonOn()
    {
        isPresed = true;
    }
    public void ButtonOff()
    {
        isPresed = false;
    }
}
