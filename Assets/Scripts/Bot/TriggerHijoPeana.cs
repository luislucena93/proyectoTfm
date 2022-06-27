using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerHijoPeana : MonoBehaviour, IInteraccionable
{
    [SerializeField]
    Peana _peana;

    [SerializeField]
    GameObject _referenciaMano;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ComenzarInteraccion(){
        _peana.ComenzarInteraccion();
    }

    public void PausarInteraccion(){
        _peana.PausarInteraccion();
    }

    public void FinalizarInteraccion(){
        _peana.FinalizarInteraccion();
    }

    public Transform GetTransform(){
        return _referenciaMano.transform;
    }


    private void OnTriggerEnter(Collider other) {
        _peana.OnEnter(other);
    }

    private void OnTriggerStay(Collider other) {
        _peana.OnStay(other);
    }

    private void OnTriggerExit(Collider other) {
        _peana.OnExit(other);
    }

    private void OnCollisionEnter(Collision other) {
        _peana.OnEnter(other.collider);
    }

    private void OnCollisionStay(Collision other) {
        _peana.OnStay(other.collider);
    }

    private void OnCollisionExit(Collision other) {
        _peana.OnExit(other.collider);
    }
}
