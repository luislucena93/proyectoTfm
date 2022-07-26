using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text;

public class PulsadorAbrirCerrarPlantas : MonoBehaviour, IInteraccionable, IListenerAbrir
{
    [SerializeField]
    GameObject _goCanvasTextoPulsador;

    [SerializeField]
    TMP_Text _textoAccion;
    // Start is called before the first frame update

    [SerializeField]
    GameObject _goPuertaIPuerta;

    IPuerta _iPuerta;

    private bool _interaccionando;

    private bool _pulsado;

    

    private static string MENSAJE_PULSA_ABRIR = "Pulsa para Abrir";
    private static string MENSAJE_PULSA_CERRAR = "Pulsa para Cerrar";
    private static string MENSAJE_ABRIENDO = "Abriendo";

    private static string MENSAJE_CERRANDO = "Cerrando";

    private static string MENSAJE_ABIERTO = "Abrierto";

    private static string MENSAJE_CERRADO = "Cerrado";

    private static string MENSAJE_SALTO_LINEA ="\n";

    private StringBuilder _sb = new StringBuilder();

    void Start()
    {
        _textoAccion.text = "";
        _iPuerta = _goPuertaIPuerta.GetComponent<IPuerta>();
        if(_iPuerta == null){
            Debug.Log("IPuerta no encontrada "+this.name);
        }   else{
            _iPuerta.SetIListenerAbrir(this);
        }

        _goCanvasTextoPulsador.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void ComenzarInteraccion(){
        _interaccionando = true;
        _goCanvasTextoPulsador.SetActive(true);

        if(_iPuerta.isAbriendo() || _iPuerta.isAbierta()){
            _textoAccion.text = MENSAJE_CERRANDO;
            _iPuerta.Cerrar();
        }   else if(_iPuerta.isCerrando() || !_iPuerta.isAbierta()){
            _textoAccion.text = MENSAJE_ABRIENDO;
            _iPuerta.Abrir();
        }
    }

    public void PausarInteraccion(){
        _interaccionando = false;
    }

    public void FinalizarInteraccion(){
        _interaccionando = false;
    }



    private void OnTriggerEnter(Collider other) {
        ColliderEnter(other);      
    }

    private void OnTriggerStay(Collider other) {
        ColliderStay(other);  
    }

    private void OnTriggerExit(Collider other) {
        ColliderExit(other);  
    }

 
    private void OnCollisionEnter(Collision other){
        ColliderEnter(other.collider);
    }

    private void OnCollisionStay(Collision other) {
        ColliderStay(other.collider);
    }

    private void OnCollisionExit(Collision other){
        ColliderExit(other.collider);
    }


    private void ColliderEnter(Collider other){
        if(other.gameObject.tag == "Player") {
            _goCanvasTextoPulsador.SetActive(true);
            LogicaMensajesCartelesNoInteraccionando();
        }
    }

    private void ColliderStay(Collider other){
        if(other.gameObject.tag == "Player") {
            if(!_interaccionando){
                LogicaMensajesCartelesNoInteraccionando();
            }

        }
    }

    private void ColliderExit(Collider other){
        if(other.gameObject.tag == "Player") {
            _goCanvasTextoPulsador.SetActive(false);
            _interaccionando = false;
        }
    }


    public Transform GetTransform(){
        return gameObject.transform;
    }


    public void ComienzaAbrir(){
    }
    public void ComienzaCerrar(){
    }
    public void FinalizaAbrir(){
        if(_interaccionando){
            _textoAccion.text = MENSAJE_ABIERTO;
        }   else{
            _sb.Clear();
            _sb.Append(MENSAJE_ABIERTO).Append(MENSAJE_SALTO_LINEA).Append(MENSAJE_PULSA_CERRAR);
            _textoAccion.text = _sb.ToString();
        }

    }
    public void FinalizaCerrar(){
        if(_interaccionando){
            _textoAccion.text = MENSAJE_CERRADO;
        }   else{
            _sb.Clear();
            _sb.Append(MENSAJE_CERRADO).Append(MENSAJE_SALTO_LINEA).Append(MENSAJE_PULSA_ABRIR);
            _textoAccion.text = _sb.ToString();
        }
    }


    private void LogicaMensajesCartelesNoInteraccionando(){
        _sb.Clear();
        if(_iPuerta.isAbriendo()){
            _sb.Append(MENSAJE_ABRIENDO).Append(MENSAJE_SALTO_LINEA).Append(MENSAJE_PULSA_CERRAR);
        }   else if(_iPuerta.isCerrando()){
            _sb.Append(MENSAJE_CERRANDO).Append(MENSAJE_SALTO_LINEA).Append(MENSAJE_PULSA_ABRIR);
        }   else if(_iPuerta.isAbierta()){
            _sb.Append(MENSAJE_PULSA_CERRAR);
        }   else{
            _sb.Append(MENSAJE_PULSA_ABRIR);
        }

        _textoAccion.text = _sb.ToString();
    }
}
