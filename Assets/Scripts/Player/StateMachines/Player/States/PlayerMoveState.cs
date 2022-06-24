using UnityEngine;

public class PlayerMoveState : PlayerBaseState 
{
    public PlayerMoveState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter() 
    {
        Debug.Log("Enter Move");
        stateMachine.animator.SetBool(GameConstants.isMovingHash, true);
    }

    public override void Tick(float deltaTime) 
    {
        Debug.Log("Ejecutando estado Move");

        if (!stateMachine.isGrounded) 
        {
            stateMachine.velocity.y += stateMachine.gravity * Time.deltaTime;
            stateMachine.characterController.Move(stateMachine.velocity * Time.deltaTime);
        }

        if (stateMachine.inputReader.jumpAction.triggered && stateMachine.isGrounded) 
        {
            stateMachine.SwitchState(new PlayerJumpState(stateMachine));
        }

        if (stateMachine.isPushing)
        {
            stateMachine.SwitchState(new PlayerPushState(stateMachine));
        }


        if (stateMachine.inputReader.jumpAction.triggered && stateMachine.isGrounded) 
        {
            stateMachine.SwitchState(new PlayerJumpState(stateMachine));
        }

        stateMachine.movementValue = stateMachine.inputReader.moveAction.ReadValue<Vector2>();
        stateMachine.inputDirection = CalculateMovement(stateMachine.movementValue);

        PlayerMovement(stateMachine.inputDirection * stateMachine.speed, deltaTime);

        if (stateMachine.movementValue == Vector2.zero) 
        {
            stateMachine.SwitchState(new PlayerIdleState(stateMachine));
            stateMachine.animator.SetFloat(GameConstants.movementSpeedHash, 0, 0, deltaTime);
            // Evitamos que rote en caso de estar parados
            return;
        }

        stateMachine.animator.SetFloat(GameConstants.movementSpeedHash, 1, 0.1f, deltaTime);
        RotationDirection(stateMachine.inputDirection, deltaTime);
    }

    public override void Exit() 
    {
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

        // El valor 'y' del vector2 es el correcto para el 'z' del vector3
        return forward * movementValue.y + right * movementValue.x;
    }

    private void RotationDirection(Vector3 inputDirection, float deltaTime) 
    {
        //Lerp para rotar suavemente
        stateMachine.transform.rotation = Quaternion.Lerp(
            stateMachine.transform.rotation,
            Quaternion.LookRotation(inputDirection),
            deltaTime * stateMachine.RotationDamping);
    }

    protected void PlayerMovement(Vector3 movement, float deltaTime) 
    {
        stateMachine.characterController.SimpleMove(movement * stateMachine.speed);
    }

}
