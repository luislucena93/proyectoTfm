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
    private static string MENSAJE_LISTO = "¿Listos?";
    private static string MENSAJE_REPETIMOS = "¿Repetimos?";
    private static string MENSAJE_FASE2 = "Fase 2";
    private static string MENSAJE_FASE3 = "Fase 3";
    private static string MENSAJE_ENHORABUENA = "ENHORABUENA";

    bool _mostrarIndicarInteraccion;
    bool _activo;

    EnumPCFinalZona3 _pcPulsarActual;

    Coroutine _corutinaActual;

    [SerializeField]
    [Range(3,9)]
    int _pcsEstado7 = 3;

    int _pcsEstado7Actual;

    [SerializeField]
    [Range(3,9)]
    int _pcsEstado12 = 3;

    [SerializeField]
    GameObject _goCristalCamara;

    IPuerta _iPuertaCristal;

    [SerializeField]
    JoyaFinalNivel2 _joya;

    GeneradorDialogosSalaNivel2 _generadorDialogos;

    void Start(){
        _generadorDialogos = GetComponent<GeneradorDialogosSalaNivel2>();

        _pcSecundarioA.SetEnumPC(EnumPCFinalZona3.A, this);
        _pcSecundarioB.SetEnumPC(EnumPCFinalZona3.B, this);
        _pcSecundarioC.SetEnumPC(EnumPCFinalZona3.C, this);

        _estadoFase = EnumEstadoPCFinalZona3.E00;

        _iPuertaCristal = _goCristalCamara.GetComponent<IPuerta>();

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
                MostrarListo();
                StartCoroutine(CorutinaFase01());
            break;
            case EnumEstadoPCFinalZona3.E02:
                DesactivarPC();
                _pcPulsarActual = EnumPCFinalZona3.A;
                _pcSecundarioA.ActivarPC(_duracionFase1.ToString());
                _corutinaActual = StartCoroutine(CorutinaFase02());
            break;
            case EnumEstadoPCFinalZona3.E03:
                _generadorDialogos.GenerarDialogo(_estadoFase);
                MostrarRepetimos();
            break;
            case EnumEstadoPCFinalZona3.E04:
                _generadorDialogos.GenerarDialogo(_estadoFase);
                MostrarEnhorabuena();
                _depositoA.CambioVerde();
                StartCoroutine(CorutinaFase04());
            break;
            case EnumEstadoPCFinalZona3.E05:
                _textoAccion.text = MENSAJE_FASE2;
                _goPensando.SetActive(false);
                _mostrarIndicarInteraccion = true;
            break;
            case EnumEstadoPCFinalZona3.E06:
                MostrarListo();
                StartCoroutine(CorutinaFase06());
            break;
            case EnumEstadoPCFinalZona3.E07:
                DesactivarPC();
                _pcPulsarActual = EnumPCFinalZona3.B;
                _pcsEstado7Actual = _pcsEstado7 - 1;
                DesactivarPC();
                _pcSecundarioB.ActivarPC(_duracionFase2.ToString());
                _corutinaActual = StartCoroutine(CorutinaFase07());
            break;
            case EnumEstadoPCFinalZona3.E08:
                _generadorDialogos.GenerarDialogo(_estadoFase);
                MostrarRepetimos();
            break;
            case EnumEstadoPCFinalZona3.E09:
                _generadorDialogos.GenerarDialogo(_estadoFase);
                MostrarEnhorabuena();
                _depositoB.CambioVerde();
                StartCoroutine(CorutinaFase09());
            break;
            case EnumEstadoPCFinalZona3.E10:
                _textoAccion.text = MENSAJE_FASE3;
                _goPensando.SetActive(false);
                _mostrarIndicarInteraccion = true;
            break;
            case EnumEstadoPCFinalZona3.E11:
                MostrarListo();
                StartCoroutine(CorutinaFase11());
            break;
            case EnumEstadoPCFinalZona3.E12:
                _pcPulsarActual = EnumPCFinalZona3.B;
                _grupoElecB.Encender(true);
                _pcsEstado7Actual = _pcsEstado12 - 1;
                DesactivarPC();
                _pcSecundarioB.ActivarPC(_duracionFase2.ToString());
                _corutinaActual = StartCoroutine(CorutinaFase12());
            break;
            case EnumEstadoPCFinalZona3.E13:
                _generadorDialogos.GenerarDialogo(_estadoFase);
                MostrarRepetimos();
            break;
            case EnumEstadoPCFinalZona3.E14:
                _generadorDialogos.GenerarDialogo(_estadoFase);
                MostrarEnhorabuena();
                _depositoC.CambioVerde();
                StartCoroutine(CorutinaFase14());
            break;
            case EnumEstadoPCFinalZona3.E15:
                DesactivarPC();
                _iPuertaCristal.Abrir();
                _joya.Activar();
                StartCoroutine(CorutinaFase15());
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
                LogicaPulsadoresFase07_12(EnumPCFinalZona3.PRINCIPAL,false);
            break;
            case EnumEstadoPCFinalZona3.E08:
                _estadoFase = EnumEstadoPCFinalZona3.E06;
                EntroEnNuevoEstado();
            break;
            case EnumEstadoPCFinalZona3.E09:

            break;
            case EnumEstadoPCFinalZona3.E10:
                _estadoFase = EnumEstadoPCFinalZona3.E11;
                EntroEnNuevoEstado();
            break;
            case EnumEstadoPCFinalZona3.E11:
            
            break;
            case EnumEstadoPCFinalZona3.E12:
                LogicaPulsadoresFase07_12(EnumPCFinalZona3.PRINCIPAL,false);
            break;
            case EnumEstadoPCFinalZona3.E13:
                _estadoFase = EnumEstadoPCFinalZona3.E11;
                EntroEnNuevoEstado();
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
                LogicaPulsadoresFase07_12(pc,false);
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
                LogicaPulsadoresFase07_12(pc,true);
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
        _grupoElecA.Encender(false);
        _grupoElecB.Encender(false);
        _grupoElecC.Encender(false);
        _estadoFase = EnumEstadoPCFinalZona3.E08;
        EntroEnNuevoEstado();
    }

    private void LogicaPulsadoresFase07_12(EnumPCFinalZona3 pulsado, bool activarTrampas){
        if(pulsado == _pcPulsarActual){
            _pcsEstado7Actual--;
            if(_pcsEstado7Actual > 0){
                int s = Random.Range(0,10)%2;
                EnumPCFinalZona3 pcSiguiente = EnumPCFinalZona3.PRINCIPAL;
                switch(_pcPulsarActual){
                    case EnumPCFinalZona3.PRINCIPAL:
                        pcSiguiente = s==1?EnumPCFinalZona3.A:EnumPCFinalZona3.C;
                        DesactivarPC();
                    break;
                    case EnumPCFinalZona3.A:
                        pcSiguiente = s==1?EnumPCFinalZona3.PRINCIPAL:EnumPCFinalZona3.B;
                        _pcSecundarioA.DesactivarPC();
                        _grupoElecA.Encender(false);
                    break;
                    case EnumPCFinalZona3.B:
                        pcSiguiente = s==1?EnumPCFinalZona3.A:EnumPCFinalZona3.C;
                        _pcSecundarioB.DesactivarPC();
                        _grupoElecB.Encender(false);
                    break;
                    case EnumPCFinalZona3.C:
                        pcSiguiente = s==1?EnumPCFinalZona3.PRINCIPAL:EnumPCFinalZona3.B;
                        _pcSecundarioC.DesactivarPC();
                        _grupoElecC.Encender(false);
                    break;
                }
                _pcPulsarActual = pcSiguiente;
                switch(_pcPulsarActual){
                    case EnumPCFinalZona3.PRINCIPAL:
                        ActivarPC(_duracionFaseActual.ToString());
                    break;
                    case EnumPCFinalZona3.A:
                        _pcSecundarioA.ActivarPC(_duracionFaseActual.ToString());
                        _grupoElecA.Encender(activarTrampas);
                    break;
                    case EnumPCFinalZona3.B:
                        _pcSecundarioB.ActivarPC(_duracionFaseActual.ToString());
                        _grupoElecB.Encender(activarTrampas);
                    break;
                    case EnumPCFinalZona3.C:
                        _pcSecundarioC.ActivarPC(_duracionFaseActual.ToString());
                        _grupoElecC.Encender(activarTrampas);
                    break;
                }
            }   else{
                StopCoroutine(_corutinaActual);
                _pcSecundarioA.DesactivarPC();
                _pcSecundarioB.DesactivarPC();
                _pcSecundarioC.DesactivarPC();
                _grupoElecA.Encender(false);
                _grupoElecB.Encender(false);
                _grupoElecC.Encender(false);
                ActivarPC(MENSAJE_VACIO);
                _estadoFase = activarTrampas?EnumEstadoPCFinalZona3.E14:EnumEstadoPCFinalZona3.E09;
                EntroEnNuevoEstado();
            }
        }
    }

    IEnumerator CorutinaFase09(){
        yield return new WaitForSeconds(3);
        _estadoFase = EnumEstadoPCFinalZona3.E10;
        EntroEnNuevoEstado();
    }


    IEnumerator CorutinaFase11(){
        yield return new WaitForSeconds(3);
        _estadoFase = EnumEstadoPCFinalZona3.E12;
        EntroEnNuevoEstado();
    }
    
    IEnumerator CorutinaFase12(){
        _duracionFaseActual = _duracionFase3;
        ActualizarTiempoPcActual();
        while(_duracionFaseActual > 0){
            yield return new WaitForSeconds(1);
            _duracionFaseActual--;
            ActualizarTiempoPcActual();
        }
        _pcSecundarioA.DesactivarPC();
        _pcSecundarioB.DesactivarPC();
        _pcSecundarioC.DesactivarPC();
        _grupoElecA.Encender(false);
        _grupoElecB.Encender(false);
        _grupoElecC.Encender(false);
        _estadoFase = EnumEstadoPCFinalZona3.E13;
        EntroEnNuevoEstado();
    }

    IEnumerator CorutinaFase14(){
        yield return new WaitForSeconds(3);
        _estadoFase = EnumEstadoPCFinalZona3.E15;
        EntroEnNuevoEstado();
    }

    IEnumerator CorutinaFase15(){
        GameObject[] goLuces = GameObject.FindGameObjectsWithTag(Tags.TAG_LIGHT);
        List<Light> luces = new List<Light>();
        List<float> intensidadOriginal = new List<float>();
        List<float> intensidadFinal = new List<float>();
        if(goLuces != null && goLuces.Length > 0){
            for(int i = 0; i < goLuces.Length; i++){
                Light l = goLuces[i].GetComponent<Light>();
                if(l != null){
                    luces.Add(l);
                    intensidadOriginal.Add(l.intensity);
                    intensidadFinal.Add(l.intensity * 0.5f);
                }
            }
            int x = 0;
            float p = 0;
            while(x < 20){
                p = x * 0.05f;
                for(int i = 0; i < luces.Count; i++){
                    luces[i].intensity = Mathf.Lerp(intensidadOriginal[i],intensidadFinal[i],p);

                }
                yield return new WaitForEndOfFrame();
                x++;
            }

        }   else{
            yield return new WaitForEndOfFrame();
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
    

    private void MostrarEnhorabuena(){
        _textoAccion.text = MENSAJE_ENHORABUENA;
        _textoNumero.text = MENSAJE_VACIO;
        _goPensando.SetActive(true);
        _goIndicarInteraccion.SetActive(false);
        _activo = true;
        _mostrarIndicarInteraccion = false;
    }

    private void MostrarRepetimos(){
         _goCanvasTextoPulsador.SetActive(true);
        _textoAccion.text = MENSAJE_REPETIMOS;
        _textoNumero.text = MENSAJE_VACIO;
        _goLuz.SetActive(true);
        _goPensando.SetActive(false);
        _goIndicarInteraccion.SetActive(false);
        _activo = true;
        _mostrarIndicarInteraccion = true;
    }

    private void MostrarListo(){
         _textoAccion.text = MENSAJE_LISTO;
        _mostrarIndicarInteraccion = false;
        _goIndicarInteraccion.SetActive(false);
        _goPensando.SetActive(true);
    }
}

public enum EnumPCFinalZona3{PRINCIPAL,A,B,C};

public enum EnumEstadoPCFinalZona3{E00, E01,E02,E03,E04,E05,E06,E07,E08,E09, E10, E11, E12, E13, E14, E15};