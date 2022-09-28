using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubePart : Cube
{
    public bool IsConnected; //переменная, определяющая, присоеденен ли куб к гл герою

    private Rigidbody rb;
    public GameObject redCube;//красный кубик которым мы заменяем пройденые фазы

    GameObject closestFinishCube; //ближайший финиш-кубик

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
             transform.Find($"HeroCubeConnectZone").GetComponent<BoxCollider>().enabled = true;
             transform.Find($"HeroCubeConnectZone1").GetComponent<BoxCollider>().enabled = true;
             transform.Find($"HeroCubeConnectZone2").GetComponent<BoxCollider>().enabled = true;
             transform.Find($"HeroCubeConnectZone3").GetComponent<BoxCollider>().enabled = true;

             //удаление триггера присоеденения
             //Destroy(other.gameObject);
             
             transform.localScale = new Vector3(0.98f,0.98f,0.98f);//уменьшаемся в размере, чтобы лучше помещаться в щели 

             Destroy(rb);
        }
    }
}
