using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRepairState : PlayerBaseState
{
    public PlayerRepairState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter() {
        //Debug.Log("Enter Repair");
        stateMachine.animator.SetBool("isRepairing", true);

    }

    public override void Tick(float deltaTime) {
        //Debug.Log("Ejecutando estado Repair");

        if (stateMachine.inputReader.repairAction.triggered) {
            stateMachine.SwitchState(new PlayerIdleState(stateMachine));
        }
    }

    public override void Exit() {
        //Debug.Log("Exit Repair");
        stateMachine.animator.SetBool("isRepairing", false);

    }

}
