using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class OrdenadorFinalCentral : MonoBehaviour, IInteraccionable{ [SerializeField]
    GameObject _goCanvasTextoPulsador;

    [SerializeField]
    GameObject _goPensando;

    [SerializeField]
    GameObject _goLuz;

    [SerializeField]
    TMP_Text _textoNumero;

    [SerializeField]
    TMP_Text _textoAccion;

    [SerializeField]
    GameObject _goPosicionarMano;

    [SerializeField]
    OrdenadorFinalSecundario _pcSecundarioA;
    [SerializeField]
    OrdenadorFinalSecundario _pcSecundarioB;
    [SerializeField]
    OrdenadorFinalSecundario _pcSecundarioC;

    [SerializeField]
    DepositoZonaFinal _depositoA;
    [SerializeField]
    DepositoZonaFinal _depositoB;
    [SerializeField]
    DepositoZonaFinal _depositoC;
    [SerializeField]
    GrupoTrampasElectricas _grupoElecA;
    [SerializeField]
    GrupoTrampasElectricas _grupoElecB;
    [SerializeField]
    GrupoTrampasElectricas _grupoElecC;



    private bool _interaccionando;

    private static string MENSAJE_VACIO = "";
    private static string MENSAJE_START = "START";


    void Start(){
        _textoNumero.text = MENSAJE_START;
        _textoAccion.text = MENSAJE_VACIO;

        _goCanvasTextoPulsador.SetActive(true);
        _goPensando.SetActive(false);
        _goLuz.SetActive(true);


        _pcSecundarioA.SetEnumPC(EnumPCFinalZona3.A, this);
        _pcSecundarioB.SetEnumPC(EnumPCFinalZona3.B, this);
        _pcSecundarioC.SetEnumPC(EnumPCFinalZona3.C, this);
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


    private void OnCollisionEnter(Collision other){
        OnEnter(other.collider);
    }

    private void OnCollisionStay(Collision other) {
        OnStay(other.collider);
    }

    private void OnCollisionExit(Collision other){
        OnExit(other.collider);
    }

    public void ComenzarInteraccion(){
        _interaccionando = true;
        _goCanvasTextoPulsador.SetActive(true);
    }

    public void PausarInteraccion(){
        _interaccionando = false;
    }

    public void FinalizarInteraccion(){
        _interaccionando = false;
    }


 
    private void OnEnter(Collider other){
        if(other.gameObject.CompareTag(GameConstants.TAG_PLAYER)){

        }
    }

    private void OnStay(Collider other) {
        if(other.gameObject.CompareTag(GameConstants.TAG_PLAYER)){

        }
    }

    private void OnExit(Collider other){
        if(other.gameObject.CompareTag(GameConstants.TAG_PLAYER)){

            FinalizarInteraccion();
        }
    }



    public Transform GetTransform(){
        return _goPosicionarMano.transform;
    }

    public void PulsadoSecundario(EnumPCFinalZona3 pc){
        
    }
}

public enum EnumPCFinalZona3{A,B,C};

public enum EnumEstadoPCFinalZona3{E1,E2,E3,E4,E5,E6,E7,E8,E9};