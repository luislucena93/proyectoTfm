using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boxInArea : MonoBehaviour
{
    [SerializeField]
    GameObject _puerta;

    [SerializeField]
    TipoLlaveEnum _tipoLlave;

    private bool boxIn = false;
    public GameObject light;
    private Renderer renderer;
    // Start is called before the first frame update
    void Start()
    {
        renderer = light.GetComponent<Renderer>();
        renderer.material.SetColor("_TintColor", Color.red);
    }

    // Update is called once per frame
    void Update() 
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Pushable")
        {
            boxIn = true;
            renderer.material.SetColor("_TintColor", Color.green);

            IPuerta iPuerta = _puerta.GetComponent<IPuerta>();
            if(iPuerta != null){
                iPuerta.Abrir(_tipoLlave); 
            } else{
                Debug.LogError("No se encuentra iPuerta");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Pushable")
        {
            boxIn = false;
            renderer.material.SetColor("_TintColor", Color.red);

            IPuerta iPuerta = _puerta.GetComponent<IPuerta>();
            if (iPuerta != null) {
                iPuerta.Cerrar();
            }
            else {
                Debug.LogError("No se encuentra iPuerta");
            }
        }
    }

    public bool getBoxInArea()
    {
        return boxIn;

    }
}

