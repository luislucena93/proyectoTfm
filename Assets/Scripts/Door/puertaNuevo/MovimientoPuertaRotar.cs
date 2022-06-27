using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoPuertaRotar : MonoBehaviour, IMovimientoPuerta
{
    [SerializeField]
    Vector3 _vectorRotacionCerrada;


    bool _abriendo = false;

    bool _cerrando = false;


    bool empiezaCerrada = true;

    float _progresoLerp = 1;

    Vector3 _posicionAbierta;
    Vector3 _posicionCerrada;

    [Range(0.01f, 10f)]
    [SerializeField]
    float _velicidadApertura = 2;

    bool _isAbierta;

    List<IListenerAbrir> _listaListenersAbrir = new List<IListenerAbrir>();

    private void Awake() {
        if(empiezaCerrada){
            _isAbierta = false;
            _progresoLerp = 1;
            _posicionAbierta = this.transform.localRotation.eulerAngles - _vectorRotacionCerrada;
            _posicionCerrada = this.transform.localRotation.eulerAngles;
        }   else{
            _isAbierta = true;
            _progresoLerp = 0;
            _posicionAbierta = this.transform.localRotation.eulerAngles;
            _posicionCerrada = this.transform.localRotation.eulerAngles + _vectorRotacionCerrada;
        }
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        AbreCierraPuerta();
        
    }


    public void MovimientoAbrir(){
        _abriendo = true;
        _cerrando = false;
        _isAbierta = _progresoLerp <= 0;
        NotificaComienzaAbrir();
    }

    public void MovimientoCerrar(){
        _abriendo = false;
        _cerrando = true;
        _isAbierta = _progresoLerp > 0;
        NotificaComienzaCerrar();
    }

    private void AbreCierraPuerta(){
        if(_abriendo){
             _progresoLerp -= _velicidadApertura*Time.deltaTime;
            this.transform.localRotation = Quaternion.Euler(Vector3.Lerp(_posicionAbierta, _posicionCerrada, _progresoLerp));
            if(_progresoLerp<0){
                _abriendo = false;
                _isAbierta = true;
                NotificaFinalizaAbrir();
            }
        }


        if(_cerrando){
            _progresoLerp += _velicidadApertura*Time.deltaTime;
            this.transform.localRotation = Quaternion.Euler(Vector3.Lerp(_posicionAbierta, _posicionCerrada, _progresoLerp));
            if(_progresoLerp>1){
                _cerrando = false;
                NotificaFinalizaCerrar();
            }
        }
    }

    public bool IsAbierta(){
        return _isAbierta;
    }

    public void SetIListenerAbrir(IListenerAbrir listener){
        _listaListenersAbrir.Add(listener);
    }


    private void NotificaComienzaAbrir(){
        if(_listaListenersAbrir != null){
            int longitud = _listaListenersAbrir.Count;
            for(int i = 0; i<longitud; i++){
                _listaListenersAbrir[i].ComienzaAbrir();
            }
        }
    }
    private void NotificaComienzaCerrar(){
        if(_listaListenersAbrir != null){
            int longitud = _listaListenersAbrir.Count;
            for(int i = 0; i<longitud; i++){
                _listaListenersAbrir[i].ComienzaCerrar();
            }
        }
    }
    private void NotificaFinalizaAbrir(){
        if(_listaListenersAbrir != null){
            int longitud = _listaListenersAbrir.Count;
            for(int i = 0; i<longitud; i++){
                _listaListenersAbrir[i].FinalizaAbrir();
            }
        }
    }
    private void NotificaFinalizaCerrar(){
        if(_listaListenersAbrir != null){
            int longitud = _listaListenersAbrir.Count;
            for(int i = 0; i<longitud; i++){
                _listaListenersAbrir[i].FinalizaCerrar();
            }
        }
    }

    public bool isAbriendo(){
        return _abriendo;
    }
    public bool isCerrando(){
        return _cerrando;
    }
}
