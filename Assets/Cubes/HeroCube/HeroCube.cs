using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroCube : Cube // герой как и любой куб является наследником класса Cube
{
    public Rigidbody rb; 

    public Transform startPos;              // стартовая позиция

    public GameObject redCube;              // префаб черный куб
   
    public int playerSpeed;                 //скорость передвижения
     
     GameObject closestFinishCube;          //ближайший финиш-кубик

     [SerializeField] bool isGrounded;      //стоит ли на земле

    public bool isPressLeftButton = false;
    public bool isPressRightButton = false;

    void Start()
    {
        boxCollider = gameObject.GetComponent<BoxCollider>();
        colorType = "Green";
        rb = gameObject.GetComponent<Rigidbody>();
        //transform.localScale = new Vector3(95f, 95f, 95f);

        //подписываемся на событие, при котором заканчивается фаза 
        GameObject.FindGameObjectWithTag("PhaseController").GetComponent<Level_Controller>().endPhase += OnEndPhase;
        GameObject.FindGameObjectWithTag("PhaseController").GetComponent<Level_Controller>().restartPhase += OnRestartPhase;//подписка на событие
    }

    
    void Update()
    {
        Move();
    }

    void OnEndPhase()                                                 //метод, вызывается при окончании фазы
    {
        Instantiate(redCube,transform.position, Quaternion.identity); //создаем на своем месте красный куб(до перемещения)

        transform.position = startPos.position;                       //перемещаемся в начальную позицию
    }
    private void  Move()            // передвижение
    {
        if (isPressLeftButton == false && isPressLeftButton == false)//
        {                                                                        // чтобы не скользил лишний раз когда должен стоятть на земле
            rb.velocity = new Vector3(0f, rb.velocity.y, 0f);                    //
        }

        if (isPressLeftButton)
        {
            rb.velocity = Vector3.left * playerSpeed + Vector3.up * rb.velocity.y; 
            isPressLeftButton = false;
        }

        if(isPressRightButton)
        {
            rb.velocity = Vector3.right * playerSpeed + Vector3.up * rb.velocity.y;
            isPressRightButton = false;
        }

        transform.position = new Vector3(transform.position.x, transform.position.y,0f);//фиксируем по позиции Z
        if (transform.position.y < -7){OnRestartPhase();}// при выходе за пределы карты начинаем фазу заново
    }

  

    public float jumpForce;         // сила прыжка
    public void Jump()             //прыжок
    {
       if  (FigureIsGrounded() == true)
       {
           rb.AddForce(Vector3.up * jumpForce);
       }
    }



    public bool FigureIsGrounded()             //узнаем, стоим ли мы на земле. это нужно например для прыжка
    {
        bool isGrounded = false; 
        foreach(CubePart cubePart in transform.GetComponentsInChildren<CubePart>())//
        {                                                                          //проверяем, стоит ли какой нибудь из присоедененных желтых кубов на земле
            if(cubePart.IsGrounded() == true)                                 //
            {                                                                      //
                isGrounded = true;                                                 //
            }                                                                      //
        }                                

        if (GreenCubeIsGrounded() == true) 
        {                                                                          // проверяем, приземлен ли на земле сам зеленый куб
            isGrounded = true;                                                     //
        }                                                                          //
        return isGrounded;
    }

    void OnRestartPhase()
    {
        transform.position = startPos.position;
    }
     
    public float maxGroundDistance;
    public bool GreenCubeIsGrounded() //проверяет , стоит ли именно зеленый куб на чем нибудь твердом (с помощью трех лучей сразу)
    {
        RaycastHit hit;

        Ray ray = new Ray (new Vector3(transform.position.x - 0.45f,transform.position.y,transform.position.z), Vector3.down);
         
        Physics.Raycast(ray,out hit,Mathf.Infinity,1,QueryTriggerInteraction.Ignore);   

        if (hit.collider != null && (hit.collider.gameObject.tag == "WhiteCube" || hit.collider.gameObject.tag == "RedCube") && hit.distance < maxGroundDistance)
        {
            return true;
        }
        else
        {
            ray = new Ray (new Vector3(transform.position.x + 0.45f,transform.position.y,transform.position.z), Vector3.down);
         
            Physics.Raycast(ray,out hit,Mathf.Infinity,1,QueryTriggerInteraction.Ignore);

            if (hit.collider != null && (hit.collider.gameObject.tag == "WhiteCube" || hit.collider.gameObject.tag == "RedCube") && hit.distance < maxGroundDistance)
            {
                return true;
            }
            else
            {
                ray = new Ray(new Vector3(transform.position.x + 0.4f, transform.position.y, transform.position.z), Vector3.down);

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

