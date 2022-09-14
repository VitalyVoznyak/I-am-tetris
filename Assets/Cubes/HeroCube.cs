using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroCube : Cube // герой как и любой куб является наследником класса Cube
{
    public Rigidbody rb;
    [SerializeField] private GameObject self; // сам игрок

    public int playerSpeed;
    void Start()
    {
        boxCollider = gameObject.GetComponent<BoxCollider>();
        colorType = "Green";
        rb = gameObject.GetComponent<Rigidbody>();  
    }

    
    void Update()
    {
        Move();
        Jump();
    }


    public float zPos;// постоянная позиция игрока по оси Z (как правило, статична)
    private void  Move() // передвижение
    {
        transform.position = new Vector3(transform.position.x,transform.position.y,zPos);// теперь герой статичен по оси z
        
        if(Input.GetKey(KeyCode.D))
        {
            rb.velocity = new Vector3 
            (self.transform.forward.x * playerSpeed  ,
               rb.velocity.y,
                self.transform.forward.z * playerSpeed );// аккуратно прибавляем скорость движения к уже имеющийся скорости

            self.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.Euler(transform.rotation.x,90f,transform.rotation.z), 0.05f );// разворот в правую сторону
        }

        if(Input.GetKey(KeyCode.A))
        {
            rb.velocity = new Vector3 
            (self.transform.forward.x * playerSpeed  ,
               rb.velocity.y,
                self.transform.forward.z * playerSpeed );// аккуратно прибавляем скорость движения к уже имеющийся строчке

            self.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.Euler(transform.rotation.x,270f,transform.rotation.z), 0.05f );// разворот в левую сторону
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
}
