using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSanar : MonoBehaviour
{

    IRecuperarSalud _iSaludPropio;
    private void Start() {
        _iSaludPropio = (IRecuperarSalud) gameObject.transform.parent.GetComponent(typeof(IRecuperarSalud));


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
        //Debug.Log("enter triggerSanar");
        if(other.gameObject.layer == Tags.TAG_LAYER_TRIGGER_MUERTO){
            //Debug.Log("enter triggerSanarMuerto");
            IRecuperarSalud iSaludDetectado = (IRecuperarSalud) other.gameObject.transform.parent.GetComponent(typeof(IRecuperarSalud));
            if(iSaludDetectado != null){
                if(iSaludDetectado.IsDead()){
                    _iSaludPropio.SetAvisoCurable(true, iSaludDetectado);
                }
            }   else{
                Debug.Log("Error al detectar collider muerto");
            }
        }
    }

    void CheckColisionSaludExit(Collider other){
        //Debug.Log("exit triggerSanar");
        if(other.gameObject.layer == Tags.TAG_LAYER_TRIGGER_MUERTO){
            //Debug.Log("exit triggerSanarMuerto");
            _iSaludPropio.SetAvisoCurable(true, null);
        }
    }
}