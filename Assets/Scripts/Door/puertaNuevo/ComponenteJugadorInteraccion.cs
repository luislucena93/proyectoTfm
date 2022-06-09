using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponenteJugadorInteraccion : MonoBehaviour
{
    public IInteraccionable _objetoInteraccionable;

    private InputReader _inputReader;

    private void Awake() {
        _inputReader = GetComponent<InputReader>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _inputReader = GetComponent<InputReader>();
        if(_inputReader == null){
            Debug.Log("No se pudo hacer GetComponent de InputReader "+this.name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(_inputReader.interactAction.WasPressedThisFrame()){
            if(_objetoInteraccionable!=null){
                _objetoInteraccionable.ComenzarInteraccion();
            }
        }
        if(_inputReader.interactAction.WasReleasedThisFrame()){
            if(_objetoInteraccionable!=null){
                _objetoInteraccionable.FinalizarInteraccion();
            }
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag(Tags.TAG_INTERACCIONABLE)){
            GameObject otro = other.gameObject;
            IInteraccionable i = otro.GetComponent<IInteraccionable>();
            if(i != null){
                _objetoInteraccionable = i;
            }
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag(Tags.TAG_INTERACCIONABLE) && _objetoInteraccionable != null){
            _objetoInteraccionable.FinalizarInteraccion();
            _objetoInteraccionable = null;
        }
    }
}
