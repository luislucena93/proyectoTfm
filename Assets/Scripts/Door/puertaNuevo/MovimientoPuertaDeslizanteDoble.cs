using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoPuertaDeslizanteDoble : MonoBehaviour, IMovimientoPuerta
{
    [SerializeField]
    GameObject _hojaA;

    IMovimientoPuerta _iMovPuertaA;

    [SerializeField]
    GameObject _hojaB;

    IMovimientoPuerta _iMovPuertaB;

    private void Awake() {
        _iMovPuertaA = _hojaA.GetComponent<IMovimientoPuerta>();
        _iMovPuertaB = _hojaB.GetComponent<IMovimientoPuerta>();
    }

    public void MovimientoAbrir(){
        _iMovPuertaA.MovimientoAbrir();
        _iMovPuertaB.MovimientoAbrir();
    }

    public void MovimientoCerrar(){
        _iMovPuertaA.MovimientoCerrar();
        _iMovPuertaB.MovimientoCerrar();
    }

    public bool IsAbierta(){
        return _iMovPuertaA.IsAbierta() &&  IsAbierta();
    }
}
