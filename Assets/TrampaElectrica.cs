using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrampaElectrica : MonoBehaviour, IMaquina
{
    [SerializeField]
    GameObject _zonaElectrica;

    [SerializeField]
    GameObject _goLuzEfectos;

    [SerializeField]
    bool _comienzaActivo = true;

    bool _estadoActual;

    [SerializeField]
    Color _colorSueloOscurecido;

    Color _colorOriginal;

    [SerializeField]
    Renderer _rendererSuelo;
    Material _materialSuelo;

    void Start()
    {
        _estadoActual = _comienzaActivo;
        _materialSuelo = _rendererSuelo.material;
        _colorOriginal = _materialSuelo.color;
        Encender(_estadoActual);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Encender(bool encender){
        _estadoActual = encender;
        _zonaElectrica.SetActive(_estadoActual);
        _goLuzEfectos.SetActive(_estadoActual);
        _materialSuelo.color = _estadoActual?_colorOriginal:_colorSueloOscurecido;
    }

    public void AlternarEstado(){
        _estadoActual = !_estadoActual;
        Encender(_estadoActual);
    }

    public bool IsEncendida(){
        return _estadoActual;
    }
}
