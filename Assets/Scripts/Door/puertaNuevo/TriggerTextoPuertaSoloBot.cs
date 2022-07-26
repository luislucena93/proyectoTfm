using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TriggerTextoPuertaSoloBot : MonoBehaviour
{
    [SerializeField]
    GameObject goPuerta;

    IPuerta _puerta;


    [SerializeField]
    TMP_Text _textoAccion;

    [SerializeField]
    GameObject _goCanvasMensajeAveriado;

    private void Awake() {
        _puerta = goPuerta.GetComponent<IPuerta>();
    }

    void Start()
    {
        _goCanvasMensajeAveriado.SetActive(false);
        _textoAccion.enabled = false;
    }

    // Update is called once per frame
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
        if(other.CompareTag(Tags.TAG_BOT)) {
            _puerta.Abrir();
            OcultarMensaje();
        }   else  if(other.CompareTag(Tags.TAG_PLAYER)) {
            if(!_puerta.isAbierta()){
                MostrarMensaje();
            }        
        }
    }

    private void CheckStay(GameObject other){
        if(other.CompareTag(Tags.TAG_BOT)) {
            _puerta.Abrir();
            OcultarMensaje();
        }   else  if(other.CompareTag(Tags.TAG_PLAYER)) {
            if(!_puerta.isAbierta()){
                MostrarMensaje();
            }        
        }
    }

    private void CheckExit(GameObject other){
        if(other.CompareTag(Tags.TAG_BOT)) {
            _puerta.Cerrar();
            OcultarMensaje();
        }   else  if(other.CompareTag(Tags.TAG_PLAYER)) {
            OcultarMensaje();     
        }
    }

    private void MostrarMensaje(){
        _goCanvasMensajeAveriado.SetActive(true);
        _textoAccion.enabled = true;
    }

    private void OcultarMensaje(){
        _goCanvasMensajeAveriado.SetActive(false);
        _textoAccion.enabled = false;
    }
}
