using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSanarState : PlayerBaseState
{
    public PlayerSanarState(PlayerStateMachine stateMachine) : base(stateMachine) { }




    private float _tiempoCuracion = 2;
    private static int PUNTOS_RECURAR_SANAR = 100;

    public override void Enter() {
        //Debug.Log("Enter Repair");
        stateMachine.animator.SetBool("isRepairing", true);

        if(stateMachine._elementoCurable == null){
            Debug.Log("Error Enter PlayerSanarState, curable null");
            stateMachine.SwitchState(new PlayerIdleState(stateMachine));
        }
        
        stateMachine._goFuenteCorazones.SetActive(true);
    }

    public override void Tick(float deltaTime) {
        //Debug.Log("Ejecutando estado Repair");
        stateMachine.LogicaEscudoEnTikEstados();

        if(stateMachine._nivelSalud<=0){
            stateMachine.SwitchState(new PlayerDeadState(stateMachine));
            return;
        }

        _tiempoCuracion-=deltaTime;

        if(_tiempoCuracion<=0){
            stateMachine._elementoCurable.RecuperarSalud(PUNTOS_RECURAR_SANAR);
            stateMachine.SwitchState(new PlayerIdleState(stateMachine));
        }


        if (!stateMachine.inputReader.interactAction.inProgress) {
            
            stateMachine.SwitchState(new PlayerIdleState(stateMachine));
        }
    }

    public override void Exit() {
        //Debug.Log("Exit Repair");
        stateMachine.animator.SetBool("isRepairing", false);
        stateMachine._goFuenteCorazones.SetActive(false);
    }

}
