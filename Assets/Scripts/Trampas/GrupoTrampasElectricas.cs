using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrupoTrampasElectricas : MonoBehaviour, IMaquina
{
    [SerializeField]
    List<GameObject> _listaGO = new List<GameObject>();

    List<IMaquina> _listaTrampas = new List<IMaquina>();

    [SerializeField]
    bool _comienzaActivo = false;

    bool _estadoActual;

    public 
    void Start()
    {
        IMaquina _iMaq;
        for(int i = 0; i < _listaGO.Count; i++){
            _iMaq = _listaGO[i].GetComponent<IMaquina>();
            if(_iMaq != null){
                _listaTrampas.Add(_iMaq);
            }
        }
        _estadoActual = _comienzaActivo;
        StartCoroutine(CoroutineInicio());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Encender(bool encender){
        _estadoActual = encender;
        for(int i = 0; i < _listaTrampas.Count; i++){
            _listaTrampas[i].Encender(encender);
        }
    }

    public void AlternarEstado(){
        _estadoActual = !_estadoActual;
        Encender(_estadoActual);
    }

    public bool IsEncendida(){
        return _estadoActual;
    }

    IEnumerator CoroutineInicio() {
        yield return new WaitForEndOfFrame();
        Encender(_comienzaActivo);
    }
}