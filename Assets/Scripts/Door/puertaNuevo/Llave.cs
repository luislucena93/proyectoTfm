using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Llave : MonoBehaviour
{
    

    [Range(0, 100)]
    [SerializeField]
    float _velocidadGiro = 50;


    [SerializeField]
    GameObject _puerta;

    [SerializeField]
    TipoLlaveEnum _tipoLlave = TipoLlaveEnum.Oro;

    bool _activo = true;  

    AudioSource _audioSource; 



    void Start()
    {
     //   _audioSource = GetComponent<AudioSource>(); 
    }

    void Update()
    {
        this.transform.Rotate(Vector3.up*_velocidadGiro*Time.deltaTime, Space.Self);  
    }


    private void OnTriggerEnter(Collider other) {
        
        if(other.gameObject.tag == "Player" && _activo) {
            IPuerta iPuerta = _puerta.GetComponent<IPuerta>();
            if(iPuerta != null){
                iPuerta.Abrir(_tipoLlave); 
            } else{
                Debug.LogError("No se encuentra iPuerta");
            }
            
            //_audioSource.Play();
            _activo = false; 
            Desactivar();
            //Invoke("Desactivar", _audioSource.clip.length); // Desactivamos el GO cuando termine el clip.
        }

    }

    private void Desactivar(){ 
        this.gameObject.SetActive(false); 
    }
}


public enum TipoLlaveEnum {Oro,Plata, Bronce, Titanio, RepararA, RepararB, RepararC, Bloque1, Bloque2};