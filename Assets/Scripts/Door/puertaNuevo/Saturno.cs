using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saturno : MonoBehaviour
{
    

    [Range(0, 100)]
    [SerializeField]
    float _velocidadGiro = 50;



    void Update()
    {
        this.transform.Rotate(Vector3.up*_velocidadGiro*Time.deltaTime, Space.Self);  
    }
}

