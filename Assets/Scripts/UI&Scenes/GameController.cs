using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [field: SerializeField]
    private Animator _transiciones;

    public void LoadSceneByID(int id) 
    {
        StartCoroutine(NextLevel(id)); 
    }

    IEnumerator NextLevel(int id) 
    {
        _transiciones.SetTrigger("NextScene");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(id);
    }
    public void ExitGame() {
        Application.Quit();
    }
}

