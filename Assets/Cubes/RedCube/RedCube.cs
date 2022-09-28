using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime;

public class RedCube : Cube
{
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3
        (
            (float)(decimal.Round((decimal)transform.position.x)),//
            (float)(decimal.Round((decimal)transform.position.y)),// округляем нашу позицию, чтобы небыло ненужных смещений
            (float)(decimal.Round((decimal)transform.position.z)) // (преобразуя float в decimal, округлив, и преобразовав обратно)
        );
    }
}
