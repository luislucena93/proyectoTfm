using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestorSalaRoja : MonoBehaviour
{
    [SerializeField]
    List<GameObject> _listaExclamaciones;

    bool _exclamacionesActivos;

    [SerializeField]
    [Range(0.2f,10)]
    float _tiempoParpadeo = 2;
    void Start()
    {
        StartCoroutine(Corutina());
    }


    IEnumerator Corutina(){
        while(true){
            _exclamacionesActivos = !_exclamacionesActivos;
            foreach(GameObject go in _listaExclamaciones){
                go.SetActive(_exclamacionesActivos);
            }
            yield return new WaitForSeconds(_tiempoParpadeo);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
