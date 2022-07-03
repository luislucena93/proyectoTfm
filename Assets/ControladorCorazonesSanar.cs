using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorCorazonesSanar : MonoBehaviour
{
    [SerializeField]
    ParticleSystem _sistemasParticulas;


    private void Start() {
        _sistemasParticulas.Clear();
    }
    private void OnEnable() {
        _sistemasParticulas.Play();
    }

}
