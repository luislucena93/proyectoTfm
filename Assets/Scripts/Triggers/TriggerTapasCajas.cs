using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTapasCajas : MonoBehaviour
{
    [SerializeField]
    GameObject _goLogicaCaja;

    IPushable _iPushable;
    private void Start() {
        _iPushable = (IPushable) _goLogicaCaja.GetComponent(typeof(IPushable));
    }

    public void JugadorEnTapa(bool enTapa){
        _iPushable.SetBloquearMoviento(enTapa);
    }
}
