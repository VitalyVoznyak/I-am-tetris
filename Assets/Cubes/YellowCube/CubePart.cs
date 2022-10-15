using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubePart : Cube
{
    public bool IsConnected; //переменная, определяющая, присоеденен ли куб к гл герою

    private Rigidbody rb;
    public GameObject redCube;//красный кубик которым мы заменяем пройденые фазы

    GameObject closestFinishCube; //ближайший финиш-кубик

    public bool thisCubeisGrounded;//приземлен ли данный кубик

    private Vector3 startPos;//изначальная позиция

    void Start()
    {
        IsConnected = false;// изначально не присоединен

        startPos = transform.position;

        rb = gameObject.GetComponent<Rigidbody>();
        rb.isKinematic = true;

        GameObject.FindGameObjectWithTag("PhaseController").GetComponent<Level_1_Script>().endPhase += OnEndPhase;//подписка на событие 
        GameObject.FindGameObjectWithTag("PhaseController").GetComponent<Level_1_Script>().restartPhase += OnRestartPhase;//подписка на событие 
    }

    void OnEndPhase()
    {   
            if(IsConnected)
            {
                transform.parent = null;                                       //отсоединяемся от гл героя
                Instantiate(redCube,transform.position, Quaternion.identity);  //создаем вместо себя красный куб 
                this.gameObject.SetActive(false);                              //затем самовыключаемся
            }
    }
    
    void OnRestartPhase()//при рестарте фазы возвращаем на сови места
    {
        if (IsConnected)
        {
            IsConnected = false;
            transform.parent = null;
            transform.position = startPos;

            rb = gameObject.AddComponent<Rigidbody>();
            rb.mass = 6;
            rb.isKinematic = true;

            //включение собственных зон для присоеденения
            transform.Find($"CubeConnectZone").GetComponent<BoxCollider>().enabled = false;
            transform.Find($"CubeConnectZone1").GetComponent<BoxCollider>().enabled = false;
            transform.Find($"CubeConnectZone2").GetComponent<BoxCollider>().enabled = false;
            transform.Find($"CubeConnectZone(Down)").GetComponent<BoxCollider>().enabled = false;
        }
    }
     
    private void OnTriggerEnter(Collider other)// процесс присоединения данного куба к игроку
    {   
        // процесс присоединения данного куба к игроку
        if (other.tag == "ConnectZone" && IsConnected == false  && other.transform.parent.gameObject == closestYellowOrGreenCube())
        {
             IsConnected = true;
             rb.isKinematic = false;
             // ищем точку в которую переместится данный объект
             GameObject point = other.transform.Find("PointForConnect").gameObject; 
            
             // меняем положение в пространстве
             transform.position = point.transform.position;
             transform.rotation = point.transform.rotation;
             // делаем игрока родителем данного объекта
             transform.parent = GameObject.FindGameObjectWithTag("Player").transform;

             //включение собственных зон для присоеденения
             transform.Find($"CubeConnectZone").GetComponent<BoxCollider>().enabled = true;
             transform.Find($"CubeConnectZone1").GetComponent<BoxCollider>().enabled = true;
             transform.Find($"CubeConnectZone2").GetComponent<BoxCollider>().enabled = true;
             transform.Find($"CubeConnectZone(Down)").GetComponent<BoxCollider>().enabled = true;
             Destroy(rb);
        }
    }
    
    GameObject closestYellowOrGreenCube() //ищем ближайший куб к которому можно присоедениться
    {
        GameObject cubeForConnect = GameObject.FindGameObjectWithTag("Player");

        GameObject[] yellowCubes = GameObject.FindGameObjectsWithTag("CubePart");

        foreach (GameObject yellowCube in yellowCubes)
        {
            if  (Vector3.Distance(yellowCube.transform.position, transform.position)
               < Vector3.Distance(cubeForConnect.transform.position, transform.position)
              && yellowCube != this.gameObject)
            {
                cubeForConnect = yellowCube;
            };
        }
        return cubeForConnect;
    }
  
    public float maxGroundDistance;
    public bool IsGrounded() //проверяет , стоит ли именно зеленый куб на чем нибудь твердом (с помощью двух лучей сразу)
    {
        RaycastHit hit;

        Ray ray = new Ray (new Vector3(transform.position.x - 0.4f,transform.position.y,transform.position.z), Vector3.down);
         
        Physics.Raycast(ray,out hit,Mathf.Infinity,1,QueryTriggerInteraction.Ignore);   

        if (hit.collider != null && (hit.collider.gameObject.tag == "WhiteCube" || hit.collider.gameObject.tag == "RedCube") && hit.distance < maxGroundDistance)
        {
            return true;
        }
        else
        {
            ray = new Ray (new Vector3(transform.position.x + 0.4f,transform.position.y,transform.position.z), Vector3.down);
         
            Physics.Raycast(ray,out hit,Mathf.Infinity,1,QueryTriggerInteraction.Ignore);

            if (hit.collider != null && (hit.collider.gameObject.tag == "WhiteCube" || hit.collider.gameObject.tag == "RedCube") && hit.distance < maxGroundDistance)
            {
                return true;
            }
            else
            {
                ray = new Ray(new Vector3(transform.position.x, transform.position.y, transform.position.z), Vector3.down);

                Physics.Raycast(ray, out hit, Mathf.Infinity, 1, QueryTriggerInteraction.Ignore);

                if (hit.collider != null && (hit.collider.gameObject.tag == "WhiteCube" || hit.collider.gameObject.tag == "RedCube") && hit.distance < maxGroundDistance)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
