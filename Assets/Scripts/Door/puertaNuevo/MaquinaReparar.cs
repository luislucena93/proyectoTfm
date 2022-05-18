using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class MaquinaReparar : MonoBehaviour, IInteraccionable
{
    [SerializeField]
    UnityEvent _eventoFinReparar;


    [SerializeField]
    [Range(1,50)]
    private float _tiempoReparar = 10;

    private bool _reparando;

    private bool _reparado;

    private float _tiempoActualReparar = 0;


    [SerializeField]
    TMP_Text _textoReparando;

    [SerializeField]
    GameObject _goCanvasMensajeReparando;

    private static string MENSAJE_PULSAR_REPARAR = "Manten pulsado\npara reparar";
    private static string MENSAJE_REPARANDO= "Reparando";

    private static string MENSAJE_REPARADO= "Reparado";

    [SerializeField]
    GameObject _goCanvasSliderReparando;


    [SerializeField]
    Slider _slider;

    [SerializeField]
    Image _imagenSlider;

    [SerializeField]
    Gradient _gradienteColorSlider;

    bool _interaccionando;


    void Start()
    {
        CalcularProgresoReparacion();
        OcultarInterfaces();
    }

    void Update(){
        if(!_reparado){
            if(_reparando){
                _tiempoActualReparar += Time.deltaTime;
                if(_tiempoReparar <= _tiempoActualReparar){
                    MostrarReparado();
                    FinReparacion();
                } else{
                    MostrarReparando();
                    CalcularProgresoReparacion();
                }
            }
        }        
    }


    public void ComenzarInteraccion(){
        if(!_reparado){
            _reparando = true;
        }
    }

    public void PausarInteraccion(){
        _reparando = false;
    }

    public void FinalizarInteraccion(){
        _reparando = false;
    }

    private void FinReparacion(){
        _reparado = true;
        _eventoFinReparar.Invoke();
    }
    

    private void OcultarInterfaces(){
        _goCanvasMensajeReparando.SetActive(false);
        _goCanvasSliderReparando.SetActive(false);
    }

    private void MostrarReparado(){
        _goCanvasMensajeReparando.SetActive(true);
        _goCanvasSliderReparando.SetActive(true);
        _textoReparando.text = MENSAJE_REPARADO;
    }

    private void MostrarReparando(){
        _goCanvasMensajeReparando.SetActive(true);
        _goCanvasSliderReparando.SetActive(true);
        _textoReparando.text = MENSAJE_REPARANDO;
    }

    private void MostrarPulsaParaReparar(){
        _goCanvasMensajeReparando.SetActive(true);
        _goCanvasSliderReparando.SetActive(false);
        _textoReparando.text = MENSAJE_PULSAR_REPARAR;
    }

    private void CalcularProgresoReparacion(){
        float porcentaje = _tiempoActualReparar / _tiempoReparar;
        _slider.value = porcentaje;
        Color color = _gradienteColorSlider.Evaluate(porcentaje);
        _imagenSlider.color = color;
    }


    private void OnTriggerEnter(Collider other) {
        CheckEnter(other.gameObject);
    }

    private void OnTriggerStay(Collider other) {        
        CheckStay(other.gameObject);
    }

    private void OnTriggerExit(Collider other) {
        CheckExit(other.gameObject);
    }

    private void OnCollisionEnter(Collision other){
        CheckEnter(other.gameObject);
    }

    private void OnCollisionStay(Collision other) {
        CheckStay(other.gameObject);
    }

    private void OnCollisionExit(Collision other){
        CheckExit(other.gameObject);
    }




    private void CheckEnter(GameObject other){
        if(other.CompareTag(Tags.TAG_PLAYER)) {
            if(_reparado){
                MostrarReparado();
            }   else{
                if(_reparando){
                    MostrarReparando();
                }   else{
                    MostrarPulsaParaReparar();
                }
            }
        }
    }

    private void CheckStay(GameObject other){
        if(other.CompareTag(Tags.TAG_PLAYER)) {
            if(_reparado){
                MostrarReparado();
            }   else{
                if(_reparando){
                    MostrarReparando();
                }   else{
                    MostrarPulsaParaReparar();
                }
            }
        }
    }

    private void CheckExit(GameObject other){
        if(other.CompareTag(Tags.TAG_PLAYER)) {
            OcultarInterfaces();
        }
    }
}
