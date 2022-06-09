using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerBaseState 
{
    public PlayerIdleState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter() {
        //Debug.Log("Enter Idle");
    }

    public override void Tick(float deltaTime) {
        //Debug.Log("Ejecutando estado Idle");
        if (stateMachine.inputReader.repairAction.triggered) {
            CheckReparando();
        }

        if (stateMachine.isPushing)
        {
            stateMachine.SwitchState(new PlayerPushState(stateMachine));
        }

        if (stateMachine.inputReader.moveAction.ReadValue<Vector2>() != Vector2.zero) {
            stateMachine.SwitchState(new PlayerMoveState(stateMachine));
        }   else{
            CheckCayendo(deltaTime);
        }

        if (stateMachine.inputReader.interactAction.triggered) {
            //Debug.Log("interact action triggered");
            CheckInteraccionable();
        }

       /* if (stateMachine.inputReader.interactAction.inProgress) {
            Debug.Log("interact action progress");
           // CheckInteraccionable();
        }*/

        /*
        if (!stateMachine.characterController.isGrounded) {
            stateMachine.SwitchState(new PlayerFallState(stateMachine));
        }
        */

        /*
        if (!stateMachine.IsGrounded()) {
            stateMachine.SwitchState(new PlayerFallState(stateMachine));
        }
        */
    }

    public override void Exit() {
        //Debug.Log("Exit Idle");
    }


    protected void CheckCayendo(float deltaTime)
    {
        //SimpleMove aplica automáticamente la gravedad
        stateMachine.characterController.SimpleMove(Vector3.zero);
    }

    protected void CheckReparando(){
        if(stateMachine._objetoInteraccionable != null){
            IReparable _iReparable = stateMachine._objetoInteraccionable.GetTransform().gameObject.GetComponent<IReparable>();
            if(_iReparable != null && !_iReparable.IsReparado()){
                stateMachine.SwitchState(new PlayerRepairState(stateMachine));
            }
        }
    }

    protected void CheckInteraccionable(){
        Debug.Log("check interaccionable");
        if(stateMachine._objetoInteraccionable != null){
            Debug.Log("switch interaccionable");
            stateMachine.SwitchState(new PlayerInteraccionState(stateMachine));
        }
    }
}
