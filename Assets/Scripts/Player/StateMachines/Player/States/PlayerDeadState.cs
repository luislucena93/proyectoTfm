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
        stateMachine._camaraDead.SetActive(true);
        stateMachine._camaraFree.SetActive(false);

        stateMachine.animator.SetBool(GameConstants.isDead, true);
        stateMachine.characterController.enabled = false;
        stateMachine._goTriggerMuerto.SetActive(true);
        stateMachine._menuController.CheckMuertos();
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
            return;
        }
    }



    public override void Exit() {
        
        stateMachine._camaraFree.transform.position = stateMachine._posicionCamaraSanado.transform.position;
        stateMachine._camaraFree.transform.rotation = stateMachine._posicionCamaraSanado.transform.rotation;
        
        stateMachine._camaraFree.SetActive(true);
        stateMachine._camaraDead.SetActive(false);
        stateMachine.characterController.enabled = true;
        stateMachine._goTriggerMuerto.SetActive(false);
    }
}
