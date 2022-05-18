using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuertaCompuestaLlaves : MonoBehaviour, IPuerta
{
    //Llaves necesarias para desbloquear la puerta
    [SerializeField]
    TipoLlaveEnum[] _tiposLlaves;

    [SerializeField]
    Vector3 _vectorTranslacionCerrada;


    //Tabla para controlar si las llaves se han recogido
    // Par "TipoLlaveEnum" y "Bool" true para recogida
    Hashtable _tablaLlaves = new Hashtable();

    bool _bloqueada = false;


    bool _abriendo = false;

    bool _cerrando = false;

    [SerializeField]
    bool empiezaCerrada = true;

    float _progresoLerp = 1;

    Vector3 _posicionAbierta;
    Vector3 _posicionCerrada;


     [Range(0.01f, 10f)]
    [SerializeField]
    float _velicidadApertura = 2;



    [SerializeField]
    [Range(0.1f, 5f)]
    float tiempoEsperaCerrarPuerta = 1.0f;

    float _tiempoActualEsperarCerrarPuerta = 0;

    bool _checkCerrarPuerta = false;

    IMovimientoPuerta _iMovimientoPuerta;

    private void Awake() {
        if(empiezaCerrada){
            _progresoLerp = 1;
            _posicionAbierta = this.transform.localPosition - _vectorTranslacionCerrada;
            _posicionCerrada = this.transform.localPosition;
        }   else{
            _progresoLerp = 0;
            _posicionAbierta = this.transform.localPosition;
            _posicionCerrada = this.transform.localPosition + _vectorTranslacionCerrada;
        }

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
    }

    // Update is called once per frame
    void Update()
    {
        ComprobarCerrarPuerta();
        //AbreCierraPuerta();
    }


    public void Abrir(TipoLlaveEnum tipoLlave){
        if(_tablaLlaves.ContainsKey(tipoLlave)){
            _tablaLlaves[tipoLlave] = true;

        }
        ComprobarLlaves();
        if(!_bloqueada){
             _iMovimientoPuerta.MovimientoAbrir();
            //_abriendo = true;
            //_cerrando = false;
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
    
    private void AbreCierraPuerta(){
        if(!_bloqueada &&_abriendo){
             _progresoLerp -= _velicidadApertura*Time.deltaTime;
            this.transform.localPosition = Vector3.Lerp(_posicionAbierta, _posicionCerrada, _progresoLerp);
            if(_progresoLerp<0){
                _abriendo = false;
            } 
            //Debug.Log("actual "+this.transform.localPosition);
        }


        if(_cerrando){
            _progresoLerp += _velicidadApertura*Time.deltaTime;
            this.transform.localPosition = Vector3.Lerp(_posicionAbierta, _posicionCerrada, _progresoLerp);
            if(_progresoLerp>1){
                _cerrando = false;
            } 
            //Debug.Log("actual "+this.transform.localPosition);
        }
    }


    public bool isBloqueada(){
        return _bloqueada;
    }
    
}
