using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeadState : PlayerBaseState 
{
    public PlayerDeadState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    private float velocityX = 0.0f;
    private float velocityY = 0.0f;

    bool _recuperado = false;

    float _temporizador = 2;

    public override void Enter() {
        //Debug.Log("Enter Move");
        stateMachine.animator.SetBool(GameConstants.isDead, true);


    }

    public override void Tick(float deltaTime) {

        if(!_recuperado && stateMachine._nivelSalud>0){
            _recuperado = true;
            stateMachine.animator.SetBool(GameConstants.isDead, false);
        }

        if(_recuperado){
            _temporizador-= Time.deltaTime;
            if(_temporizador<0){
                stateMachine.SwitchState(new PlayerIdleState(stateMachine));
            }
        }
    }



    public override void Exit() {
    }
}
