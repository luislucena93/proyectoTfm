using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepositoZonaFinal : MonoBehaviour
{
    Renderer _renderer;
    Material _material;
    [SerializeField]
    Texture _texturaVerde;

    //float temp = 2;
    void Start()
    {
        _renderer = GetComponent<Renderer>();
        _material = _renderer.material;
    }

    // Update is called once per frame
    void Update()
    {
        /*if(temp>0){
            temp-=Time.deltaTime;
            if(temp <= 0){
                CambioVerde();
            }
        }*/
    }

    public void CambioVerde(){
        _material.SetTexture("_MainTex",_texturaVerde);
    }
}
