using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPushState : PlayerBaseState {
    public PlayerPushState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter() {
        Debug.Log("Enter Push");
    }

    public override void Tick(float deltaTime) {
        Debug.Log("Ejecutando estado Push");

        if (!stateMachine.isPushing) {
            stateMachine.SwitchState(new PlayerIdleState(stateMachine));
        }

        Vector2 movementValue = stateMachine.inputReader.moveAction.ReadValue<Vector2>();
        Vector3 inputDirection = new Vector3();
        inputDirection.x = movementValue.x;
        inputDirection.y = 0;
        inputDirection.z = movementValue.y;

        float rotationDirection = inputDirection.x;
        stateMachine.rb.transform.Rotate(0, rotationDirection * 130 * Time.deltaTime, 0);
        float curSpeed = stateMachine.speed * inputDirection.z;
        stateMachine.rb.transform.Translate(Vector3.forward * (curSpeed) * Time.deltaTime);

        if (curSpeed != 0)
        {
            stateMachine.animator.SetBool("isPushing", true);
        } else
        {
            stateMachine.animator.SetBool("isPushing", false);
        }
    }

    public override void Exit() {
        Debug.Log("Exit Push");
        stateMachine.animator.SetBool("isPushing", false);

    }

}
