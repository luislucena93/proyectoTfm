using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;


public class MenuController : MonoBehaviour
{
    public Animator menu, transitions;
    public GameObject InitialButton;

    public void OpenCloseMenu() {

        gameObject.SetActive(true);
        if (menu.GetBool("menuIsOpen") == false) {
            menu.SetBool("menuIsOpen", true);
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(InitialButton);
            //Time.timeScale = 0f;
        }
        else {
            menu.SetBool("menuIsOpen", false);
            //Time.timeScale = 1f;
        }
    }
    public void Continue() {
        OpenCloseMenu();
    }

    public void ExitGame() {
        Application.Quit();
    }

    public void ReloadScene() {
        OpenCloseMenu();
        StartCoroutine(NextLevel());
    }

    IEnumerator NextLevel() {
        transitions.SetTrigger("NextScene");
        yield return new WaitForSeconds(1);
        int sceneID = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(sceneID);
    }
}
