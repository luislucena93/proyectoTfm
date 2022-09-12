using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OrdenadorFinalSecundario : MonoBehaviour, IInteraccionable{
    [SerializeField]
    GameObject _goCanvasTextoPulsador;

    [SerializeField]
    GameObject _goPensando;

    [SerializeField]
    GameObject _goLuz;

    [SerializeField]
    TMP_Text _textoNumero;

    [SerializeField]
    TMP_Text _textoAccion;

    [SerializeField]
    GameObject _goPosicionarMano;
    [SerializeField]
    GameObject _goIndicarInteraccion;


    EnumPCFinalZona3 _enumPC;

    OrdenadorFinalCentral _pcPrincipal;

    private static string MENSAJE_VACIO = "";
    private static string MENSAJE_HURRY = "HURRY";
    bool _activo = false;

    bool _mostrarIndicarInteraccion;

    void Start()
    {
        DesactivarPC();
    }


    private void OnTriggerEnter(Collider other) {
        OnEnter(other);
    }

    private void OnTriggerStay(Collider other) {
        OnStay(other);
    }

    private void OnTriggerExit(Collider other) {
        OnExit(other);
    }


    private void OnCollisionEnter(Collision other){
        OnEnter(other.collider);
    }

    private void OnCollisionStay(Collision other) {
        OnStay(other.collider);
    }

    private void OnCollisionExit(Collision other){
        OnExit(other.collider);
    }

    public void ComenzarInteraccion(){
        if(_activo){
            _goIndicarInteraccion.SetActive(false);
            _pcPrincipal.PulsadoSecundario(_enumPC);
        }
    }

     public void PausarInteraccion(){
    }

    public void FinalizarInteraccion(){
        if(_activo){
            _goIndicarInteraccion.SetActive(true);
        }
    }

 
    private void OnEnter(Collider other){
        if(other.gameObject.CompareTag(GameConstants.TAG_PLAYER)){
            if(_activo){
                _goIndicarInteraccion.SetActive(true);
            }
        }
    }

    private void OnStay(Collider other) {
        if(other.gameObject.CompareTag(GameConstants.TAG_PLAYER)){
            if(_activo){
                _goIndicarInteraccion.SetActive(true);
            }
        }
    }

    private void OnExit(Collider other){
        if(other.gameObject.CompareTag(GameConstants.TAG_PLAYER)){
            if(_activo){
                _goIndicarInteraccion.SetActive(false);
            }
        }
    }


    public Transform GetTransform(){
        return _goPosicionarMano.transform;
    }


    public void SetEnumPC(EnumPCFinalZona3 ePC, OrdenadorFinalCentral pcPrincipal){
        _enumPC = ePC;
        _pcPrincipal = pcPrincipal;
    }

    public void ActivarPC(string texto){
        _goCanvasTextoPulsador.SetActive(true);
        _goLuz.SetActive(true);
        _textoNumero.text = texto;
        _textoAccion.text = MENSAJE_VACIO;
        _activo = true;
        _mostrarIndicarInteraccion = true;
    }

    public void ActualizarTexto(string texto){
        _textoNumero.text = texto;
    }

    public void DesactivarPC(){
        _goCanvasTextoPulsador.SetActive(false);
        _goPensando.SetActive(false);
        _goLuz.SetActive(false);
        _goIndicarInteraccion.SetActive(false);
        _activo = false;
         _mostrarIndicarInteraccion = false;
    }
}