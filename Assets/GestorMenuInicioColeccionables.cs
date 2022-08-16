using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestorMenuInicioColeccionables : MonoBehaviour
{
    [SerializeField]
    GameObject _goCamaraMenuInicio;
    [SerializeField]
    GameObject _goCamaraColeccionables;

    [SerializeField]
    GestorColeccionables _gestorColeccionables;
    
    public void AbrirColeccionables(){
        _goCamaraColeccionables.SetActive(true);
        _goCamaraMenuInicio.SetActive(false);
        _gestorColeccionables.AbrirColeccionables();
    }

    public void CerrarColeccionables(){
        _goCamaraColeccionables.SetActive(false);
        _goCamaraMenuInicio.SetActive(true);
        _gestorColeccionables.CerrarColeccionables();
    }
}
