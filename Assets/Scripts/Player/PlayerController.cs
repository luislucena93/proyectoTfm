using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    PlayerStateMachine playerStateMachine;

    private void Awake()
    {
        playerStateMachine = GetComponent<PlayerStateMachine>();
    }

    private void OnTriggerStay(Collider other)
    {
        //Debug.Log("GHasd");
        if(other.gameObject.tag == "ColliderPuerta")
        {
            Debug.Log("Estoy en la zona");
            if (playerStateMachine.inputReader.interactAction.triggered)
            {
                other.gameObject.GetComponent<TriggerPuertaBoton>().accionarPuerta();
                Debug.Log("Estoy abriendo la puerta");
            }
        }
    }

}
