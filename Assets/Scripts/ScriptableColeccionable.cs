using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "NuevoColeccionable", menuName = "Coleccionable", order = 51)]
public class ScriptableColeccionable : ScriptableObject
{
    [SerializeField]
    private string nombre;

    private GameObject modelo;
}
