using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallState : PlayerBaseState {
    public PlayerFallState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter() {
        //Debug.Log("Enter Fall");
        stateMachine.animator.SetBool("isFalling", true);

    }

    public override void Tick(float deltaTime) {
        //Debug.Log("Ejecutando estado Fall");

        if (stateMachine.characterController.isGrounded) {
            stateMachine.SwitchState(new PlayerIdleState(stateMachine));
        }

        /*
        if (!stateMachine.IsGrounded()) {
            stateMachine.SwitchState(new PlayerIdleState(stateMachine));
        }
        */
    }

    public override void Exit() {
        Debug.Log("Exit Fall");
        stateMachine.animator.SetBool("isFalling", false);

    }

}
