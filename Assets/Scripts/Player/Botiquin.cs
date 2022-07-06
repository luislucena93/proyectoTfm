using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Botiquin : MonoBehaviour
{
    [SerializeField]
    [Range (1,5000)]
    int _puntosRecupera;

    bool _consumido;

    [SerializeField]
    GameObject _fuenteCorazones;
    // Start is called before the first frame update

    Renderer _renderer;

    [SerializeField]
    [Range (0.1f,5)]
    float _tiempoEmitiendoCorazones;
    void Start()
    {
        _renderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other) {
        CheckColisionSalud(other);
    }

    private void OnTriggerStay(Collider other) {
        CheckColisionSalud(other);
    }

    private void OnTriggerExit(Collider other) {
    }

    private void OnCollisionEnter(Collision other) {
        CheckColisionSalud(other.collider);
    }

    private void OnCollisionStay(Collision other) {
        CheckColisionSalud(other.collider);
    }

    private void OnCollisionExit(Collision other) {

    }

    void CheckColisionSalud(Collider other){
        if(other.gameObject.CompareTag(Tags.TAG_PLAYER) && !_consumido){
            IRecuperarSalud iSalud = (IRecuperarSalud) other.gameObject.GetComponent(typeof(IRecuperarSalud));
            if(iSalud != null){
                if(iSalud.IsHurt()){
                    iSalud.RecuperarSalud(_puntosRecupera);
                    _consumido = true;
                    _renderer.enabled = false;
                    //this.gameObject.SetActive(false);
                    StartCoroutine(EsperaInicial());
                }
            }
        }
    }

    private     IEnumerator EsperaInicial(){
        _fuenteCorazones.SetActive(true);
        yield return new WaitForSeconds(_tiempoEmitiendoCorazones);
        this.gameObject.SetActive(false);
    }
}
