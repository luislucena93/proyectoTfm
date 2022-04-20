using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Selected : MonoBehaviour
{

    private PlayerInput playerInput;
    private InputAction interaccionarAction;

    public float distancia = 15f;

    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        interaccionarAction = playerInput.actions["Interact"];
    }

    // Update is called once per frame
    void Update()
    {

        RaycastHit hit;

        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, distancia))
        {
            if(hit.collider.tag == "Door")
            {
                Debug.Log("contactando con puerta");
                if (interaccionarAction.triggered)
                {
                    hit.collider.transform.GetComponent<OpenDoor>().moverPuerta();
                    Debug.Log("Interaccionando con puerta");
                }
            }
        }
        
    }
}
