using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PantallaFinDeJuego : MonoBehaviour
{
    public void FinalizarEscena(){
        SceneManager.LoadScene("Menu 1");
    }
}
