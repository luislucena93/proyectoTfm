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


    string _cadenaColeccionables = "";

    [SerializeField]
    Animator _animatorDialogoRecogido;

    [SerializeField]
    TMP_Text _textoDialogColeccionable;

    [SerializeField]
    DialogueManager _dialogManager;

    

    private void Awake() {
        //Reset coleccionables
        //PlayerPrefs.SetString(GameConstants.PLAYER_PREFS_COLECCIONABLES,"");
    }

    // Start is called before the first frame update
    void Start()
    {
        OcultarTodos();
        RecuperarCadenaColeccionables();
        CheckColeccionablesEscena();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Seleccionar(EnumColeccionable enumC){
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
            Debug.Log("letra "+((char)_listaColeccionables[i].GetEnumColeccionable()));
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
        SetRecogido(_cadenaColeccionables);
        Seleccionar(EnumColeccionable.Max);
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

    private void CheckColeccionablesEscena(){
        GameObject[] coleccionables = GameObject.FindGameObjectsWithTag(GameConstants.TAG_OBJETO_COLECCIONABLE);
        if(coleccionables != null && coleccionables.Length > 0 ){
            foreach(GameObject go in coleccionables){
                Coleccionable col = go.GetComponent<Coleccionable>();
                if(col != null){
                    string cadena = ((char) col.GetEnumColeccionable()).ToString();
                    if(_cadenaColeccionables.Contains(cadena)){
                        go.SetActive(false);
                    }
                }
            }
        }
    }


    public void Recogido(EnumColeccionable enumC){
        _cadenaColeccionables+=(char) enumC;
        PlayerPrefs.SetString(GameConstants.PLAYER_PREFS_COLECCIONABLES,_cadenaColeccionables);
        Debug.Log("Recogido "+enumC.ToString()+"  cadena final "+_cadenaColeccionables);
        _animatorDialogoRecogido.SetBool("Animar",true);
        _textoDialogColeccionable.text = enumC.ToString()+" desbloqueado.";
    }

    private void RecuperarCadenaColeccionables(){
        _cadenaColeccionables = PlayerPrefs.GetString(GameConstants.PLAYER_PREFS_COLECCIONABLES);
        if(_cadenaColeccionables == null || _cadenaColeccionables.Length == 0){
            _cadenaColeccionables = "A";
        }
        Debug.Log("RecuperarCadena "+_cadenaColeccionables);
    }

    public void RecogidaLlave(TipoLlaveEnum enumLlave){
        if(!_dialogManager.isDialogueOpen()){
            _animatorDialogoRecogido.SetBool("Animar",true);
            _textoDialogColeccionable.text = "Llave "+enumLlave.ToString()+" recogida.";
        }
    }
}