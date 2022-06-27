using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrampaElectrica : MonoBehaviour
{
    [SerializeField]
    GameObject _zonaElectrica;

    [SerializeField]
    bool _comienzaActivo = true;

    void Start()
    {
        _zonaElectrica.SetActive(_comienzaActivo);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
