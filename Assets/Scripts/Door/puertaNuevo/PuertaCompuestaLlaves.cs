using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuertaCompuestaLlaves : MonoBehaviour, IPuerta
{
    //Llaves necesarias para desbloquear la puerta
    [SerializeField]
    TipoLlaveEnum[] _tiposLlaves;


    //Tabla para controlar si las llaves se han recogido
    // Par "TipoLlaveEnum" y "Bool" true para recogida
    Hashtable _tablaLlaves = new Hashtable();

    bool _bloqueada = false;


    [SerializeField]
    [Range(0.1f, 5f)]
    float tiempoEsperaCerrarPuerta = 1.0f;

    float _tiempoActualEsperarCerrarPuerta = 0;

    bool _checkCerrarPuerta = false;

    IMovimientoPuerta _iMovimientoPuerta;


    [SerializeField]
    bool _empiezaAbierta;
    private void Awake() {
        _iMovimientoPuerta =  GetComponent<IMovimientoPuerta>();
    }

    // Start is called before the first frame update
    void Start()
    {
       if(_tiposLlaves.Length>0){
            for(int i= 0; i < _tiposLlaves.Length; i++){
                _tablaLlaves.Add(_tiposLlaves[i],false);
            }
            _bloqueada = true;
        }   else{
            _bloqueada = false;
        }

        if(_empiezaAbierta){
            Abrir();
        }
    }

    // Update is called once per frame
    void Update()
    {
        ComprobarCerrarPuerta();
    }


    public void Abrir(TipoLlaveEnum tipoLlave){
        if(_tablaLlaves.ContainsKey(tipoLlave)){
            _tablaLlaves[tipoLlave] = true;

        }
        ComprobarLlaves();
        if(!_bloqueada){
             _iMovimientoPuerta.MovimientoAbrir();
        }
    }


    public void Abrir(){
        ComprobarLlaves();
        if(!_bloqueada){
            _iMovimientoPuerta.MovimientoAbrir();
        }
    }

    public void Cerrar(){
        if(!_checkCerrarPuerta){
            _tiempoActualEsperarCerrarPuerta = tiempoEsperaCerrarPuerta;
            _checkCerrarPuerta = true;
        }
    }

    public bool isAbierta(){
        return _iMovimientoPuerta.IsAbierta();
    }

    private void ComprobarLlaves(){
        bool todasRecogidas = true;
        for(int i= 0; i < _tiposLlaves.Length; i++){
            if(!(bool) _tablaLlaves[_tiposLlaves[i]]){
                todasRecogidas = false;
            }
        }
        if(todasRecogidas){
            _bloqueada = false;  
        }  else{
            _bloqueada = true;
        }
    }

    private void ComprobarCerrarPuerta(){
        if(_checkCerrarPuerta){
            _tiempoActualEsperarCerrarPuerta -= Time.deltaTime;
            if(_tiempoActualEsperarCerrarPuerta<=0){
                _checkCerrarPuerta = false;
                _iMovimientoPuerta.MovimientoCerrar();
            }
        }
    }

    public bool isBloqueada(){
        return _bloqueada;
    }


    public  void SetIListenerAbrir(IListenerAbrir listener){
        _iMovimientoPuerta.SetIListenerAbrir(listener);
    }

    public bool isAbriendo(){
        return _iMovimientoPuerta.isAbriendo();
    }
    public bool isCerrando(){
        return _iMovimientoPuerta.isCerrando();
    }
    
}
