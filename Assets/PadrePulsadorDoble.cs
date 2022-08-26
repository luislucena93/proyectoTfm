using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PadrePulsadorDoble : MonoBehaviour
{
    [SerializeField]
    GameObject _goPuertaIPuerta;
    IPuerta _iPuerta;
    // Start is called before the first frame update

    int _pulsados = 0;
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
        _pulsados ++;
        Debug.Log("pulsado hijo "+_pulsados);
        if(!_puertaAbierta && _pulsados >= 2){
            _iPuerta.Abrir();
            _puertaAbierta = true;
        }

    }

    public void SoltadoHijo(){
        _pulsados --;
    }

    public bool GetPuertaAbierta(){
        return _puertaAbierta;
    }
}
