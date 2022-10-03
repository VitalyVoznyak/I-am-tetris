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

    void Start()
    {
        IsConnected = false;
        rb = gameObject.GetComponent<Rigidbody>();

        GameObject.FindGameObjectWithTag("PhaseController").GetComponent<Level_1_Script>().endPhase += OnEndPhase;//подписка на событие
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
    

     
    private void OnTriggerEnter(Collider other)// процесс присоединения данного куба к игроку
    {   
        // процесс присоединения данного куба к игроку
        if (other.tag == "ConnectZone" && IsConnected == false)
        {

            IsConnected = true;
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

             //удаление триггера присоеденения
             //Destroy(other.gameObject);
             
             transform.localScale = new Vector3(0.98f,0.98f,0.98f);//уменьшаемся в размере, чтобы лучше помещаться в щели 

             Destroy(rb);
        }
    }

    
    

     
    public float maxGroundDistance;
    public bool IsGrounded() //проверяет , стоит ли именно зеленый куб на чем нибудь твердом (с помощью двух лучей сразу)
    {
        RaycastHit hit;

        Ray ray = new Ray (new Vector3(transform.position.x - 0.5f,transform.position.y,transform.position.z), Vector3.down);
         
        Physics.Raycast(ray,out hit,Mathf.Infinity,1,QueryTriggerInteraction.Ignore);   

        if ((hit.collider.gameObject.tag == "WhiteCube" || hit.collider.gameObject.tag == "RedCube") && hit.distance < maxGroundDistance)
        {
            return true;
        }
        else
        {
           

            ray = new Ray (new Vector3(transform.position.x + 0.5f,transform.position.y,transform.position.z), Vector3.down);
         
            Physics.Raycast(ray,out hit,Mathf.Infinity,1,QueryTriggerInteraction.Ignore); 

             if ((hit.collider.gameObject.tag == "WhiteCube" || hit.collider.gameObject.tag == "RedCube") && hit.distance < maxGroundDistance)
        {
            return true;
        }
        else return false;
        }
    }
}
