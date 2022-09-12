using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;
public class BotonColeccionable : MonoBehaviour, ISelectHandler
{
    public Coleccionable _goColeccionable;

    Color _colorBordeOriginal;

    [SerializeField]
    Color _colorBordeDesactivado;

    Color _coloriconoOriginal;

    [SerializeField]
    Color _colorIconoDesactivado;

    [SerializeField]
    Image _icono;

    Button _boton;

    Image _bordeBoton;

    [SerializeField]
    GestorColeccionables _gestor;

    private void Awake() {
        _boton = GetComponent<Button>();
        _bordeBoton = GetComponent<Image>();
        _colorBordeOriginal = _bordeBoton.color;
        _coloriconoOriginal = _icono.color;
    }



    public void ActivarColeccionable(){
        //Debug.Log("click");
        _gestor.Seleccionar(_goColeccionable.GetEnumColeccionable());
    }

    public void ActualizarBoton(){
        bool recogido = _goColeccionable.GetRecogido();
        try{
            _bordeBoton.color = recogido ? _colorBordeOriginal : _colorBordeDesactivado;
            _icono.color = recogido ? _coloriconoOriginal : _colorIconoDesactivado;
        }   catch (NullReferenceException e){
            
            Debug.Log("Excepcion en boton "+gameObject.name+" excepcion: "+e.StackTrace);
        }
        
    }

    public void OnSelect(BaseEventData eventData)
    {
        //Debug.Log(this.gameObject.name + " was selected");
        _boton.onClick.Invoke();
    }
}

