using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListenerPrimeraVezMostrarMenu : MonoBehaviour
{
    Canvas _canvas;

    private void OnEnable() {
        _canvas.GetComponent<Canvas>();
    }

    public void MostrarCanvas(){
        _canvas.enabled = true;
    }
}
