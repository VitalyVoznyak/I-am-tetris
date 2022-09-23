using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishCube : Cube
{
    public bool activated;// вставлен ли игровой куб в фишишный куб
    public float distanceToActivate; //дистанция, необходимая чтобы куб считался вставленным 
    void Start()
    {
        activated = false;
    }

    public float distanceToClosestCube;
    void Update()
    {
        closestCube = FindClosestPlayerCube();
        distanceToClosestCube = Vector3.Distance(this.transform.position, closestCube.transform.position);
        if( distanceToClosestCube < distanceToActivate)
        {
            activated = true;
        }
        else
        {
            activated = false;
        }
    }

    public GameObject closestCube; 
    GameObject FindClosestPlayerCube() // ищет ближайший из кубов
    {
        GameObject[] cubesThanCanActivate;
        cubesThanCanActivate = GameObject.FindGameObjectsWithTag("CubePart");// ищет кубы, которые могут его активировать - желтые \ зеленые

        float distance = 999999;
        
        //проверяем дистанцию до каждого кубика
        foreach(GameObject CheckingCube in cubesThanCanActivate)                                        
        {
            if (Vector3.Distance(this.transform.position, CheckingCube.transform.position) < distance)
            {
                distance = Vector3.Distance(this.transform.position, CheckingCube.transform.position);
                closestCube = CheckingCube;
            }   
        }

        if (distance > Vector3.Distance(GameObject.FindGameObjectWithTag("Player").transform.position, transform.position ))
        {
            closestCube = GameObject.FindGameObjectWithTag("Player").gameObject;
        }
        return closestCube;
    }
}
