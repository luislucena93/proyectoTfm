using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class GestorColeccionables : MonoBehaviour
{
    [SerializeField]
    List<Coleccionable> _listaColeccionables;

    [SerializeField]
    List<BotonColeccionable> _listaBotones;

    [SerializeField]
    TMP_Text _textoNombre;

    [SerializeField]
    GameObject _goCamara;
    [SerializeField]
    GameObject _goCanvasColeccionables;


    // Start is called before the first frame update
    void Start()
    {
        OcultarTodos();
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

    private void MostrarTodos(){
        for(int i = 0; i < _listaColeccionables.Count; i++){
                _listaColeccionables[i].SetActivo(true);
        }
    }

    private void OcultarTodos(){
        for(int i = 0; i < _listaColeccionables.Count; i++){
                _listaColeccionables[i].SetActivo(false);
        }
    }

    private void Inicializar(){
        SetRecogido("AB");
        Activar(EnumColeccionable.Max);
        ActualizarBotones();
    }

    public void AbrirColeccionables(){
        MostrarTodos();
        _goCanvasColeccionables.SetActive(true);
        Inicializar();
        _goCamara.SetActive(true);
        if(_listaBotones.Count>1){
            EventSystem.current.SetSelectedGameObject(_listaBotones[0].gameObject);
        }

    }

    public void CerrarColeccionables(){
        OcultarTodos();
        _goCamara.SetActive(false);
        _goCanvasColeccionables.SetActive(false);
    }


}