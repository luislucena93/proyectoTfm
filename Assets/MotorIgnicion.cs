using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotorIgnicion : MonoBehaviour, IMaquina
{
    [SerializeField]
    GameObject _llama;
    [SerializeField]
    GameObject _luzLlama;

    [SerializeField]
    GameObject _goTriggerLlama;

    [SerializeField]
    bool _comienzaActivo = true;

    bool _estadoActual;

    void Start()
    {
        _estadoActual = _comienzaActivo;
        Encender(_estadoActual);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Encender(bool encender){
        _estadoActual = encender;
        _goTriggerLlama.SetActive(_estadoActual);
        _llama.SetActive(_estadoActual);
        _luzLlama.SetActive(_estadoActual);
    }

    public void AlternarEstado(){
        _estadoActual = !_estadoActual;
        Encender(_estadoActual);
    }

    public bool IsEncendida(){
        return _estadoActual;
    }
}

