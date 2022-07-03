using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDJugador : MonoBehaviour
{
    [SerializeField]
    Slider _sliderSalud;
    [SerializeField]
    Slider _sliderEscudo;

    [SerializeField]
    GameObject _goTarjetaAmarilla;
    [SerializeField]
    GameObject _goTarjetaVerde;
    [SerializeField]
    GameObject _goTarjetaRoja;

    [SerializeField]
    GameObject _goCaraNormal;

    [SerializeField]
    GameObject _goCaraGris;

    void Start()
    {    
        _goTarjetaAmarilla.SetActive(false);
        _goTarjetaVerde.SetActive(false);
        _goTarjetaRoja.SetActive(false);
        _goCaraGris.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void RecogidaTarjeta(TipoLlaveEnum tipoLlave){
        if(tipoLlave == TipoLlaveEnum.Oro){
            _goTarjetaAmarilla.SetActive(true);
        }   else if(tipoLlave == TipoLlaveEnum.Verde){
            _goTarjetaVerde.SetActive(true);
        }   else if(tipoLlave == TipoLlaveEnum.Roja){
            _goTarjetaRoja.SetActive(true);
        }
    }

    public void SetNivelSalud(float nivelSalud){
        _sliderSalud.value = nivelSalud;
        if(nivelSalud<=0){
            _goCaraNormal.SetActive(false);
            _goCaraGris.SetActive(true); 
        }   else{
            _goCaraNormal.SetActive(true);
            _goCaraGris.SetActive(false); 
        }
    }

    public void SetNivelSaludMaxima(float nivelSaludMaxima){
        _sliderSalud.maxValue = nivelSaludMaxima;
    }

    public void SetNivelEscudo(float nivelEscudo){
        _sliderEscudo.value = nivelEscudo;
    }

    public void SetNivelEscudoMaximo(float nivelEscudoMaximo){
        _sliderEscudo.maxValue = nivelEscudoMaximo;
    }
}
