using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BotonColeccionable : MonoBehaviour
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
        Debug.Log("click");
        _gestor.Activar(_goColeccionable.GetEnumColeccionable());
    }

    public void ActualizarBoton(){
        bool recogido = _goColeccionable.GetRecogido();
        _bordeBoton.color = recogido ? _colorBordeOriginal : _colorBordeDesactivado;
        _icono.color = recogido ? _coloriconoOriginal : _colorIconoDesactivado;
    }
}

