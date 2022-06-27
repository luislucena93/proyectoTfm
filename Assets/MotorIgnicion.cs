using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotorIgnicion : MonoBehaviour
{
    [SerializeField]
    GameObject _llama;
    [SerializeField]
    GameObject _luzLlama;

    [SerializeField]
    GameObject _goTriggerLlama;

    [SerializeField]
    [Range(0,5)]
    float tiempoApagado = 3;

    [SerializeField]
    [Range(0,5)]
    float tiempoEncencido = 3;
    // Start is called before the first frame update

    [SerializeField]
    bool _comienzaActivo = true;


    void Start()
    {
        _goTriggerLlama.SetActive(_comienzaActivo);
        _llama.SetActive(_comienzaActivo);
        _luzLlama.SetActive(_comienzaActivo);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}

public enum ComportamientoMotorEnum {};

