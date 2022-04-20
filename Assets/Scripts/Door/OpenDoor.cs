using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    [SerializeField]
    Vector3 vectorTranslacionCerrada;

    Vector3 _posicionAbierta;
    Vector3 _posicionCerrada;

    [Range(0.01f, 10f)]
    [SerializeField]
    float _velicidadApertura = 2;

    bool _abriendo = false;

    bool _cerrando = false;

    float _progresoLerp = 1;

    [SerializeField]
    bool empiezaCerrada = true;

    private void Awake()
    {
        if (empiezaCerrada)
        {
            _progresoLerp = 1;
            _posicionAbierta = this.transform.localPosition - vectorTranslacionCerrada;
            _posicionCerrada = this.transform.localPosition;
        }
        else
        {
            _progresoLerp = 0;
            _posicionAbierta = this.transform.localPosition;
            _posicionCerrada = this.transform.localPosition + vectorTranslacionCerrada;
        }

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
            //Debug.Log("actual "+this.transform.localPosition);
        }


        if (_cerrando)
        {
            _progresoLerp += _velicidadApertura * Time.deltaTime;
            this.transform.localPosition = Vector3.Lerp(_posicionAbierta, _posicionCerrada, _progresoLerp);
            if (_progresoLerp > 1)
            {
                _cerrando = false;
            }
            //Debug.Log("actual "+this.transform.localPosition);
        }
    }


    public void Abrir()
    {
        _abriendo = true;
        _cerrando = false;
    }

    public void Cerrar()
    {
        _abriendo = false;
        _cerrando = true;
    }

}
