using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Escudo : MonoBehaviour
{
    int _colorEmmissionID;
 
    Renderer _rendererEscudo;
    Material _materialEscudo;
    Color _colorOriginal;

    float _colorOriginalH, _colorOriginalS,_colorOriginalV;

    [SerializeField]
    public int _nivelEscudo = 750;

    [SerializeField]
    public int _nivelEscudoMaximo = 750;
    // Start is called before the first frame update
   
    [SerializeField]
    [Range(0.01f, 1000)]
    float _velocidadRecuperarEscudo = 1;

    [SerializeField]
    [Range(0.01f, 5)]
    float _tiempoEsperarRecuperarEscudo = 1;

    float _tiempoActualEsperarRecuperarEscudo = 1;

    bool _esperandoRecuperarEscudo;


    [SerializeField]
    GameObject _goEscudo;


    [SerializeField]
    [Range(1, 1000)]
    int _consumoEncendidoEscudo = 1;

    [SerializeField]
    HUDJugador hudJugador;


    bool _activo;

    [SerializeField]
    [Range(0.01f, 5)]
    float _tiempoDanhoActivoEscudo = 0.2f;

    float _tiempoDanhoActivoEscudoActual = 0;

    [SerializeField]
    [Range(0.01f, 50)]
    float _velocidadAnimacionEscudoDanho = 0.2f;
    [SerializeField]
    [Range(0.01f, 50)]
    float _amplitudAnimacionEscudoDanho = 0.2f;

    void Start()
    {
        _colorEmmissionID = Shader.PropertyToID("_EmissionColor");
        hudJugador.SetNivelEscudo(_nivelEscudo);
        hudJugador.SetNivelEscudoMaximo(_nivelEscudoMaximo);
        _rendererEscudo = _goEscudo.GetComponent<Renderer>();
        _materialEscudo = _rendererEscudo.material;
        _colorOriginal = _materialEscudo.GetColor(_colorEmmissionID);
        Color.RGBToHSV(_colorOriginal, out  _colorOriginalH, out  _colorOriginalS, out  _colorOriginalV);
    }

    // Update is called once per frame

    void Update()
    {
        if(_activo){
            ReiniciarTiempoEscudo();
            _nivelEscudo-=(int) (_consumoEncendidoEscudo*Time.deltaTime);
            if(_nivelEscudo>0){
                hudJugador.SetNivelEscudo(_nivelEscudo);
                _activo = true;
            } else{
                _goEscudo.SetActive(false);
                _nivelEscudo = 0;
                hudJugador.SetNivelEscudo(_nivelEscudo);
                _activo = false;
            }
            LogicaAnimacionEscudo();
        }
                    

        if(!_esperandoRecuperarEscudo){
            if(_nivelEscudo<_nivelEscudoMaximo){
                //Debug.Log("Update esperando = true");
                _nivelEscudo += (int) (_velocidadRecuperarEscudo*Time.deltaTime);
                //Debug.Log("Update valor incremento = "+(int) (_velocidadRecuperarEscudo*Time.deltaTime));
                if(_nivelEscudo>_nivelEscudoMaximo){
                    _nivelEscudo=_nivelEscudoMaximo;
                }
                hudJugador.SetNivelEscudo(_nivelEscudo);
            }
        }   else{
            
            //Debug.Log("Update esperando = false");
            _tiempoActualEsperarRecuperarEscudo -= Time.deltaTime;
            if(_tiempoActualEsperarRecuperarEscudo <= 0){
                //Debug.Log("Update esperando < 0");
                _esperandoRecuperarEscudo = false;
            }
        }
    }

    public bool IsActivo(){
        return _activo;
    }

    public int ConsumirEscudo(int puntosConsumir){

        int retorno = 0;
        _nivelEscudo -= puntosConsumir;
        if(_nivelEscudo < 0){
            retorno = _nivelEscudo;
            _nivelEscudo = 0;
        }   
        hudJugador.SetNivelEscudo(_nivelEscudo);
        _esperandoRecuperarEscudo = true;
        _tiempoActualEsperarRecuperarEscudo = _tiempoEsperarRecuperarEscudo;

        _tiempoDanhoActivoEscudoActual = _tiempoDanhoActivoEscudo;

        return retorno;
    }


    public bool ActivarEscudo(bool activar){
        if(activar){
            if(_nivelEscudo>0){
                ReiniciarTiempoEscudo();
                _goEscudo.SetActive(true);
                hudJugador.SetNivelEscudo(_nivelEscudo);
                _activo = true;
                return true;
            } else{
                _goEscudo.SetActive(false);
                hudJugador.SetNivelEscudo(_nivelEscudo);
                _nivelEscudo = 0;
                _activo = false;
                return false;
            }

        }   
        _activo = false;
        _goEscudo.SetActive(false);
        //Debug.Log("Activar Escudo Desactivar");
        return false;
    }


    private void ReiniciarTiempoEscudo(){
            _esperandoRecuperarEscudo = true;
            _tiempoActualEsperarRecuperarEscudo = _tiempoEsperarRecuperarEscudo;
    }

    private void LogicaAnimacionEscudo(){
       if(_tiempoDanhoActivoEscudoActual > 0){
            _tiempoDanhoActivoEscudoActual-=Time.deltaTime;
            if(_activo){
                if(_tiempoDanhoActivoEscudoActual > 0){
                    Color colorNuevo = Color.HSVToRGB(_colorOriginalH, _colorOriginalS, _colorOriginalV*(1-_amplitudAnimacionEscudoDanho*Mathf.Sin(Time.time*_velocidadAnimacionEscudoDanho)));
                    _materialEscudo.SetColor(_colorEmmissionID,colorNuevo);

                }   else{
                    _materialEscudo.SetColor(_colorEmmissionID,_colorOriginal);
                }
            }
        }
        
    }

}
