using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BotStateMachine : StateMachine
{
    [SerializeField]
    GameObject _goRuedaIzq;
    [SerializeField]
    GameObject _goRuedaDer;
    [SerializeField]
    GameObject _goCabezal;
    [SerializeField]
    [Range(0,500)]
    float _velocidadCabezal = 1;

    Vector3 _posicionAnterior;
    

    [SerializeField]
    [Range(0,500)]
    float _factorVelocidadRuedas = 1;

    NavMeshAgent _agent;

    [SerializeField]
    [Range (0.1f,5)]
    float _tiempoEntreComprobaciones = 0.5f;

    float _tiempoActualComprobacion = 0;

    [SerializeField]
    List<GameObject> listaPosicionesDestino;

    int posicionActualDestino = 0;

    bool _llegadoDestino = false;

    int indiceDestino = 0;



    // Start is called before the first frame update
    void Start()
    {
        _posicionAnterior = transform.position;
        _agent = GetComponent<NavMeshAgent>();
        SwitchState(new BotCaminoState(this));
    }


    public void AnimarRuedas(){
        _goCabezal.transform.Rotate(Vector3.up*_velocidadCabezal*Time.deltaTime);

        float velocidadRuedas = (transform.position-_posicionAnterior).magnitude;

        _goRuedaDer.transform.Rotate(Vector3.left*_factorVelocidadRuedas*velocidadRuedas);
        _goRuedaIzq.transform.Rotate(Vector3.left*_factorVelocidadRuedas*velocidadRuedas);
        _posicionAnterior = transform.position;
    }

    public bool CheckTiempo(){
        _tiempoActualComprobacion+=Time.deltaTime;
        if(_tiempoActualComprobacion>_tiempoEntreComprobaciones){
            _tiempoActualComprobacion -= _tiempoEntreComprobaciones;
            return true;
        }
        return false;
    }


}
