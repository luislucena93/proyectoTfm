using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuntoReciclarBot : MonoBehaviour
{
    [SerializeField]
    [Range(0.1f,5f)]
    float _tiempoEsperarAviso = 1;

    
    private void OnTriggerEnter(Collider other) {
        OnEnter(other);
    }


    private void OnCollisionEnter(Collision other) {
        OnEnter(other.collider);
    }

    private void OnEnter(Collider other) {
        Debug.Log("Colision punto reinicio con "+other.name);
        BotLimpieza bot = other.GetComponent<BotLimpieza>();

        if(bot!=null){
            Debug.Log("Es bot");
            StartCoroutine(DesactivarBot(bot));
        }

    }

    IEnumerator DesactivarBot(BotLimpieza bot){
        Debug.Log("entra corrutina");
        yield return new WaitForSeconds(_tiempoEsperarAviso);
        Debug.Log("Vuelve corrutina");
        bot.BotFinalizado();
    }
}
