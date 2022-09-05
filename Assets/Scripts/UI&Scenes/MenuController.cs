using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


public class MenuController : MonoBehaviour
{
    private static string _rutaFichero;
    public Animator menu, transitions;
    public GameObject InitialButton;

    public GameObject _reiniciarButton;

    public GameObject _coleccionablesButton;

    public GameObject _textoPausa;
    public GameObject _textoDead;

    [SerializeField]
    PlayerStateMachine _p1StateMachine;
    [SerializeField]
    PlayerStateMachine _p2StateMachine;

    [SerializeField]
    bool _comienzoNivel;

    [SerializeField]
    bool _nivelCero;

    [SerializeField]
    GestorColeccionables _gestorColeccionables;

    [SerializeField]
    CanvasHUDAnim _canvasUIJuego;

    bool _pantallaColeccionablesActiva;

    [field: SerializeField]
    private Animator _transiciones;

    private void Awake() {
        _rutaFichero = Application.persistentDataPath + "/gamesave.save";
       // Debug.Log("ruta awake"+_rutaFichero);
    }

    private void Start() {
        CompruebaRecargarGuardarDatos();
    }

    public void OpenCloseMenu() {
        if(!_pantallaColeccionablesActiva){
            InitialButton.SetActive(true);
            gameObject.SetActive(true);
            _textoDead.SetActive(false);
            _textoPausa.SetActive(true);
            if (menu.GetBool("menuIsOpen") == false) {
                Cursor.visible = true;
                menu.SetBool("menuIsOpen", true);
                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(InitialButton);
                Time.timeScale = 0f;
            }
            else {
                Cursor.visible = false;
                Time.timeScale = 1f;
                menu.SetBool("menuIsOpen", false);
            }
        }
    }
    public void Continue() {
        OpenCloseMenu();
    }

    public void ExitGame() {
        Application.Quit();
    }

    public void ReloadScene() {
        OpenCloseMenu();
        StartCoroutine(RecargaLevel());
    }

    public void SiguienteEscena(){
        _transiciones.SetTrigger("NextScene");
        GuardarPartida();
        StartCoroutine(NextLevel());
    }

    private IEnumerator NextLevel() {
        transitions.SetTrigger("NextScene");
        yield return new WaitForSeconds(1);
        int sceneID = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(sceneID);
    }

    private IEnumerator RecargaLevel() {
        transitions.SetTrigger("NextScene");
        yield return new WaitForSeconds(1);
        int sceneID = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void VolverEscenaInicio(){
        Time.timeScale = 1f;
        StartCoroutine(VolverPantallaInicio());
    }

    private IEnumerator VolverPantallaInicio() {
        transitions.SetTrigger("NextScene");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(0);
    }



     public void CheckMuertos(){
        InitialButton.SetActive(false);
        if(_p1StateMachine.IsDead() && _p2StateMachine.IsDead()){
            _coleccionablesButton.SetActive(false);
            _textoDead.SetActive(true);
            _textoPausa.SetActive(false);
            if (menu.GetBool("menuIsOpen") == false) {
                menu.SetBool("menuIsOpen", true);
                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(_reiniciarButton);
                Time.timeScale = 0f;
            }
        }
    }


    private void GuardarPartida(){
        PartidaGuardada partidaGuardada = new PartidaGuardada();
        partidaGuardada.puntosVidaJ1 = _p1StateMachine.GetNivelSalud();
        partidaGuardada.puntosVidaJ2 = _p2StateMachine.GetNivelSalud();
        partidaGuardada.listaLlavesJ1 = _p1StateMachine.GetHUDJugador().GetLlaves();
        partidaGuardada.listaLlavesJ2 = _p2StateMachine.GetHUDJugador().GetLlaves();
        partidaGuardada.nivelActual = SceneManager.GetActiveScene().name;


        BinaryFormatter bf = new BinaryFormatter();
        if(File.Exists(_rutaFichero)){
            Debug.Log("entra borrar");
            File.Delete(_rutaFichero);
        }
        FileStream file = File.Create(_rutaFichero);
        bf.Serialize(file, partidaGuardada);
        file.Close();
    }

    public void CargarDatosPartida(){ 
        if (File.Exists(_rutaFichero)){
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(_rutaFichero, FileMode.Open);
            PartidaGuardada partidaGuardada = (PartidaGuardada)bf.Deserialize(file);
            file.Close();

            _p1StateMachine.SetNivelSaludComienzo(partidaGuardada.puntosVidaJ1);
            _p2StateMachine.SetNivelSaludComienzo(partidaGuardada.puntosVidaJ2);
            for(int i = 0; i < partidaGuardada.listaLlavesJ1.Count; i++){
                _p1StateMachine.GetHUDJugador().RecogidaTarjeta(partidaGuardada.listaLlavesJ1[i]);
            }

            for(int i = 0; i < partidaGuardada.listaLlavesJ2.Count; i++){
                _p2StateMachine.GetHUDJugador().RecogidaTarjeta(partidaGuardada.listaLlavesJ2[i]);
            }
        }
    }



    private void CompruebaRecargarGuardarDatos(){
        if(!_nivelCero && !_comienzoNivel){
            CargarDatosPartida();
        }
    }

    public void AbrirColeccionables(){
        Cursor.visible = true;
        _textoDead.SetActive(false);
        _textoPausa.SetActive(false);
        menu.SetBool("menuIsOpen", false);
        _gestorColeccionables.AbrirColeccionables();
        _pantallaColeccionablesActiva = true;
        _canvasUIJuego.Ocultar();

    }

    public void CerrarColeccionables(){
        _gestorColeccionables.CerrarColeccionables();
        _canvasUIJuego.Mostrar();
        _textoDead.SetActive(false);
        _textoPausa.SetActive(true);
        _pantallaColeccionablesActiva = false;
        if (menu.GetBool("menuIsOpen") == false) {
            menu.SetBool("menuIsOpen", true);
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(InitialButton);
        }
    }


}