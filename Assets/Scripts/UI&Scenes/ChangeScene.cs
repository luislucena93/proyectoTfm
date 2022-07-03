using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    [field: SerializeField]
    private Animator _transiciones;

    [field: SerializeField]
    public int nextScene = 0;

    private void OnTriggerEnter(Collider other) 
    {
        if (other.tag == "Player") 
        {
            StartCoroutine(NextLevel());
        }
    }

    IEnumerator NextLevel() 
    {
        _transiciones.SetTrigger("NextScene");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(nextScene);
    }
}
