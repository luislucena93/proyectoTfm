using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerHijoZonaJuntos : MonoBehaviour
{
    [SerializeField]
    Renderer _rendererZona;
    Material _materialZona;
    [SerializeField]
    Color _colorActivo;
    Color _colorOriginal;

    GestorZonaJuntos _gestorZonaJuntos;

    [SerializeField]
    ZonasJuntosEnum _tipoZonaEnum;
    static string STRING_TINT = "_TintColor";

    // Start is called before the first frame update
    void Start()
    {
        _materialZona = _rendererZona.material;
        _colorOriginal = _materialZona.GetColor(STRING_TINT);
        _gestorZonaJuntos = this.transform.parent.GetComponent<GestorZonaJuntos>();

    }



    private void OnTriggerEnter(Collider other) {
        OnEnter(other);
    }

    private void OnTriggerStay(Collider other) {
        OnStay(other);
    }

    private void OnTriggerExit(Collider other) {
        OnExit(other);
    }

    private void OnCollisionEnter(Collision other) {
        OnEnter(other.collider);
    }

    private void OnCollisionStay(Collision other) {
        OnStay(other.collider);
    }

    private void OnCollisionExit(Collision other) {
        OnExit(other.collider);
    }


    private void OnEnter(Collider other){
        if(other.gameObject.CompareTag(Tags.TAG_PLAYER)) {
            _materialZona.SetColor(STRING_TINT,_colorActivo);
        }
    }

    private void OnStay(Collider other){
       if(other.gameObject.CompareTag(Tags.TAG_PLAYER)) {
            _materialZona.SetColor(STRING_TINT,_colorActivo);
            _gestorZonaJuntos.CambioEnZona(_tipoZonaEnum,true);
       }
    }
    private void OnExit(Collider other){
        if(other.gameObject.CompareTag(Tags.TAG_PLAYER)) {
            _materialZona.SetColor(STRING_TINT,_colorOriginal);
            _gestorZonaJuntos.CambioEnZona(_tipoZonaEnum,false);
        }
    }

}


public enum ZonasJuntosEnum {ZonaA, ZonaB};

