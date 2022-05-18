using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerBaseState 
{
    public PlayerMoveState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    private float velocityX = 0.0f;
    private float velocityY = 0.0f;

    public override void Enter() {
        Debug.Log("Enter Move");
        stateMachine.animator.SetBool(GameConstants.isMovingHash, true);

    }

    public override void Tick(float deltaTime) {
        Debug.Log("Ejecutando estado Move");

        // Obtengo el valor del movimiento en cada momento
        Vector2 movementValue = stateMachine.inputReader.moveAction.ReadValue<Vector2>();

        Vector3 inputDirection = CalculateMovement(movementValue);


        /*
        if (!stateMachine.IsGrounded()) {
            stateMachine.SwitchState(new PlayerFallState(stateMachine));
        }*/

        // El valor 'y' del vector2 es el correcto para el 'z' del vector3
        /*
       Vector3 inputDirection = new Vector3();
       inputDirection.x = movementValue.x;
       inputDirection.y = 0;
       inputDirection.z = movementValue.y;
       */

        // Movimiento y rotación
        //stateMachine.rb.transform.LookAt(stateMachine.rb.transform.position + inputDirection);
        //stateMachine.rb.MovePosition(stateMachine.rb.position + inputDirection.normalized* stateMachine.speed * Time.deltaTime);

        /*
        float rotationDirection = inputDirection.x;
        stateMachine.rb.transform.Rotate(0, rotationDirection * 130 * Time.deltaTime, 0);
        float curSpeed = stateMachine.speed * inputDirection.z;
        curSpeed = curSpeed < 0 ? curSpeed / 4 : curSpeed;
        stateMachine.rb.transform.Translate(Vector3.forward * (curSpeed) * Time.deltaTime);
        */


        /* Character controller */

        PlayerMovement(inputDirection * stateMachine.speed, deltaTime);

        //velocityX = Mathf.Abs(movementValue.x);
        //velocityY = Mathf.Abs(movementValue.y);

        //stateMachine.animator.SetFloat(GameConstants.movementSpeedHash, velocity);
        //stateMachine.animator.SetFloat(GameConstants.movementXHash, movementValue.x, 0, deltaTime);
        //stateMachine.animator.SetFloat(GameConstants.movementYHash, movementValue.y, 0, deltaTime);

        if (movementValue == Vector2.zero) {
            stateMachine.SwitchState(new PlayerIdleState(stateMachine));
            stateMachine.animator.SetFloat(GameConstants.movementSpeedHash, 0, 0, deltaTime);
            // Evitamos que rote en caso de estar parados
            return;
        }

        /*
        if (!stateMachine.characterController.isGrounded) {
            stateMachine.SwitchState(new PlayerFallState(stateMachine));
            return;
        }
        */
        stateMachine.animator.SetFloat(GameConstants.movementSpeedHash, 1, 0.1f, deltaTime);

        RotationDirection(inputDirection, deltaTime);

    }



    public override void Exit() {
        Debug.Log("Exit Move");
        stateMachine.animator.SetBool(GameConstants.isMovingHash, false);
    }


    private Vector3 CalculateMovement(Vector3 movementValue) 
    {
        Vector3 forward = stateMachine.MainCameraTransform.forward;
        Vector3 right = stateMachine.MainCameraTransform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        return forward * movementValue.y + right * movementValue.x;
    }

    private void RotationDirection(Vector3 inputDirection, float deltaTime) 
    {
        //Lerp para girar suavemente
        stateMachine.transform.rotation = Quaternion.Lerp(
            stateMachine.transform.rotation,
            Quaternion.LookRotation(inputDirection),
            deltaTime * stateMachine.RotationDamping);
    }
}
