using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TriggerTextoPuertaLlave : MonoBehaviour
{
    [SerializeField]
    GameObject goPuerta;

    IPuerta _puerta;


    [SerializeField]
    TMP_Text _textoAccion;

    [SerializeField]
    GameObject _goCanvasMensaje;
    


    private void Awake() {
        _puerta = goPuerta.GetComponent<IPuerta>();
    }

    void Start()
    {
        _goCanvasMensaje.SetActive(false);
        _textoAccion.enabled = false;
    }


    void Update()
    {
        
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
            _puerta.Abrir();
            if(_puerta.isBloqueada()){
                MostrarMensaje();
            }   else{
                OcultarMensaje();
            }
        }
    }

    private void CheckStay(GameObject other){
        if(other.CompareTag(Tags.TAG_PLAYER)) {
            if(_puerta.isBloqueada()){
                MostrarMensaje();
            }   else{
                OcultarMensaje();
            }
        }
    }

    private void CheckExit(GameObject other){
        if(other.CompareTag(Tags.TAG_PLAYER)) {
            _puerta.Cerrar();
            OcultarMensaje();
        }
    }

    private void MostrarMensaje(){
        _goCanvasMensaje.SetActive(true);
        _textoAccion.enabled = true;
    }

    private void OcultarMensaje(){
        _goCanvasMensaje.SetActive(false);
        _textoAccion.enabled = false;
    }
}
