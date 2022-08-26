using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementoProgresoPCSecundario : MonoBehaviour
{
    [SerializeField]
    [Range(0.1f,100)]
    float _velocidadGiro;

    private void Update() {
        this.transform.Rotate(Vector3.forward*_velocidadGiro*Time.deltaTime, Space.Self);  
    }
}
