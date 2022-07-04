using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestorZonaJuntos : MonoBehaviour
{
    [SerializeField]
    GameObject _goPuerta;
    IPuerta _puerta;

    bool _zonaAActiva;
    bool _zonaBActiva;

    // Start is called before the first frame update
    void Start()
    {
        _puerta = _goPuerta.GetComponent<IPuerta>();
        if(_puerta == null){
            Debug.LogError("Puerta null en "+this.name);
        }
    }

    public void CambioEnZona(ZonasJuntosEnum zona, bool estado){
        if(zona == ZonasJuntosEnum.ZonaA){
            _zonaAActiva = estado;
        }   else if(zona == ZonasJuntosEnum.ZonaB){
            _zonaBActiva = estado;
        }

        if(_zonaAActiva && _zonaBActiva){{
            _puerta.Abrir(TipoLlaveEnum.Juntos);
        }}
    }

}

