using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeadState : PlayerBaseState 
{
    public PlayerDeadState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    private float velocityX = 0.0f;
    private float velocityY = 0.0f;

    bool _recuperado = false;


    public override void Enter() {
        //Debug.Log("Enter Move");
        stateMachine.animator.SetBool(GameConstants.isDead, true);
        stateMachine.characterController.enabled = false;
        stateMachine._goTriggerMuerto.SetActive(true);

    }

    public override void Tick(float deltaTime) {

        if(!_recuperado && stateMachine._nivelSalud>0){
            _recuperado = true;
            stateMachine.animator.SetBool(GameConstants.isDead, false);
            stateMachine.SetAvisoMePuedenCurar(false);
        }

        if(_recuperado && stateMachine._finLevantado){
            stateMachine._finLevantado = false;
            stateMachine.SwitchState(new PlayerIdleState(stateMachine));
        }
    }



    public override void Exit() {
        stateMachine.characterController.enabled = true;
        stateMachine._goTriggerMuerto.SetActive(false);
    }
}
