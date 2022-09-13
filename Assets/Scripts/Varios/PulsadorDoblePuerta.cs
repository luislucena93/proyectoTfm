using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PulsadorDoblePuerta : MonoBehaviour, IInteraccionable
{
    [SerializeField]
    GameObject _goCanvasTextoPulsador;

    [SerializeField]
    TMP_Text _textoAccion;
    // Start is called before the first frame update

    

    [SerializeField]
    GameObject _goPosicionarMano;

    PadrePulsadorDoble _padrePulsadorDoble;
    [SerializeField]
    private bool _interaccionando;

    private static string MENSAJE_VACIO = "";
    private static string MENSAJE_MANTENER_ABRIR= "Manten pulsado\npara abrir";

    private static string MENSAJE_PUELTA_ABIERTA= "Puerta\nAbierta";

    void Start()
    {
        _padrePulsadorDoble = GetComponentInParent<PadrePulsadorDoble>();
        _textoAccion.text = MENSAJE_MANTENER_ABRIR;


        _goCanvasTextoPulsador.SetActive(false);
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
        Debug.Log("Comienza "+gameObject.name);
        _interaccionando = true;
        _goCanvasTextoPulsador.SetActive(true);
        _padrePulsadorDoble.PulsadoHijo();
    }

    public void PausarInteraccion(){
         Debug.Log("Pausa "+gameObject.name);
        _interaccionando = false;
    }

    public void FinalizarInteraccion(){
         Debug.Log("Finaliza "+gameObject.name);
        _interaccionando = false;
    }


 
    private void OnEnter(Collider other){
        if(other.gameObject.CompareTag(GameConstants.TAG_PLAYER)){
            if(!_interaccionando){
                if(_padrePulsadorDoble.GetPuertaAbierta()){
                    MostrarMensajeAbierta();
                } else{
                    MostrarMensajeMantener();
                }
            }   else{
                OcultarMensaje();
            }
        }
    }

    private void OnStay(Collider other) {
        if(other.gameObject.CompareTag(GameConstants.TAG_PLAYER)){
            if(!_interaccionando){
                if(_padrePulsadorDoble.GetPuertaAbierta()){
                    MostrarMensajeAbierta();
                } else{
                    MostrarMensajeMantener();
                }
            }   else{
                OcultarMensaje();
            }
        }
    }

    private void OnExit(Collider other){
        if(other.gameObject.CompareTag(GameConstants.TAG_PLAYER)){

            FinalizarInteraccion();
        }
    }

    private void MostrarMensajeMantener(){
        _goCanvasTextoPulsador.SetActive(true);
        _textoAccion.text = MENSAJE_MANTENER_ABRIR;
    }
    private void MostrarMensajeAbierta(){
        _goCanvasTextoPulsador.SetActive(true);
        _textoAccion.text = MENSAJE_PUELTA_ABIERTA;
    }

    private void OcultarMensaje(){
        _goCanvasTextoPulsador.SetActive(false);
        _textoAccion.text = MENSAJE_VACIO;
    }

    public Transform GetTransform(){
        return _goPosicionarMano.transform;
    }

    public bool isInteraccionando(){
        return _interaccionando;
    }
}
