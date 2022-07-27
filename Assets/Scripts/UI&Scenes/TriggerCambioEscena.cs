using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCambioEscena : MonoBehaviour
{

    [SerializeField]
    MenuController _menuController;
    private void OnTriggerEnter(Collider other) {
        CheckEnter(other.gameObject);
    }

    private void OnCollisionEnter(Collision other){
        CheckEnter(other.gameObject);
    }



    private void CheckEnter(GameObject other){
        if(other.CompareTag(Tags.TAG_PLAYER)) {
            _menuController.SiguienteEscena();
        }
    }
}
