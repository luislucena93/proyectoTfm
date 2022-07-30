using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerBaseState 
{
    public PlayerJumpState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    float _tiempoEsperarSalto = 0.3f;

    float _valorGuardadoStepOffset;
    float _valorGuardadoSlopeLimit;

    public override void Enter() 
    {
        Debug.Log("Enter jump");
        if(stateMachine.isGrounded){
            stateMachine.isJumping = true;
            stateMachine.animator.SetBool("isJumping", true);
        }
        _valorGuardadoStepOffset = stateMachine.characterController.stepOffset;
        stateMachine.characterController.stepOffset = 0;

        _valorGuardadoSlopeLimit = stateMachine.characterController.slopeLimit;
        stateMachine.characterController.slopeLimit = 90;
    }

    public override void Tick(float deltaTime) 
    {
    //    Debug.Log("Ejecutando estado jump");

        stateMachine.LogicaEscudoEnTikEstados();

        if(_tiempoEsperarSalto > 0){
            _tiempoEsperarSalto -= deltaTime;
        }


        if (stateMachine.isGrounded) 
        {
            if(_tiempoEsperarSalto < 0 ){
                stateMachine.SwitchState(new PlayerIdleState(stateMachine));
                return;
            }
        }
        
        if (!stateMachine.isGrounded) 
        {
            stateMachine.velocity.y += stateMachine.gravity * Time.deltaTime;
           // stateMachine.characterController.Move(stateMachine.velocity * Time.deltaTime);

        }
        else if (stateMachine.isJumping)
        {
            stateMachine.isGrounded = false;
            stateMachine.velocity.y = 0;
            stateMachine.velocity.y += stateMachine.jumpForce;

           // stateMachine.characterController.Move(stateMachine.velocity * Time.deltaTime);
        }

        stateMachine.movementValue = stateMachine.inputReader.moveAction.ReadValue<Vector2>();
        Debug.Log("If Salto "+stateMachine.movementValue+" segundo "+stateMachine._segundoColliderCheckSuelo+" ground "+stateMachine.isGrounded);
        if (stateMachine.movementValue.magnitude <0.1f && stateMachine._segundoColliderCheckSuelo){
            stateMachine.inputDirection = CalculateMovementCallendoBorde(new Vector2(0,stateMachine._fuerzaBordeCajas));
        }   else{
            stateMachine.inputDirection = CalculateMovement(stateMachine.movementValue);
        }
        Debug.Log("Input Salto"+(stateMachine.inputDirection));

        

        Debug.Log("PlayerMovement Salto"+(stateMachine.inputDirection * stateMachine.speed + stateMachine.velocity));
        PlayerMovement(stateMachine.inputDirection * stateMachine.speed + stateMachine.velocity, Time.deltaTime);

        if (stateMachine.movementValue == Vector2.zero) {
            // Evitamos que rote en caso de estar parados
            return; 
        }  

        RotationDirection(stateMachine.inputDirection, Time.deltaTime);
    }

    public override void Exit() 
    {
        Debug.Log("Exit jump");
        stateMachine.isJumping = false;
        stateMachine.animator.SetBool("isJumping", false);
        stateMachine.characterController.stepOffset = _valorGuardadoStepOffset;
        stateMachine.characterController.slopeLimit = _valorGuardadoSlopeLimit;
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

    private Vector3 CalculateMovementCallendoBorde(Vector3 movementValue) 
    {
        Vector3 forward = stateMachine.gameObject.transform.forward;
        Vector3 right = stateMachine.gameObject.transform.right;

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



