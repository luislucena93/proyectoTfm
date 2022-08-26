using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pushable : MonoBehaviour
{
    public GameObject messageCanvas;
    // Start is called before the first frame update
    void Start()
    {
        messageCanvas.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag(GameConstants.TAG_PLAYER)){
            messageCanvas.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag(GameConstants.TAG_PLAYER)){
            messageCanvas.SetActive(false);
        }
    }
}
