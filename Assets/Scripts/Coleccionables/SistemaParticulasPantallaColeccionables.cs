using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SistemaParticulasPantallaColeccionables : MonoBehaviour
{
    ParticleSystem _sistemasParticulas;
    private void Awake() {
        Debug.Log("Se ejecuta");
        _sistemasParticulas = GetComponent<ParticleSystem>();
    }
    private void OnEnable() {
        _sistemasParticulas.Play();
    }

    private void OnDisable() {
        _sistemasParticulas.Clear();
    }

    private void Update() {
        if (Time.timeScale < 0.01f)
         {
            _sistemasParticulas.Simulate(Time.unscaledDeltaTime, true, false);
         }
    }
}
