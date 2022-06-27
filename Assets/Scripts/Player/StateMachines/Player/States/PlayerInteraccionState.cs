using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraccionState : PlayerBaseState
{
    public PlayerInteraccionState(PlayerStateMachine stateMachine) : base(stateMachine) { }


    private IInteraccionable _interaccionable;

    public override void Enter() {
        Debug.Log("Enter interaccion");
        if(stateMachine._objetoInteraccionable!=null){
            _interaccionable = stateMachine._objetoInteraccionable;
            _interaccionable.ComenzarInteraccion();

            stateMachine._ikReferenciaMano.transform.position = stateMachine._objetoInteraccionable.GetTransform().position;
            stateMachine._ikReferenciaMano.transform.rotation = stateMachine._objetoInteraccionable.GetTransform().rotation;
        }

        stateMachine._ikRigMano.weight =1;

        stateMachine.transform.forward = -stateMachine._objetoInteraccionable.GetTransform().forward;

    }

    public override void Tick(float deltaTime) {
        if(stateMachine._nivelSalud<=0){
            stateMachine.SwitchState(new PlayerDeadState(stateMachine));
            return;
        }

        if (stateMachine.inputReader.interactAction.inProgress) {

        } else{
            _interaccionable.FinalizarInteraccion();
            stateMachine.SwitchState(new PlayerIdleState(stateMachine));
        }
    }

    public override void Exit() {
                Debug.Log("Exit interaccion");
        stateMachine._ikRigMano.weight = 0;
    }

}
