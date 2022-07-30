using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCheckSuelo : MonoBehaviour
{
    PlayerStateMachine _playerStateMachinePadre;
    SphereCollider _collider;
    string _playerName;
    private void Awake() {
        _playerStateMachinePadre = gameObject.transform.parent.GetComponent<PlayerStateMachine>();
        _collider = GetComponent<SphereCollider>();
        _playerName = gameObject.transform.parent.name;
    }

    [SerializeField]
    List<GameObject> _listaGOContacto = new List<GameObject>();



    private void OnTriggerEnter(Collider other) {
        EnterCollision(other);
    }

    private void OnTriggerStay(Collider other) { 
    }

    private void OnTriggerExit(Collider other) {
        ExitCollision(other);
    }

    private void OnCollisionEnter(Collision other) {
        EnterCollision(other.collider);
    }

    private void OnCollisionStay(Collision other) { 
    }

    private void OnCollisionExit(Collision other) {
        ExitCollision(other.collider);

    }

    private void EnterCollision(Collider other){
        _listaGOContacto.Add(other.gameObject);
    }

    private void ExitCollision(Collider other){
        _listaGOContacto.Remove(other.gameObject);
    }

    private void FixedUpdate() {
        //if(_playerName == "Player1")  Debug.Log("Fixed Count "+_playerName+" "+_listaGOContacto.Count);
        _playerStateMachinePadre.CollisionSuelo(_listaGOContacto.Count>0);
    }
}
