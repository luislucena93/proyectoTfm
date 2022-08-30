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
    GameObject _goIndicarInteraccion;

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

    EnumEstadoPCFinalZona3  _estadoFase = EnumEstadoPCFinalZona3.E00;

    [SerializeField]
    [Range(10,99)]
    int _duracionFase1 = 30;

    [SerializeField]
    [Range(10,99)]
    int _duracionFase2 = 40;

    [SerializeField]
    [Range(10,99)]
    int _duracionFase3 = 70;

    int _duracionFaseActual;

    private bool _interaccionando;

    private static string MENSAJE_VACIO = "";
    private static string MENSAJE_BIENVENIDA = "Bienvenidos";
    private static string MENSAJE_LISTO = "Listos?";
    private static string MENSAJE_REPETIMOS = "Repetimos?";
    private static string MENSAJE_FASE2 = "Fase 2";
    private static string MENSAJE_FASE3 = "Fase 3";
    private static string MENSAJE_ENHORABUENA = "ENHORABUENA";

    bool _mostrarIndicarInteraccion;
    bool _activo;

    EnumPCFinalZona3 _pcPulsarActual;

    Coroutine _corutinaActual;

    void Start(){
        _pcSecundarioA.SetEnumPC(EnumPCFinalZona3.A, this);
        _pcSecundarioB.SetEnumPC(EnumPCFinalZona3.B, this);
        _pcSecundarioC.SetEnumPC(EnumPCFinalZona3.C, this);

        _estadoFase = EnumEstadoPCFinalZona3.E04;

        StartCoroutine(InicioDelayed());
    }

    IEnumerator InicioDelayed(){
		yield return new WaitForEndOfFrame();
        EntroEnNuevoEstado();
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
        PulsadoPrincipal();
    }

    public void PausarInteraccion(){
        _interaccionando = false;
    }

    public void FinalizarInteraccion(){
        _interaccionando = false;
    }


 
    private void OnEnter(Collider other){
        if(_activo && _mostrarIndicarInteraccion && other.gameObject.CompareTag(GameConstants.TAG_PLAYER)){
            _goIndicarInteraccion.SetActive(true);
        }
    }

    private void OnStay(Collider other) {
        if(_activo && _mostrarIndicarInteraccion && !_interaccionando && other.gameObject.CompareTag(GameConstants.TAG_PLAYER)){
            _goIndicarInteraccion.SetActive(true);
        }
    }

    private void OnExit(Collider other){
        if(_activo && other.gameObject.CompareTag(GameConstants.TAG_PLAYER)){
            _goIndicarInteraccion.SetActive(false);
        }
    }



    public Transform GetTransform(){
        return _goPosicionarMano.transform;
    }

    private void EntroEnNuevoEstado(){
        switch(_estadoFase){
            case EnumEstadoPCFinalZona3.E00:
                _pcSecundarioA.DesactivarPC();
                _pcSecundarioB.DesactivarPC();
                _pcSecundarioC.DesactivarPC();
                _textoAccion.text = MENSAJE_BIENVENIDA;
                _textoNumero.text = MENSAJE_VACIO;
                _goPensando.SetActive(false);
                _goIndicarInteraccion.SetActive(false);
                _activo = true;
                _mostrarIndicarInteraccion = true;
            break;
            case EnumEstadoPCFinalZona3.E01:
                _textoAccion.text = MENSAJE_LISTO;
                _mostrarIndicarInteraccion = false;
                _goIndicarInteraccion.SetActive(false);
                _goPensando.SetActive(true);
                StartCoroutine(CorutinaFase01());
            break;
            case EnumEstadoPCFinalZona3.E02:
                DesactivarPC();
                _pcPulsarActual = EnumPCFinalZona3.A;
                _pcSecundarioA.ActivarPC(_duracionFase1.ToString());
                _corutinaActual = StartCoroutine(CorutinaFase02());
            break;
            case EnumEstadoPCFinalZona3.E03:
                _goCanvasTextoPulsador.SetActive(true);
                _textoAccion.text = MENSAJE_REPETIMOS;
                _textoNumero.text = MENSAJE_VACIO;
                _goLuz.SetActive(true);
                _goPensando.SetActive(false);
                _goIndicarInteraccion.SetActive(false);
                _activo = true;
                _mostrarIndicarInteraccion = true;
            break;
            case EnumEstadoPCFinalZona3.E04:
                _textoAccion.text = MENSAJE_ENHORABUENA;
                _textoNumero.text = MENSAJE_VACIO;
                _goPensando.SetActive(true);
                _goIndicarInteraccion.SetActive(false);
                _activo = true;
                _mostrarIndicarInteraccion = false;
                StartCoroutine(CorutinaFase04());
            break;
            case EnumEstadoPCFinalZona3.E05:
                _textoAccion.text = MENSAJE_FASE2;
                _goPensando.SetActive(false);
                _mostrarIndicarInteraccion = true;
            break;
            case EnumEstadoPCFinalZona3.E06:
                _textoAccion.text = MENSAJE_LISTO;
                _mostrarIndicarInteraccion = false;
                _goIndicarInteraccion.SetActive(false);
                _goPensando.SetActive(true);
                StartCoroutine(CorutinaFase06());
            break;
            case EnumEstadoPCFinalZona3.E07:
                _textoNumero.text = MENSAJE_VACIO;
                _goPensando.SetActive(false);
                _goLuz.SetActive(false);
                _goCanvasTextoPulsador.SetActive(false);
                _pcPulsarActual = EnumPCFinalZona3.B;
                _pcSecundarioB.ActivarPC(_duracionFase2.ToString());
                _corutinaActual = StartCoroutine(CorutinaFase07());
            break;
            case EnumEstadoPCFinalZona3.E08:
                _goCanvasTextoPulsador.SetActive(true);
                _textoAccion.text = MENSAJE_REPETIMOS;
                _textoNumero.text = MENSAJE_VACIO;
                _goLuz.SetActive(true);
                _goPensando.SetActive(false);
                _goIndicarInteraccion.SetActive(false);
                _activo = true;
                _mostrarIndicarInteraccion = true;
            break;
            case EnumEstadoPCFinalZona3.E09:

            break;
            case EnumEstadoPCFinalZona3.E10:

            break;
            case EnumEstadoPCFinalZona3.E11:

            break;
            case EnumEstadoPCFinalZona3.E12:

            break;
            case EnumEstadoPCFinalZona3.E13:

            break;
            case EnumEstadoPCFinalZona3.E14:

            break;
            case EnumEstadoPCFinalZona3.E15:

            break;
        }
    } 

    private void PulsadoPrincipal(){
        switch(_estadoFase){
            case EnumEstadoPCFinalZona3.E00:
                _estadoFase = EnumEstadoPCFinalZona3.E01;
                EntroEnNuevoEstado();
            break;
            case EnumEstadoPCFinalZona3.E01:

            break;
            case EnumEstadoPCFinalZona3.E02:
                if (_pcPulsarActual == EnumPCFinalZona3.PRINCIPAL){
                    StopCoroutine(_corutinaActual);
                    _pcSecundarioA.DesactivarPC();
                    _pcSecundarioB.DesactivarPC();
                    _pcSecundarioC.DesactivarPC();
                    _estadoFase = EnumEstadoPCFinalZona3.E04;
                    EntroEnNuevoEstado();
                }
            break;
            case EnumEstadoPCFinalZona3.E03:
                _estadoFase = EnumEstadoPCFinalZona3.E01;
                EntroEnNuevoEstado();
            break;
            case EnumEstadoPCFinalZona3.E04:

            break;
            case EnumEstadoPCFinalZona3.E05:
                _estadoFase = EnumEstadoPCFinalZona3.E06;
                EntroEnNuevoEstado();
            break;
            case EnumEstadoPCFinalZona3.E06:

            break;
            case EnumEstadoPCFinalZona3.E07:

            break;
            case EnumEstadoPCFinalZona3.E08:
                _estadoFase = EnumEstadoPCFinalZona3.E06;
                EntroEnNuevoEstado();
            break;
            case EnumEstadoPCFinalZona3.E09:

            break;
            case EnumEstadoPCFinalZona3.E10:

            break;
            case EnumEstadoPCFinalZona3.E11:

            break;
            case EnumEstadoPCFinalZona3.E12:

            break;
            case EnumEstadoPCFinalZona3.E13:

            break;
            case EnumEstadoPCFinalZona3.E14:

            break;
            case EnumEstadoPCFinalZona3.E15:

            break;
        }
    }

    public void PulsadoSecundario(EnumPCFinalZona3 pc){
        switch(_estadoFase){
            case EnumEstadoPCFinalZona3.E00:

            break;
            case EnumEstadoPCFinalZona3.E01:

            break;
            case EnumEstadoPCFinalZona3.E02:
                if(pc == EnumPCFinalZona3.A){
                    _pcSecundarioB.ActivarPC(_duracionFaseActual.ToString());
                    _pcPulsarActual = EnumPCFinalZona3.B;
                    _pcSecundarioA.DesactivarPC();
                }   else if (pc == EnumPCFinalZona3.B){
                    _pcSecundarioC.ActivarPC(_duracionFaseActual.ToString());
                    _pcPulsarActual = EnumPCFinalZona3.C;
                    _pcSecundarioB.DesactivarPC();
                }   else if (pc == EnumPCFinalZona3.C){
                    ActivarPC(_duracionFaseActual.ToString());
                    _pcPulsarActual = EnumPCFinalZona3.PRINCIPAL;
                    _pcSecundarioC.DesactivarPC();
                }

            break;
            case EnumEstadoPCFinalZona3.E03:

            break;
            case EnumEstadoPCFinalZona3.E04:

            break;
            case EnumEstadoPCFinalZona3.E05:

            break;
            case EnumEstadoPCFinalZona3.E06:

            break;
            case EnumEstadoPCFinalZona3.E07:

            break;
            case EnumEstadoPCFinalZona3.E08:

            break;
            case EnumEstadoPCFinalZona3.E09:

            break;
            case EnumEstadoPCFinalZona3.E10:

            break;
            case EnumEstadoPCFinalZona3.E11:

            break;
            case EnumEstadoPCFinalZona3.E12:

            break;
            case EnumEstadoPCFinalZona3.E13:

            break;
            case EnumEstadoPCFinalZona3.E14:

            break;
            case EnumEstadoPCFinalZona3.E15:

            break;
        }
    }

    IEnumerator CorutinaFase01(){
        yield return new WaitForSeconds(2);
        _estadoFase = EnumEstadoPCFinalZona3.E02;
        EntroEnNuevoEstado();
    }

    IEnumerator CorutinaFase02(){
        _duracionFaseActual = _duracionFase1;
        ActualizarTiempoPcActual();
        while(_duracionFaseActual > 0){
            yield return new WaitForSeconds(1);
            _duracionFaseActual--;
            ActualizarTiempoPcActual();
        }
        _pcSecundarioA.DesactivarPC();
        _pcSecundarioB.DesactivarPC();
        _pcSecundarioC.DesactivarPC();
        _estadoFase = EnumEstadoPCFinalZona3.E03;
        EntroEnNuevoEstado();
    }

    IEnumerator CorutinaFase04(){
        yield return new WaitForSeconds(3);
        _estadoFase = EnumEstadoPCFinalZona3.E05;
        EntroEnNuevoEstado();
    }

    IEnumerator CorutinaFase06(){
        yield return new WaitForSeconds(3);
        _estadoFase = EnumEstadoPCFinalZona3.E07;
        EntroEnNuevoEstado();
    }


    IEnumerator CorutinaFase07(){
        _duracionFaseActual = _duracionFase2;
        ActualizarTiempoPcActual();
        while(_duracionFaseActual > 0){
            yield return new WaitForSeconds(1);
            _duracionFaseActual--;
            ActualizarTiempoPcActual();
        }
        _pcSecundarioA.DesactivarPC();
        _pcSecundarioB.DesactivarPC();
        _pcSecundarioC.DesactivarPC();
        _estadoFase = EnumEstadoPCFinalZona3.E08;
        EntroEnNuevoEstado();
    }

    private void LogicaPulsadoresFase07_12(EnumPCFinalZona3 pulsado, bool activarTrampas){
        if(pulsado == _pcPulsarActual){
            int s = Random.Range(0,1);
            EnumEstadoPCFinalZona3 pcSiguiente = 
            switch(_pcPulsarActual){
                case EnumPCFinalZona3.PRINCIPAL:
                    _pcPulsarActual
                break;
                case EnumPCFinalZona3.A:

                break;
                case EnumPCFinalZona3.B:

                break;
                case EnumPCFinalZona3.C:

                break;
            }
        }
    }




    private void ActualizarTiempoPcActual(){
        switch(_pcPulsarActual){
            case EnumPCFinalZona3.PRINCIPAL:
                _textoNumero.SetText(_duracionFaseActual.ToString());
            break;
            case EnumPCFinalZona3.A:
                _pcSecundarioA.ActualizarTexto(_duracionFaseActual.ToString());
            break;
            case EnumPCFinalZona3.B:
                _pcSecundarioB.ActualizarTexto(_duracionFaseActual.ToString());
            break;
            case EnumPCFinalZona3.C:
                _pcSecundarioC.ActualizarTexto(_duracionFaseActual.ToString()); 
            break;
        }
    }

    private void ActivarPC(string texto){
        _goLuz.SetActive(true);
        _goCanvasTextoPulsador.SetActive(true);
        _textoNumero.text = texto;
        _textoAccion.text = MENSAJE_VACIO;
        _activo = true;
        _mostrarIndicarInteraccion = true;
    }

    private void DesactivarPC(){
        _textoNumero.text = MENSAJE_VACIO;
        _goPensando.SetActive(false);
        _goLuz.SetActive(false);
        _goCanvasTextoPulsador.SetActive(false);
    }
                    
}

public enum EnumPCFinalZona3{PRINCIPAL,A,B,C};

public enum EnumEstadoPCFinalZona3{E00, E01,E02,E03,E04,E05,E06,E07,E08,E09, E10, E11, E12, E13, E14, E15};