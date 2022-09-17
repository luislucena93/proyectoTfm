using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using System;
public class GestorPantallaCarga : MonoBehaviour
{
    [SerializeField]
    Slider _slider;

    [SerializeField]
    Gradient _gradienteColorSlider;


    [SerializeField]
    Image _imagenSlider;

    float _target = 0;

    private void Start() {
        CargaSiguienteEscena();
    }


    private void Update() {
/*
        float porcentaje = Mathf.MoveTowards(_slider.value, _target, 3*Time.deltaTime);
        _slider.value = porcentaje;
        Color color = _gradienteColorSlider.Evaluate(porcentaje);
        _imagenSlider.color = color;
*/
    }

    public void  SiguienteNivel() {
        int sceneID = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(sceneID);
    }

    public async void CargaSiguienteEscena(){
        int sceneID = SceneManager.GetActiveScene().buildIndex + 1;
        var scene = SceneManager.LoadSceneAsync(sceneID);
        scene.allowSceneActivation = false;
        _target = 0.1f;
        ActualizarSlider();
        do{
            await Task.Delay(100);
            _target = scene.progress + 0.1f;
            ActualizarSlider();
        }   while(scene.progress < 0.9f);
        _imagenSlider.gameObject.SetActive(false);
        scene.allowSceneActivation = true;
    }
    void ActualizarSlider(){
        float porcentaje =  _target;
        _slider.value = porcentaje;
        Color color = _gradienteColorSlider.Evaluate(porcentaje);
        _imagenSlider.color = color;
    }
}
