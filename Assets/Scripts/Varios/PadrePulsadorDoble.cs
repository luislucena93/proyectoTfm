using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PadrePulsadorDoble : MonoBehaviour
{
    [SerializeField]
    GameObject _goPuertaIPuerta;
    IPuerta _iPuerta;
    // Start is called before the first frame update

    [SerializeField]
    PulsadorDoblePuerta _pulsador1;
    [SerializeField]
    PulsadorDoblePuerta _pulsador2;

    bool _puertaAbierta;
    void Start()
    {
        _iPuerta = _goPuertaIPuerta.GetComponent<IPuerta>();
        if(_iPuerta == null){
            Debug.Log("IPuerta no encontrada "+this.name);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PulsadoHijo(){
        if(_pulsador1.isInteraccionando() && _pulsador2.isInteraccionando() && !_puertaAbierta){
            _iPuerta.Abrir();
            _puertaAbierta = true;
        }
    }

    public void SoltadoHijo(){

    }

    public bool GetPuertaAbierta(){
        return _puertaAbierta;
    }
}
