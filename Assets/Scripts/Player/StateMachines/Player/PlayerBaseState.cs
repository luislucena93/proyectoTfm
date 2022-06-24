
using UnityEngine;

public abstract class PlayerBaseState : State
{
    // Solo las clases que hereden de aqu� podran acceder a esta variable
    protected PlayerStateMachine stateMachine;

    // Constructor
    public PlayerBaseState(PlayerStateMachine stateMachine) {
        this.stateMachine = stateMachine;
    }
    /*
    protected void PlayerMovement(Vector3 movement, float deltaTime)
    {
        //stateMachine.characterController.Move((movement + stateMachine.ForceReceiver.Movement) * deltaTime);
        //stateMachine.characterController.SimpleMove(movement * 1f);
        stateMachine.characterController.Move(movement * stateMachine.speed * deltaTime);
    }
    */
}
