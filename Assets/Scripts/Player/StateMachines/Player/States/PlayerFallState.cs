using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallState : PlayerBaseState {
    public PlayerFallState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter() {
        Debug.Log("Enter Fall");
        stateMachine.animator.SetBool("isFalling", true);

    }

    public override void Tick(float deltaTime) {
        Debug.Log("Ejecutando estado Fall");

        if (!stateMachine.IsGrounded()) {
            stateMachine.SwitchState(new PlayerIdleState(stateMachine));
        }

        /* Character controller
         
        // Añadir gravedad
        movement += Physics.gravity;

        // Movimiento
        stateMachine.characterController.Move(movement * stateMachine.MovementSpeed * deltaTime);

        // Rotación
        stateMachine.transform.rotation = Quaternion.LookRotation(movement);

       */
    }

    public override void Exit() {
        Debug.Log("Exit Fall");
        stateMachine.animator.SetBool("isFalling", false);

    }

}
