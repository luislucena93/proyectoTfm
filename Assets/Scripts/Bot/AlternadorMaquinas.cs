using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlternadorMaquinas : MonoBehaviour, IMaquina
{
    [SerializeField]
    List<GameObject> _listaObjetosA = new List<GameObject>();
    [SerializeField]
    List<GameObject> _listaObjetosB = new List<GameObject>();
    List<IMaquina> _listaA = new List<IMaquina>();
    [SerializeField]
     List<IMaquina> _listaB = new List<IMaquina>();
    // Start is called before the first frame update


    [SerializeField]
    bool _comienzaActivoA = true;

    bool _estadoActual;

    public 
    void Start()
    {
        IMaquina _iMaq;
        for(int i = 0; i < _listaObjetosA.Count; i++){
            _iMaq = _listaObjetosA[i].GetComponent<IMaquina>();
            if(_iMaq != null){
                _listaA.Add(_iMaq);
            }
        }
        for(int i = 0; i < _listaObjetosB.Count; i++){
            _iMaq = _listaObjetosB[i].GetComponent<IMaquina>();
            if(_iMaq != null){
                _listaB.Add(_iMaq);
            }
        }
        _estadoActual = _comienzaActivoA;
        StartCoroutine(CoroutineInicio());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Encender(bool encender){
        _estadoActual = encender;
        for(int i = 0; i < _listaA.Count; i++){
            _listaA[i].Encender(encender);
        }
        for(int i = 0; i < _listaA.Count; i++){
            _listaB[i].Encender(!encender);
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
        Encender(_comienzaActivoA);
    }
}
