using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPushState : PlayerBaseState {
    public PlayerPushState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter() {
        Debug.Log("Enter Push");
    }

    public override void Tick(float deltaTime) {
        //Debug.Log("Ejecutando estado Push");

        stateMachine.LogicaEscudoEnTikEstados();

        if(stateMachine._nivelSalud<=0){
            stateMachine.SwitchState(new PlayerDeadState(stateMachine));
            return;
        }

        if (!stateMachine.inputReader.interactAction.IsPressed()) {
            stateMachine.isPushing = false;
            stateMachine.SwitchState(new PlayerIdleState(stateMachine));
        }

        stateMachine.movementValue = stateMachine.inputReader.moveAction.ReadValue<Vector2>();
        stateMachine.inputDirection = CalculateMovement(stateMachine.movementValue);

        PlayerMovement(stateMachine.inputDirection * stateMachine.speed, deltaTime);

    }

    public override void Exit() {
        Debug.Log("Exit Push");
        stateMachine.isPushing = false;
        stateMachine.animator.SetBool("isPushing", false);
    }

    private Vector3 CalculateMovement(Vector3 movementValue)
    {
        Vector3 forward = stateMachine.MainCameraTransform.forward;
        
        forward.y = 0f;

        forward.Normalize();
        return movementValue.y > 0 ? forward * movementValue.y : forward * 0;
    }

    protected void PlayerMovement(Vector3 movement, float deltaTime)
    {
        if(movement.magnitude > 0)
        {
            stateMachine.animator.SetBool("isPushing", true);
        } else
        {
            stateMachine.animator.SetBool("isPushing", false);
        }
        stateMachine.characterController.SimpleMove(movement * stateMachine.speed/4);
    }

}
