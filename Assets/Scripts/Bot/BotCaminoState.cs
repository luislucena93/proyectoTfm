using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotCaminoState : BotBaseState {
    public BotCaminoState (BotStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter() {
       // stateMachine.animator.SetBool("isFalling", true);

    }

    public override void Tick(float deltaTime) {
        //Debug.Log("Ejecutando estado Fall");

      /*  if (stateMachine.characterController.isGrounded) {
            stateMachine.SwitchState(new PlayerIdleState(stateMachine));
        }*/
        stateMachine.AnimarRuedas();
        if(stateMachine.CheckTiempo()){
            
        }
    }

    public override void Exit() {
      //  stateMachine.animator.SetBool("isFalling", false);

    }

}
