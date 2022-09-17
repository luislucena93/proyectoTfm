using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesactivarGOCambioEscena : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DesactivaPantallaCarga();
    }

    IEnumerator DesactivaPantallaCarga(){
        yield return new WaitForEndOfFrame();
        GameObject go = GameObject.FindGameObjectWithTag(Tags.TAG_PANTALLA_CARGA);
        if(go != null){
            go.SetActive(false);
        }
    }
}
