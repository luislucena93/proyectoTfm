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


    EnumPCFinalZona3 _enumPC;

    OrdenadorFinalCentral _pcPrincipal;

    private static string MENSAJE_VACIO = "";
    private static string MENSAJE_HURRY = "HURRY";
    bool _activo = false;

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

    }

     public void PausarInteraccion(){
    }

    public void FinalizarInteraccion(){

    }

 
    private void OnEnter(Collider other){
        if(other.gameObject.CompareTag(GameConstants.TAG_PLAYER)){
            if(_activo){
                _textoAccion.text = MENSAJE_HURRY;
            }
        }
    }

    private void OnStay(Collider other) {
        if(other.gameObject.CompareTag(GameConstants.TAG_PLAYER)){
            if(_activo){
                _textoAccion.text = MENSAJE_HURRY;
            }
        }
    }

    private void OnExit(Collider other){
        if(other.gameObject.CompareTag(GameConstants.TAG_PLAYER)){
            if(_activo){
                _textoAccion.text = MENSAJE_VACIO;
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
        _goPensando.SetActive(true);
        _goLuz.SetActive(true);
        _textoNumero.text = texto;
        _textoAccion.text = MENSAJE_VACIO;
        _activo = true;
    }

    public void ActualizarTexto(string texto){
        _textoAccion.text = texto;
    }

    public void DesactivarPC(){
        _goCanvasTextoPulsador.SetActive(false);
        _goPensando.SetActive(false);
        _goLuz.SetActive(false);
        _activo = false;
    }
}