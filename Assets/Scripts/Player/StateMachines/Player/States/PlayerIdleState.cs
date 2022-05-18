using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerBaseState 
{
    public PlayerIdleState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter() {
     //   Debug.Log("Enter Idle");
    }

    public override void Tick(float deltaTime) {
       // Debug.Log("Ejecutando estado Idle");
        if (stateMachine.inputReader.repairAction.triggered) {
            stateMachine.SwitchState(new PlayerRepairState(stateMachine));
        }
        if (stateMachine.inputReader.moveAction.ReadValue<Vector2>() != Vector2.zero) {
            stateMachine.SwitchState(new PlayerMoveState(stateMachine));
        }

        if (!stateMachine.IsGrounded()) {
            stateMachine.SwitchState(new PlayerFallState(stateMachine));
        }
    }

    public override void Exit() {
       // Debug.Log("Exit Idle");
    }

}
