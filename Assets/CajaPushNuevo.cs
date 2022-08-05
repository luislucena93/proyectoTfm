using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CajaPushNuevo : MonoBehaviour, IPushable
{
    [SerializeField]
    List<Renderer> _listaRenderers;

    List<Material> _listaMateriales = new List<Material> ();

    List<Color> _listaColoresInicio = new List<Color> ();

    [SerializeField]
    Color _colorDetectado;

    [SerializeField]
    Rigidbody _rbMovible;

    [SerializeField]
    [Range(0,5)]
    float _paddingJugador;

    [SerializeField]
    TipoCajaEnum _tipoCajaEnum;

    bool _cajaBloqueada;

    bool _pushing;


    private void Start() {
        if(_listaRenderers != null && _listaRenderers.Count >0){
            for(int i = 0; i < _listaRenderers.Count; i++){
                _listaMateriales.Add(_listaRenderers[i].material);
                _listaColoresInicio.Add(_listaRenderers[i].material.color);
            }
        }

        //_rb.isKinematic = true;
    }

    public void SetAvisoPushable(bool disponible){
        Debug.Log("SetAviso en Caja "+disponible);
        if(_listaMateriales != null && _listaMateriales.Count >0){
            for(int i = 0; i < _listaMateriales.Count; i++){
                _listaMateriales[i].color = disponible?_colorDetectado:_listaColoresInicio[i];
            }
        }
        if(!disponible){
            //_rb.isKinematic = true;
         //   Debug.Log("Kinematic "+true);
        }
        
    }

    public void SetPushing(bool pushing){
    _pushing = pushing;
        //_rb.isKinematic = !pushing;
//        Debug.Log("Kinematic "+!pushing);
    }

    public bool IsPushing(){
        return _pushing;
    }

    public Rigidbody GetRigidBody(){
        return _rbMovible;;
    }

    public float GetPaddingJugador(){
        return _paddingJugador;
    }


    public TipoCajaEnum GetTipoCajaEnum(){
        return _tipoCajaEnum;
    }

    public void SetBloquearMoviento(bool bloquear){
        Debug.Log("SetBloquearMoviemiento "+bloquear);
        _cajaBloqueada = bloquear;
    }

    public bool GetBloquearMoviento(){
        return _cajaBloqueada;
    }

}
