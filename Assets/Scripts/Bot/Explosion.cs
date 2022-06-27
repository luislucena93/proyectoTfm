using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField]
    ParticleSystem[] _sistemasParticulas;

    [Range(0.1f,5)]
    [SerializeField]
    float _tiempoDesactivarTotal = 1;

    float _tiempoDesactivar = 1;
    // Start is called before the first frame update


    private void Start() {
        DesactivarTodo();
    }
    public void Activar() {
        _tiempoDesactivar = _tiempoDesactivarTotal;
        if(_sistemasParticulas != null){
            for(int i = 0; i < _sistemasParticulas.Length; i++){
                _sistemasParticulas[i].gameObject.SetActive(true);
                _sistemasParticulas[i].Clear();
                _sistemasParticulas[i].Play();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        _tiempoDesactivar-=Time.deltaTime;
        if(_tiempoDesactivar<0){
            DesactivarTodo();
            this.gameObject.SetActive(false);
        }
        
    }

    private void DesactivarTodo(){
        if(_sistemasParticulas != null){
            for(int i = 0; i < _sistemasParticulas.Length; i++){
                if(_sistemasParticulas[i].gameObject!=this.gameObject)
                    _sistemasParticulas[i].gameObject.SetActive(false);

            
            }
            this.gameObject.SetActive(false);
        }
    }

}
