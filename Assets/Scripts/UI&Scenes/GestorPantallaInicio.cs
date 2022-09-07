using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GestorPantallaInicio : MonoBehaviour
{
    [SerializeField]
    GameObject _timeline;

    public List<Dialogue> dialogos;
    private DialogueManager dm;
    private bool wasOpen = false;

    int _indiciceDialogoActual = -1;

    private void Start()
    {
        dm = FindObjectOfType<DialogueManager>();
    }
    public void SiguienteDialogo()
    {
        _indiciceDialogoActual++;
        dm.StartDialogue(dialogos[_indiciceDialogoActual]);
    }

    public void SiguienteFrase(){
        dm.DisplayNextSentence();
    }

    public void  SiguienteNivel() {
        int sceneID = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(sceneID);
    }

    public void ActivarTimeline(){
        Cursor.visible = false;
        _timeline.SetActive(true);
    }

    public void SalirDelJuego(){
        Application.Quit();
    }

}

