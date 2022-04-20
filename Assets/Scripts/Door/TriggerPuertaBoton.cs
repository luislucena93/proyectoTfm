using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TriggerPuertaBoton : MonoBehaviour
{
    [SerializeField]
    GameObject goPuerta;

    OpenDoor _puerta;

    [SerializeField]
    [Range(0.1f, 5f)]
    float tiempoEsperaCerrarPuerta = 1.0f;

    float _tiempoActualEsperarCerrarPuerta = 0;

    bool _checkCerrarPuerta = false;

    [SerializeField]
    bool puertaCerrada = true;

    void Awake()
    {
        _puerta = goPuerta.GetComponent<OpenDoor>();
    }


    private void Update()
    {
        if (_checkCerrarPuerta)
        {
            _tiempoActualEsperarCerrarPuerta -= Time.deltaTime;
            if (_tiempoActualEsperarCerrarPuerta <= 0)
            {
                _checkCerrarPuerta = false;
                _puerta.Cerrar();
            }
        }
    }

    private void CerrarConRetraso()
    {
        _tiempoActualEsperarCerrarPuerta = tiempoEsperaCerrarPuerta;
        _checkCerrarPuerta = true;
    }

    public void accionarPuerta()
    {
        if (puertaCerrada)
        {
            _puerta.Abrir();
            puertaCerrada = false;
        }
        else
        {
            _puerta.Cerrar();
            puertaCerrada = true;
        }
    }
}
