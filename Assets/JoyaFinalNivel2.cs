using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoyaFinalNivel2 : MonoBehaviour
{
    [SerializeField]
    List<GameObject> _luces;

    [SerializeField]
    GameObject _particulas;

    [SerializeField]
    Vector3 _translacion;

    [SerializeField]
    [Range(0.2f,5)]
    float _duracion = 1;


    [SerializeField]
    [Range(0.2f,200)]
    float _velocidadRotacion = 1;

    [SerializeField]
    MenuController _menuController;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update(){
        this.transform.Rotate(Vector3.up*_velocidadRotacion*Time.deltaTime, Space.Self);  
    }

    public void Activar(){
        Debug.Log("Activar");
        for(int i = 0; i < _luces.Count; i++){
            _luces[i].SetActive(true);
        }
        _particulas.SetActive(true);
        StartCoroutine(Corutina());
    }


    IEnumerator Corutina(){
        Vector3 posicionInicial = gameObject.transform.position;
        Vector3 posicionFinal = gameObject.transform.position + _translacion;
        float incremento = 0.01f /_duracion;
        float incrementoTotal = 0;
        float tiempoTotal = 0;
        while (tiempoTotal < _duracion){
            yield return new WaitForSeconds(0.01f);
            gameObject.transform.position = Vector3.Lerp(posicionInicial, posicionFinal, incrementoTotal);
            incrementoTotal += incremento;
            tiempoTotal += 0.01f;
        }    
    }

    private void OnTriggerEnter(Collider other) {
        OnEnter(other);
    }

    private void OnCollisionEnter(Collision other){
        OnEnter(other.collider);
    }

    private void OnEnter(Collider other){
        if(other.gameObject.CompareTag(GameConstants.TAG_PLAYER)){
            _menuController.SiguienteEscena();
        }
    }
}