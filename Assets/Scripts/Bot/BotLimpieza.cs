using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BotLimpieza :  MonoBehaviour, IDanhable
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
    Peana _peanaBase;
    

    [SerializeField]
    [Range(0,500)]
    float _factorVelocidadRuedas = 1;

    NavMeshAgent _agent;

    [SerializeField]
    [Range (0.1f,5)]
    float _tiempoEntreComprobaciones = 0.5f;

    float _tiempoActualComprobacion = 0;

    [SerializeField]
    GameObject[] listaPosicionesDestino;

    int posicionActualDestino = -1;

    bool _inicializado = false;

    int indiceDestino = -1;

    [SerializeField]
    [Range (0,5)]
    float _tiempoEsperarInicial = 1;


    [SerializeField]
    [Range (0,5)]
    float _distanciaMaximaObjetivo = 0.25f;


    [Range(1,100)]
    [SerializeField]
    int _salud = 20;


    // Start is called before the first frame update
    void Start()
    {
        _posicionAnterior = transform.position;
        _agent = GetComponent<NavMeshAgent>();

       if(_tiempoEsperarInicial > 0){
            StartCoroutine(EsperaInicial());
       }    else{
            InicializarBot();
       }
    }

    private void Update() {
        AnimarRuedas();
        if(CheckTiempo()){
            CompruebaDestino();
        }
    }


    void AnimarRuedas(){
        _goCabezal.transform.Rotate(Vector3.up*_velocidadCabezal*Time.deltaTime);

        float velocidadRuedas = (transform.position-_posicionAnterior).magnitude;

        _goRuedaDer.transform.Rotate(Vector3.left*_factorVelocidadRuedas*velocidadRuedas);
        _goRuedaIzq.transform.Rotate(Vector3.left*_factorVelocidadRuedas*velocidadRuedas);
        _posicionAnterior = transform.position;
    }

    bool CheckTiempo(){
        _tiempoActualComprobacion+=Time.deltaTime;
        if(_tiempoActualComprobacion>_tiempoEntreComprobaciones){
            _tiempoActualComprobacion -= _tiempoEntreComprobaciones;
            return true;
        }
        return false;
    }

    void CompruebaDestino(){
       /* if(_inicializado && _agent.pathStatus == NavMeshPathStatus.PathComplete){
            Debug.Log("Completado "+indiceDestino+" / "+listaPosicionesDestino.Length);
            if(listaPosicionesDestino.Length > indiceDestino+1){
                indiceDestino++;
                _agent.SetDestination(listaPosicionesDestino[indiceDestino].transform.position);
            }
        }*/
        if(_inicializado && 
            (listaPosicionesDestino[indiceDestino].transform.position-this.transform.position).magnitude<_distanciaMaximaObjetivo){
            Debug.Log("Completado "+indiceDestino+" / "+listaPosicionesDestino.Length);
            if(listaPosicionesDestino.Length > indiceDestino+1){
                indiceDestino++;
                _agent.SetDestination(listaPosicionesDestino[indiceDestino].transform.position);
            }
        }



    }

    IEnumerator EsperaInicial(){
        yield return new WaitForSeconds(_tiempoEsperarInicial);
        InicializarBot();
    }

    void InicializarBot(){
        if(listaPosicionesDestino != null && listaPosicionesDestino.Length >0){
            indiceDestino = 0;
            _agent.SetDestination(listaPosicionesDestino[indiceDestino].transform.position);
            _inicializado = true;
        }
    }


    public void Destruir(){
        Debug.Log("Destruido Bot");
        _peanaBase.BotDestruido(this.transform.position);
        this.gameObject.SetActive(false);
    }

    public void RecibirDanho(int danho){
        Debug.Log("Recibir daño bot"+danho + "restante "+_salud);
        _salud -=danho;
        if(_salud <= 0){
            Debug.Log("salud menor de cero");
            Destruir();
        }
    }
    public void SetPeanaBase(Peana peana){
        this._peanaBase = peana;
    }
}
