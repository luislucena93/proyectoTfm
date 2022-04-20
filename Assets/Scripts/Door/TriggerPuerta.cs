using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPuerta : MonoBehaviour
{
    [SerializeField]
    GameObject goPuerta;

    OpenDoor _puerta;

    [SerializeField]
    [Range(0.1f, 5f)]
    float tiempoEsperaCerrarPuerta = 1.0f;

    float _tiempoActualEsperarCerrarPuerta = 0;

    bool _checkCerrarPuerta = false;

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

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {
            _puerta.Abrir();
            Debug.Log("Entro en la zona");
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            CerrarConRetraso();
        }
    }

    private void CerrarConRetraso()
    {
        _tiempoActualEsperarCerrarPuerta = tiempoEsperaCerrarPuerta;
        _checkCerrarPuerta = true;
    }
}
