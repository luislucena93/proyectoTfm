using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerBaseState 
{
    public PlayerJumpState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter() 
    {
    //    Debug.Log("Enter jump");
        stateMachine.isJumping = true;
        stateMachine.animator.SetBool("isJumping", true);
    }

    public override void Tick(float deltaTime) 
    {
    //    Debug.Log("Ejecutando estado jump");

        stateMachine.LogicaEscudoEnTikEstados();


        if (!stateMachine.isGrounded) 
        {
            stateMachine.velocity.y += stateMachine.gravity * Time.deltaTime;
            stateMachine.characterController.Move(stateMachine.velocity * Time.deltaTime);

        }
        else if (stateMachine.isJumping)
        {
            stateMachine.isGrounded = false;
            stateMachine.velocity.y = 0;
            stateMachine.velocity.y += stateMachine.jumpForce;

            stateMachine.characterController.Move(stateMachine.velocity * Time.deltaTime);
        }

        if (stateMachine.isGrounded) 
        {
            stateMachine.SwitchState(new PlayerIdleState(stateMachine));
        }

        stateMachine.movementValue = stateMachine.inputReader.moveAction.ReadValue<Vector2>();
        stateMachine.inputDirection = CalculateMovement(stateMachine.movementValue);

        PlayerMovement(stateMachine.inputDirection * stateMachine.speed, Time.deltaTime);

        if (stateMachine.movementValue == Vector2.zero) {
            // Evitamos que rote en caso de estar parados
            return;
        }

        RotationDirection(stateMachine.inputDirection, Time.deltaTime);
    }

    public override void Exit() 
    {
    //    Debug.Log("Exit jump");
        stateMachine.isJumping = false;
        stateMachine.animator.SetBool("isJumping", false);
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

    protected void PlayerMovement(Vector3 movement, float deltaTime) 
    {
        stateMachine.characterController.Move(movement * stateMachine.speed * Time.deltaTime);
    }

    private void RotationDirection(Vector3 inputDirection, float deltaTime) 
    {
        //Lerp para rotar suavemente
        stateMachine.transform.rotation = Quaternion.Lerp(
            stateMachine.transform.rotation,
            Quaternion.LookRotation(inputDirection),
            deltaTime * stateMachine.RotationDamping);
    }
}



