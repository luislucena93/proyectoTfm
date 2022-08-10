using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GestorColeccionables : MonoBehaviour
{
    [SerializeField]
    List<Coleccionable> _listaColeccionables;

    [SerializeField]
    List<BotonColeccionable> _listaBotones;

    [SerializeField]
    TMP_Text _textoNombre;

    // Start is called before the first frame update
    void Start()
    {
        SetRecogido("AB");
        Activar(EnumColeccionable.El);
        ActualizarBotones();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Activar(EnumColeccionable enumC){
        for(int i = 0; i < _listaColeccionables.Count; i++){
            if(_listaColeccionables[i].GetEnumColeccionable() == enumC){
                _listaColeccionables[i].SetActivo(true);
                _textoNombre.text = _listaColeccionables[i].GetRecogido()?_listaColeccionables[i].GetNombre():"???";
            }   else{
                _listaColeccionables[i].SetActivo(false);
            }
        }
    }

    public void SetRecogido(string cadena){
        for(int i = 0; i < _listaColeccionables.Count; i++){
//            Debug.Log("letra "+((char)_listaColeccionables[i].GetEnumColeccionable()));
            _listaColeccionables[i].SetRecogido(cadena.Contains(((char)_listaColeccionables[i].GetEnumColeccionable()).ToString()));
        }
    }

    private void ActualizarBotones(){
        for(int i = 0; i < _listaBotones.Count; i++){
            _listaBotones[i].ActualizarBoton();
        }
    }
}