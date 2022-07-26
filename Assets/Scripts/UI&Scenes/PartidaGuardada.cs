using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public class PartidaGuardada{
    public int puntosVidaJ1;
    public int puntosVidaJ2;

    public List<TipoLlaveEnum> listaLlavesJ1 = new List<TipoLlaveEnum> ();
    public List<TipoLlaveEnum> listaLlavesJ2 = new List<TipoLlaveEnum> ();

    public string nivelActual;
}