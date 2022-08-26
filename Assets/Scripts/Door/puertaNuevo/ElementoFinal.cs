using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementoFinal : MonoBehaviour
{
    [SerializeField]
    GameObject _goTextoFinal;

    [SerializeField]
    Light _luz;

    [SerializeField]
    GameObject _goBolaFinal;

    [SerializeField]
    [Range(0.1f, 30)]
    float _velocidadGiro = 4;




    [SerializeField]
    [Range(0.1f, 30)]
    float _atenuacionLuz=1;

    float _intensidadOrigen = 0;

    [SerializeField]
    [Range(0.1f, 30)]
    float _velocidadLuz;

    bool _recogida = false;


    [SerializeField]
    [Range(0.1f, 30)]
    float _valorLuzRecogida = 8;


    // Start is called before the first frame update
    void Start()
    {
        _goTextoFinal.SetActive(false);

        _intensidadOrigen =_luz.intensity;
    }

    // Update is called once per frame
    void Update()
    {
        if(!_recogida){
            _luz.intensity =  _intensidadOrigen + Mathf.Sin(Time.time*_velocidadLuz) * _atenuacionLuz;
            _goBolaFinal.transform.Rotate(Vector3.up*_velocidadGiro*Time.deltaTime, Space.Self);  
        }
    }


    private void OnTriggerEnter(Collider other) {
        
        if(other.gameObject.CompareTag(GameConstants.TAG_PLAYER)){
            _goBolaFinal.SetActive(false);
            _goTextoFinal.SetActive(true);
            _recogida = true;
            _luz.intensity = _valorLuzRecogida;

      }

    }


}
