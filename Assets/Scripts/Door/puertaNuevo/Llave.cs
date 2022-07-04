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


    [SerializeField]
    Light _luz;

    [SerializeField]
    [Range(0.1f, 30)]
    float _atenuacionLuz=1;

    float _intensidadOrigen = 0;

    [SerializeField]
    [Range(0.1f, 30)]
    float _velocidadLuz;

    void Start()
    {        
        _intensidadOrigen =_luz.intensity;
     //   _audioSource = GetComponent<AudioSource>(); 
    }

    void Update()
    {
        this.transform.Rotate(Vector3.up*_velocidadGiro*Time.deltaTime, Space.Self);  
    
        _luz.intensity =  _intensidadOrigen + Mathf.Sin(Time.time*_velocidadLuz) * _atenuacionLuz;
    }


    private void OnTriggerEnter(Collider other) {
        
        if(other.gameObject.tag == "Player" && _activo) {
            IPuerta iPuerta = _puerta.GetComponent<IPuerta>();
            if(iPuerta != null){
                iPuerta.Abrir(_tipoLlave); 
            } else{
                Debug.LogError("No se encuentra iPuerta");
            }
            PlayerStateMachine machine = other.gameObject.GetComponent<PlayerStateMachine>();
            if(machine != null){
                machine.GetHUDJugador().RecogidaTarjeta(_tipoLlave);
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


public enum TipoLlaveEnum {Oro,Verde,Roja,Plata, Bronce, Titanio, RepararA, RepararB, RepararC, Bloque1, Bloque2, Juntos};