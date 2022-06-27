using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceReceiver : MonoBehaviour
{
    [field: SerializeField] private CharacterController Controller;

    public bool isGrounded;
    private float gravityValue = -2f;
    private Vector3 moveDirection = Vector3.zero;

   /* public bool IsGrounded() {
        Vector3 targetCenter = Controller.bounds.center;
        //1.8 es la altura del pj, 0.9 sus pies, 0.91 justo por debajo de sus pies
        return Physics.Raycast(targetCenter, Vector3.down, 0.91f);
    }
    */
    private void Update()
    {

//        Debug.Log("A�adir gravedad");
        moveDirection.y += gravityValue * Time.deltaTime;
        Controller.Move(moveDirection * Time.deltaTime);

        /*if (IsGrounded()) {
            //float groundedGravity = -.05f;
            //moveDirection.y = groundedGravity;
            isGrounded = true;
        }
        else {
            float gravity = -9.8f;
            moveDirection.y += gravity;
            isGrounded = false;
        }
        
        Controller.Move(moveDirection * Time.deltaTime);
        */
        /*isGrounded = IsGrounded();

        Debug.Log("isGrounded" + isGrounded);

        if (!isGrounded) 
        {
            Debug.Log("A�adir gravedad");
            moveDirection.y += gravityValue * Time.deltaTime;
            Controller.Move(moveDirection * Time.deltaTime);
        }
        */



        /*
        Debug.Log("Controller.isGrounded " + Controller.isGrounded);
        
        if (verticalVelocity < 0 && Controller.isGrounded) 
        { 
            Debug.Log("Sin gravedad");
            verticalVelocity = 0f;
            //verticalVelocity = Physics.gravity.y * Time.deltaTime;
        }

        Debug.Log("Aplicar gravedad");
        verticalVelocity -= gravityValue * Time.deltaTime;


        else 
        {
            moveDirection.y -= gravityValue * Time.deltaTime;
            Debug.Log("Aplicar gravedad");
            Controller.Move(moveDirection * Time.deltaTime);
            //verticalVelocity += Physics.gravity.y * Time.deltaTime;
        }*/
    }



}
