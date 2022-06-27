using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerIPuerta : MonoBehaviour
{
    [SerializeField]
    GameObject goPuerta;

    IPuerta _puerta;

    void Awake()
    {
        _puerta = goPuerta.GetComponent<IPuerta>();
    }




    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag(Tags.TAG_PLAYER) || other.gameObject.CompareTag(Tags.TAG_BOT))
        {
            _puerta.Abrir();
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(Tags.TAG_PLAYER) || other.gameObject.CompareTag(Tags.TAG_BOT))
        {
            _puerta.Cerrar();
        }
    }

    private void OnTriggerStay(Collider other) {
        if (other.gameObject.CompareTag(Tags.TAG_PLAYER) || other.gameObject.CompareTag(Tags.TAG_BOT))
        {
            _puerta.Abrir();
        }
    }


    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag(Tags.TAG_PLAYER) || other.gameObject.CompareTag(Tags.TAG_BOT))
        {
            _puerta.Abrir();
        }
    }

    private void OnCollisionExit(Collision other) {
        
        if (other.gameObject.CompareTag(Tags.TAG_PLAYER) || other.gameObject.CompareTag(Tags.TAG_BOT))     
        {
            _puerta.Cerrar();
        }

    }

    private void OnCollisionStay(Collision other) {
        if (other.gameObject.CompareTag(Tags.TAG_PLAYER) || other.gameObject.CompareTag(Tags.TAG_BOT))
        {
            _puerta.Abrir();
        }
    }
}
