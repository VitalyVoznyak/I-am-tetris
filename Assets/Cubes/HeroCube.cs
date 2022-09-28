using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroCube : Cube // герой как и любой куб является наследником класса Cube
{
    public Rigidbody rb; 

    public Transform startpos;// стартовая позиция
    public GameObject redCube;// префаб черный куб
   
    public int playerSpeed;//скорость передвижения

     public GameObject closestCube; 
     
     GameObject closestFinishCube; //ближайший финиш-кубик

    void Start()
    {
        boxCollider = gameObject.GetComponent<BoxCollider>();
        colorType = "Green";
        rb = gameObject.GetComponent<Rigidbody>();  

        //подписываемся на событие, при котором заканчивается фаза 
        GameObject.FindGameObjectWithTag("PhaseController").GetComponent<Level_1_Script>().endPhase += OnEndPhase;
        
    }

    
    void Update()
    {
        Move();
        Jump();
    }

    private void  Move() // передвижение
    {
        if(Input.GetKey(KeyCode.A))
        {
            rb.velocity = Vector3.left * playerSpeed + Vector3.up * rb.velocity.y; 
        }

        if(Input.GetKey(KeyCode.D))
        {
            rb.velocity = Vector3.right * playerSpeed + Vector3.up * rb.velocity.y;
        }
    }

    public float jumpForce;// сила прыжка
    private void Jump()//прыжок
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpForce);
        }
    }

    void OnEndPhase()//метод, вызывается при окончании фазы
    {
        Instantiate(redCube,transform.position, Quaternion.identity);//создаем на своем месте красный куб(до перемещения)

        transform.position = startpos.position; //перемещаемся в начальную позицию
    }
}

