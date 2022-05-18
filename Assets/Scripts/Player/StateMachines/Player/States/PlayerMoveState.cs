using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerBaseState 
{
    public PlayerMoveState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter() {
        Debug.Log("Enter Move");
        stateMachine.animator.SetBool("isMoving", true);

    }


    public override void Tick(float deltaTime) {
        Debug.Log("Ejecutando estado Move");

        // Obtengo el valor del movimiento en cada momento
        Vector2 movementValue = stateMachine.inputReader.moveAction.ReadValue<Vector2>();

        if (movementValue == Vector2.zero) {
            stateMachine.SwitchState(new PlayerIdleState(stateMachine));
            // Evitamos que rote en caso de estar parados
            return;
        }

        if (stateMachine.isPushing)
        {
            stateMachine.SwitchState(new PlayerPushState(stateMachine));
        }

        /*
        if (!stateMachine.IsGrounded()) {
            stateMachine.SwitchState(new PlayerFallState(stateMachine));
        }*/

        // El valor y del vector2 es el correcto para el z del vector3
        Vector3 inputDirection = new Vector3();
        inputDirection.x = movementValue.x;
        inputDirection.y = 0;
        inputDirection.z = movementValue.y;


        // Movimiento y rotación
        //stateMachine.rb.transform.LookAt(stateMachine.rb.transform.position + inputDirection);
        //stateMachine.rb.MovePosition(stateMachine.rb.position + inputDirection.normalized* stateMachine.speed * Time.deltaTime);

        float rotationDirection = inputDirection.x;
        stateMachine.rb.transform.Rotate(0, rotationDirection * 130 * Time.deltaTime, 0);
        float curSpeed = stateMachine.speed * inputDirection.z;
        stateMachine.rb.transform.Translate(Vector3.forward * (curSpeed) * Time.deltaTime);

        /* Character controller
         
        // Movimiento
        stateMachine.characterController.Move(movement * stateMachine.MovementSpeed * deltaTime);

        // Rotación
        stateMachine.transform.rotation = Quaternion.LookRotation(movement);

        */
    }

    public override void Exit() {
        Debug.Log("Exit Move");
        stateMachine.animator.SetBool("isMoving", false);
    }

}
