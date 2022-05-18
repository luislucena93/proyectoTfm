using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boxInArea : MonoBehaviour
{
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
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Pushable")
        {
            boxIn = false;
            renderer.material.SetColor("_TintColor", Color.red);
        }
    }

    public bool getBoxInArea()
    {
        return boxIn;

    }
}

