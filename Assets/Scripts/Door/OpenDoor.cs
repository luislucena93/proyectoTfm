using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{

    [SerializeField]
    Vector3 _posicionAbierta;


    [SerializeField]
    Vector3 _posicionCerrada;

    [Range(0.01f, 10f)]
    [SerializeField]
    float _velicidadApertura = 2;

    [SerializeField]
    bool _abriendo = false;

    [SerializeField]
    bool _cerrando = false;

    float _progresoLerp = 1;

    [SerializeField]
    bool empiezaCerrada = true;

    private void Awake()
    {
        if (empiezaCerrada)
        {
            _progresoLerp = 1;
            //this.transform.localPosition = _posicionCerrada;
        }
        else
        {
            _progresoLerp = 0;
            this.transform.localPosition = _posicionAbierta;
        }

    }

    // Start is called before the first frame update
    void Start()
    {

    }

    void Update()
    {
        if (_abriendo)
        {
            _progresoLerp -= _velicidadApertura * Time.deltaTime;
            this.transform.localPosition = Vector3.Lerp(_posicionAbierta, _posicionCerrada, _progresoLerp);
            if (_progresoLerp < 0)
            {
                _abriendo = false;
            }
            Debug.Log("actual " + this.transform.localPosition);
        }


        if (_cerrando)
        {
            _progresoLerp += _velicidadApertura * Time.deltaTime;
            this.transform.localPosition = Vector3.Lerp(_posicionAbierta, _posicionCerrada, _progresoLerp);
            if (_progresoLerp > 1)
            {
                _cerrando = false;
            }
            Debug.Log("actual " + this.transform.localPosition);
        }
    }

    public void moverPuerta()
    {
        if(this.transform.localPosition.y<-3)
        {
            _cerrando = true;
        }
        else
        {
            _abriendo = true;
        }
    }

}
