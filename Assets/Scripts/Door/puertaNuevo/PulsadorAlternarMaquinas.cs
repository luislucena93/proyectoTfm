using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PulsadorAlternarMaquinas : MonoBehaviour, IInteraccionable
{
    [SerializeField]
    GameObject _goCanvasTextoPulsador;

    [SerializeField]
    TMP_Text _textoAccion;
    // Start is called before the first frame update

    [SerializeField]
    GameObject _goMaquina;

    IMaquina _iMaquina;

    private bool _interaccionando;

    private static string MENSAJE_VACIO = "";
    private static string MENSAJE_PULSA_PARA_CAMBIAR= "Pulsa para\ncambiar ;)";

    void Start()
    {
        _textoAccion.text = MENSAJE_PULSA_PARA_CAMBIAR;
        _iMaquina = _goMaquina.GetComponent<IMaquina>();
        if(_iMaquina == null){
            Debug.Log("IMaquina no encontrada "+this.name);
        }

        _goCanvasTextoPulsador.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other) {
        
        if(other.gameObject.CompareTag(GameConstants.TAG_PLAYER)){
            if(!_interaccionando){
                MostrarMensajeMantener();
            }   else{
                OcultarMensaje();
            }
        }
    }

    private void OnTriggerStay(Collider other) {
        if(other.gameObject.CompareTag(GameConstants.TAG_PLAYER)){
            if(!_interaccionando){
                MostrarMensajeMantener();
            }   else{
                OcultarMensaje();
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.gameObject.CompareTag(GameConstants.TAG_PLAYER)){
            OcultarMensaje();
            FinalizarInteraccion();
        }
    }

    public void ComenzarInteraccion(){
        _interaccionando = true;
        _goCanvasTextoPulsador.SetActive(true);
        _iMaquina.AlternarEstado();
    }

    public void PausarInteraccion(){
        _interaccionando = false;
    }

    public void FinalizarInteraccion(){
        _interaccionando = false;
    }


 
    private void OnCollisionEnter(Collision other){
        if(other.gameObject.CompareTag(GameConstants.TAG_PLAYER)){
            if(!_interaccionando){
                MostrarMensajeMantener();
            }   else{
                OcultarMensaje();
            }
        }
    }

    private void OnCollisionStay(Collision other) {
        if(other.gameObject.CompareTag(GameConstants.TAG_PLAYER)){
            if(!_interaccionando){
                MostrarMensajeMantener();
            }   else{
                OcultarMensaje();
            }
        }
    }

    private void OnCollisionExit(Collision other){
        if(other.gameObject.CompareTag(GameConstants.TAG_PLAYER)){

            FinalizarInteraccion();
        }
    }

    private void MostrarMensajeMantener(){
        _goCanvasTextoPulsador.SetActive(true);
        _textoAccion.text = MENSAJE_PULSA_PARA_CAMBIAR;
    }

    private void OcultarMensaje(){
        _goCanvasTextoPulsador.SetActive(false);
        _textoAccion.text = MENSAJE_VACIO;
    }

    public Transform GetTransform(){
        return gameObject.transform;
    }
}
