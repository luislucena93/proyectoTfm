using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PulsadorMantenido : MonoBehaviour, IInteraccionable
{
    [SerializeField]
    GameObject _goCanvasTextoPulsador;

    [SerializeField]
    TMP_Text _textoAccion;
    // Start is called before the first frame update

    [SerializeField]
    GameObject _goPuertaIPuerta;

    IPuerta _iPuerta;

    private bool _interaccionando;

    private static string MENSAJE_VACIO = "";
    private static string MENSAJE_MANTENER_ABRIR= "Manten pulsado\npara abrir";

    void Start()
    {
        _textoAccion.text = "";
        _iPuerta = _goPuertaIPuerta.GetComponent<IPuerta>();
        if(_iPuerta == null){
            Debug.Log("IPuerta no encontrada "+this.name);
        }

        _goCanvasTextoPulsador.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other) {
        
        if(other.gameObject.tag == "Player") {
            if(!_interaccionando){
                MostrarMensajeMantener();
            }   else{
                OcultarMensaje();
            }
        }
    }

    private void OnTriggerStay(Collider other) {
        if(other.gameObject.tag == "Player") {
            if(!_interaccionando){
                MostrarMensajeMantener();
            }   else{
                OcultarMensaje();
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.gameObject.tag == "Player") {
            OcultarMensaje();
            FinalizarInteraccion();
        }
    }

    public void ComenzarInteraccion(){
        _interaccionando = true;
        _goCanvasTextoPulsador.SetActive(true);
        _iPuerta.Abrir();
    }

    public void PausarInteraccion(){
        _interaccionando = false;
        _iPuerta.Cerrar();
    }

    public void FinalizarInteraccion(){
        _interaccionando = false;
        _iPuerta.Cerrar();
    }


 
    private void OnCollisionEnter(Collision other){
        if(other.gameObject.tag == "Player") {
            if(!_interaccionando){
                MostrarMensajeMantener();
            }   else{
                OcultarMensaje();
            }
        }
    }

    private void OnCollisionStay(Collision other) {
        if(other.gameObject.tag == "Player") {
            if(!_interaccionando){
                MostrarMensajeMantener();
            }   else{
                OcultarMensaje();
            }
        }
    }

    private void OnCollisionExit(Collision other){
        if(other.gameObject.tag == "Player") {

            FinalizarInteraccion();
        }
    }

    private void MostrarMensajeMantener(){
        _goCanvasTextoPulsador.SetActive(true);
        _textoAccion.text = MENSAJE_MANTENER_ABRIR;
    }

    private void OcultarMensaje(){
        _goCanvasTextoPulsador.SetActive(false);
        _textoAccion.text = MENSAJE_VACIO;
    }

    public Transform GetTransform(){
        return gameObject.transform;
    }
}
