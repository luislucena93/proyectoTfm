using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerMuerto : MonoBehaviour
{

    /*
    [SerializeField]
    [Range(0.01f,1)]
    float _tamanhoInicial = 0.01f;

    [SerializeField]
    [Range(0.01f,50)]
    float _velocidadCrecimientoTrigger = 1;


    float _tamanhoTrigger = 1;

    private void OnEnable() {
        _tamanhoTrigger = _tamanhoInicial;
        this.transform.localScale = new Vector3(_tamanhoTrigger,1,_tamanhoTrigger);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(_tamanhoTrigger<1){
            _tamanhoTrigger = 1;
            _tamanhoTrigger+=_velocidadCrecimientoTrigger*Time.deltaTime;
            this.transform.localScale = new Vector3(_tamanhoTrigger,1,_tamanhoTrigger);
        }
        
    }*/

    IRecuperarSalud _iSaludPropio;
    
    void OnEnable()
    {   
        if(_iSaludPropio == null){
            _iSaludPropio = (IRecuperarSalud) this.transform.parent.GetComponent(typeof(IRecuperarSalud));
            if(_iSaludPropio == null){
                Debug.Log("Error al recuperar el IRecuperarSalud del padre en TriggerMuerto");
            }
        }
    }


    private void OnTriggerEnter(Collider other) {
        CheckColisionSaludEnter(other);
    }

    private void OnTriggerStay(Collider other) { 
    }

    private void OnTriggerExit(Collider other) {
        CheckColisionSaludExit(other);
    }

    private void OnCollisionEnter(Collision other) {
        CheckColisionSaludEnter(other.collider);
    }

    private void OnCollisionStay(Collision other) { 
    }

    private void OnCollisionExit(Collision other) {
        CheckColisionSaludExit(other.collider);

    }

    void CheckColisionSaludEnter(Collider other){
        Debug.Log("enter triggerSanar");
        if(other.gameObject.layer == Tags.TAG_LAYER_TRIGGER_SANAR){
            _iSaludPropio.SetAvisoMePuedenCurar(true);
        }
    }

    void CheckColisionSaludExit(Collider other){
        Debug.Log("exit triggerSanar");
        if(other.gameObject.layer == Tags.TAG_LAYER_TRIGGER_SANAR){
            _iSaludPropio.SetAvisoMePuedenCurar(false);
        }
    }
}
