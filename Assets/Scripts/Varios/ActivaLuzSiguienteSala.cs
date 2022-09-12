using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivaLuzSiguienteSala : MonoBehaviour
{
    [SerializeField]
    GameObject _luz;

    private void OnTriggerEnter(Collider other) {
        OnEnter(other);
    }

    private void OnCollisionEnter(Collision other){
        OnEnter(other.collider);
    }

    private void OnEnter(Collider other) {
        if(other.CompareTag(Tags.TAG_PLAYER)){
            _luz.SetActive(true);
        }
    }
}
