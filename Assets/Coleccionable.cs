using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coleccionable : MonoBehaviour
{
    [SerializeField]
    EnumColeccionable _tipo;

    string _nombre;
    [SerializeField]
    bool _recogido;

    [SerializeField]
    List<Renderer> _renderers;

    [SerializeField]
    Renderer _renderPeana;

    [SerializeField]
    Material _materialDesactivado;

    [SerializeField]
    Material _materialDesactivadoPeana;

    List<Material> _materialesOriginales = new List<Material>();

    Material _materialOriginalPeana;

    private void Start() {
        _nombre = _tipo.ToString();
        for(int i = 0; i < _renderers.Count; i++){
            _materialesOriginales.Add(_renderers[i].material);
        }
        _materialOriginalPeana = _renderPeana.material;
    }

    public string GetNombre(){
        return _nombre;
    }

    public EnumColeccionable GetEnumColeccionable(){
        return _tipo;
    }

    public void SetActivo(bool activo){
        for(int i = 0; i < _renderers.Count; i++){
            _renderers[i].enabled = activo;
        }
        _renderPeana.enabled = activo;
    }

    public void SetRecogido(bool recogido){
        _recogido = recogido;
        for(int i = 0; i < _renderers.Count; i++){
            _renderers[i].material = recogido ? _materialesOriginales[i] : _materialDesactivado;
        }
        _renderPeana.material = recogido ? _materialOriginalPeana : _materialDesactivadoPeana;
    }

    public bool GetRecogido(){
        return _recogido;
    }


}

public enum EnumColeccionable {Max='A',El='B',Ella='C',Soldado='D',Robot='E',Psycho='F',Dino='G',GirlBoss='H',Boss='I'};
