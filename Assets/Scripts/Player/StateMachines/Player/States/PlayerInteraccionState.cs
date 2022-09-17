using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraccionState : PlayerBaseState
{
    public PlayerInteraccionState(PlayerStateMachine stateMachine) : base(stateMachine) { }


    private IInteraccionable _interaccionable;

    public override void Enter() {
    //    Debug.Log("Enter interaccion");
        if(stateMachine._objetoInteraccionable!=null){
            _interaccionable = stateMachine._objetoInteraccionable;
            _interaccionable.ComenzarInteraccion();

            stateMachine._ikReferenciaMano.transform.position = stateMachine._objetoInteraccionable.GetTransform().position;
            stateMachine._ikReferenciaMano.transform.rotation = stateMachine._objetoInteraccionable.GetTransform().rotation;
        }

        stateMachine._ikRigMano.weight =0.7f;

        stateMachine.transform.forward = -stateMachine._objetoInteraccionable.GetTransform().forward;
        stateMachine.transform.eulerAngles = new Vector3(0,stateMachine.transform.eulerAngles.y,0);
        stateMachine.animator.SetBool("isPressingButton", true);

    }

    public override void Tick(float deltaTime) {
        stateMachine.LogicaEscudoEnTikEstados();

        if(stateMachine._nivelSalud<=0){
            stateMachine.SwitchState(new PlayerDeadState(stateMachine));
            return;
        }

        if (stateMachine.inputReader.interactAction.inProgress) {

        } else{
            stateMachine.SwitchState(new PlayerIdleState(stateMachine));
            return;
        }
    }

    public override void Exit() {
    //            Debug.Log("Exit interaccion");
        stateMachine._ikRigMano.weight = 0;
        _interaccionable.FinalizarInteraccion();
        stateMachine.animator.SetBool("isPressingButton", false);
    }

}
