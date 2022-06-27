using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Peana : MonoBehaviour, IInteraccionable
{
    [SerializeField]
    GameObject _hologramaBot;
    [SerializeField]
    GameObject _letrero;

    [SerializeField]
    TMP_Text _textoAccion;

    [SerializeField]
    [Range (0,50)]
    float _velocidadGiroHolograma = 5;
    float _velocidadGiroLetrero = 5;

    [SerializeField]
    GameObject _prefabBot;

    private static string MENSAJE_VACIO = "";
    private static string MENSAJE_SOLICITAR_LIMPIEZA = "Solicitar Limpieza";

    private static string MENSAJE_LIMPIEZA_SOLICITADA = "Limpieza Solicitada";

    [SerializeField]
    private GameObject _explosion;


    private bool _interaccionando;
    
    private bool _botActivo;

    [SerializeField]
    GameObject _posicionInicialBot;
    void Start()
    {
        _letrero.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
       _hologramaBot.transform.Rotate(Vector3.up*_velocidadGiroHolograma*Time.deltaTime);
       //_letrero.transform.Rotate(Vector3.up*_velocidadGiroLetrero*Time.deltaTime);
    }

    public void ComenzarInteraccion(){
        _interaccionando = true;

        if(!_botActivo){
            _hologramaBot.SetActive(false);
            GameObject botNuevo = Instantiate(_prefabBot);
            botNuevo.transform.position = _posicionInicialBot.transform.position;
            botNuevo.SetActive(true);
            botNuevo.GetComponent<BotLimpieza>().SetPeanaBase(this);
            _botActivo = true;
        }
    }

    public void PausarInteraccion(){
        _interaccionando = false;
    }

    public void FinalizarInteraccion(){
        _interaccionando = false;
    }

    public Transform GetTransform(){
        return this.transform;
    }


    public void OnEnter(Collider other) {
        
        if(other.gameObject.CompareTag(Tags.TAG_PLAYER)) {
            if(_botActivo){
                _letrero.SetActive(true);
                _textoAccion.text = MENSAJE_LIMPIEZA_SOLICITADA;
            }   else{
                _letrero.SetActive(true);
                _textoAccion.text = MENSAJE_SOLICITAR_LIMPIEZA;
            }
        }
    }

    public void OnStay(Collider other) {
        if(other.gameObject.CompareTag(Tags.TAG_PLAYER)) {
            if(_botActivo){
                _letrero.SetActive(true);
                _textoAccion.text = MENSAJE_LIMPIEZA_SOLICITADA;
            }   else{
                _letrero.SetActive(true);
                _textoAccion.text = MENSAJE_SOLICITAR_LIMPIEZA;
            }
        }
    }

    public void OnExit(Collider other) {
        if(other.gameObject.CompareTag(Tags.TAG_PLAYER)) {
            _letrero.SetActive(false);
        }
    }

    public void BotDestruido(Vector3 posicion){
        Debug.Log("Bot destruido metodo peana");
        _botActivo = false;
        _explosion.transform.position = posicion;
        _explosion.SetActive(true);
        _explosion.GetComponent<Explosion>().Activar();
    }
}
