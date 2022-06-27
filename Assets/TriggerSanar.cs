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
        if(other.gameObject.CompareTag(Tags.TAG_PLAYER)){
            /*IRecuperarSalud iSalud = (IRecuperarSalud) other.gameObject.GetComponent(typeof(IRecuperarSalud));
            if(iSalud != null){
                if(iSalud.IsDead()){
                    iSalud.RecuperarSalud(_puntosRecupera);
                    _consumido = true;
                    this.gameObject.SetActive(false);
                }
            }*/

            _iSaludPropio.SetAvisoCurable(true);
        }
    }

    void CheckColisionSaludExit(Collider other){
        if(other.gameObject.CompareTag(Tags.TAG_PLAYER)){
            /*IRecuperarSalud iSalud = (IRecuperarSalud) other.gameObject.GetComponent(typeof(IRecuperarSalud));
            if(iSalud != null){
                if(iSalud.IsDead()){
                    iSalud.RecuperarSalud(_puntosRecupera);
                    _consumido = true;
                    this.gameObject.SetActive(false);
                }
            }*/

            _iSaludPropio.SetAvisoCurable(false);
        }
    }

}
