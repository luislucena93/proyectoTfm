using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRepairState : PlayerBaseState
{
    public PlayerRepairState(PlayerStateMachine stateMachine) : base(stateMachine) { }


    private IReparable _iReparable;
    private IInteraccionable _interaccionable;

    public override void Enter() {
        //Debug.Log("Enter Repair");
        stateMachine.animator.SetBool("isRepairing", true);
        CentraPersonaje();


        if(stateMachine._objetoInteraccionable!=null){
            _interaccionable = stateMachine._objetoInteraccionable;
            _iReparable = _interaccionable.GetTransform().gameObject.GetComponent<IReparable>();
            if(_iReparable == null){
                Debug.Log("Error Enter PlayerRepairState no reparable");
            }
            
            _interaccionable.ComenzarInteraccion();
        }   else{
            Debug.Log("Error Enter PlayerRepairState, interaccionable null");
        }

        stateMachine._pistolaReparacion.SetActive(true);
    }

    public override void Tick(float deltaTime) {
        //Debug.Log("Ejecutando estado Repair");
        if(stateMachine._nivelSalud<=0){
            stateMachine.SwitchState(new PlayerDeadState(stateMachine));
            return;
        }


        if (stateMachine.inputReader.repairAction.inProgress && !_iReparable.IsReparado()) {

        } else{
            _interaccionable.FinalizarInteraccion();
            stateMachine.SwitchState(new PlayerIdleState(stateMachine));
        }
    }

    public override void Exit() {
        //Debug.Log("Exit Repair");
        stateMachine.animator.SetBool("isRepairing", false);
        stateMachine._pistolaReparacion.SetActive(false);
    }


    private void CentraPersonaje(){
        Vector3 posicionReparable = stateMachine._objetoInteraccionable.GetTransform().position;
        stateMachine.transform.LookAt(posicionReparable);
    }

}
