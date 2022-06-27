using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZonaTrampaElectrica : MonoBehaviour
{

    [SerializeField]
    GameObject _fxElectricidad;

    [SerializeField]
    GameObject _base;
    Material _materialBase;

    [SerializeField]
    Color _colorNeutro;
    [SerializeField]
    Color _colorPisado;

    [SerializeField]
    [Range(0,100)]
    int _danhoInflingidoPorFixedUpdate = 1;

    List<IDanhable> _listaDanhables = new List<IDanhable>();



    [SerializeField]
    bool activo;




    private void OnTriggerEnter(Collider other) {
        CheckAddListaDanhables(other);
    }

    private void OnTriggerStay(Collider other) {
        CheckAddListaDanhables(other);
    }

    private void OnTriggerExit(Collider other) {
    }

    private void OnCollisionEnter(Collision other) {
        CheckAddListaDanhables(other.collider);
    }

    private void OnCollisionStay(Collision other) {
        CheckAddListaDanhables(other.collider);
    }

    private void OnCollisionExit(Collision other) {

    }

    private void CheckAddListaDanhables(Collider other){
        //Debug.Log("other "+other.name);
        IDanhable danhable = (IDanhable) other.gameObject.GetComponent(typeof(IDanhable));
        if(danhable != null && !_listaDanhables.Contains(danhable)){
          //  Debug.Log("other added");
            _listaDanhables.Add(danhable);
        }
    }

    private void FixedUpdate() {
        if(_listaDanhables.Count>0){



            for(int i = 0; i < _listaDanhables.Count; i++){
                IDanhable d = _listaDanhables[i];
                if(d!=null){
                    d.RecibirDanho(_danhoInflingidoPorFixedUpdate);
                }
            }
            _listaDanhables.Clear();
        }
    }
}
