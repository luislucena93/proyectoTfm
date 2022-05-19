using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoPuertaDeslizanteSimple : MonoBehaviour, IMovimientoPuerta
{
    [SerializeField]
    Vector3 _vectorTranslacionCerrada;

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

    private void Awake() {
        if(empiezaCerrada){
            _isAbierta = false;
            _progresoLerp = 1;
            _posicionAbierta = this.transform.localPosition - _vectorTranslacionCerrada;
            _posicionCerrada = this.transform.localPosition;
        }   else{
            _isAbierta = true;
            _progresoLerp = 0;
            _posicionAbierta = this.transform.localPosition;
            _posicionCerrada = this.transform.localPosition + _vectorTranslacionCerrada;
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
    }

    public void MovimientoCerrar(){
        _abriendo = false;
        _cerrando = true;
        _isAbierta = false;
    }

    private void AbreCierraPuerta(){
        if(_abriendo){
             _progresoLerp -= _velicidadApertura*Time.deltaTime;
            this.transform.localPosition = Vector3.Lerp(_posicionAbierta, _posicionCerrada, _progresoLerp);
            if(_progresoLerp<0){
                _abriendo = false;
                _isAbierta = true;
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

    public bool IsAbierta(){
        return _isAbierta;
    }
}
