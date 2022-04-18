using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public void LoadSceneByID(int id) 
    {
        SceneManager.LoadScene(id);
    }

    public void ExitGame() 
    {
        Application.Quit();
    }
}
