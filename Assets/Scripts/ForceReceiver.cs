using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceReceiver : MonoBehaviour
{
    private float verticalVelocity;

    [field: SerializeField] private CharacterController Controller;

    public Vector3 Movement => Vector3.up * verticalVelocity;

    private void Update()
    {
        if(verticalVelocity < 0f && Controller.isGrounded) 
        {
            Debug.Log("Sin gravedad");
            verticalVelocity = Physics.gravity.y * Time.deltaTime;
        }
        else 
        {
            Debug.Log("Aplicar gravedad" + verticalVelocity);

            verticalVelocity += Physics.gravity.y * Time.deltaTime;
        }
    }

}
