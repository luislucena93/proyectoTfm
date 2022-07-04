using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCurarState : PlayerBaseState
{
    public PlayerCurarState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    float _tiempoCurar = 3;


    public override void Enter() {
        //Debug.Log("Enter Repair");
        stateMachine.animator.SetBool("isHealing", true);
        stateMachine._pistolaCurar.SetActive(true);
    }

    public override void Tick(float deltaTime) {
        //Debug.Log("Ejecutando estado Repair");
        if(stateMachine._nivelSalud<=0){
            stateMachine.SwitchState(new PlayerDeadState(stateMachine));
            return;
        }
        _tiempoCurar -= deltaTime;
        if(_tiempoCurar <= 0){

            stateMachine.SwitchState(new PlayerIdleState(stateMachine));
        }
    }

    public override void Exit() {
        //Debug.Log("Exit Repair");
        stateMachine.animator.SetBool("isHealing", false);
        stateMachine._pistolaCurar.SetActive(false);
    }


}
