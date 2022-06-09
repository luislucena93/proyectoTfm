using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraccionState : PlayerBaseState
{
    public PlayerInteraccionState(PlayerStateMachine stateMachine) : base(stateMachine) { }


    private IInteraccionable _interaccionable;

    public override void Enter() {
        CentraPersonaje();
        if(stateMachine._objetoInteraccionable!=null){
            _interaccionable = stateMachine._objetoInteraccionable;
            _interaccionable.ComenzarInteraccion();

            stateMachine._ikReferenciaMano.transform.position = stateMachine._objetoInteraccionable.GetTransform().position;
        }

        stateMachine._ikRigMano.weight =1;

    }

    public override void Tick(float deltaTime) {

        if (stateMachine.inputReader.interactAction.inProgress) {

        } else{
            _interaccionable.FinalizarInteraccion();
            stateMachine.SwitchState(new PlayerIdleState(stateMachine));
        }
    }

    public override void Exit() {
        stateMachine._ikRigMano.weight = 0;
    }


    private void CentraPersonaje(){
        Vector2 posicionReparable = stateMachine._objetoInteraccionable.GetTransform().position;
        stateMachine.transform.LookAt(posicionReparable);
    }

}
