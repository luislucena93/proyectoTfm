using UnityEngine;

public class PlayerIdleState : PlayerBaseState 
{
    public PlayerIdleState(PlayerStateMachine stateMachine) : base(stateMachine) { }


    public override void Enter() 
    {
        //Debug.Log("Enter Idle");
        stateMachine.animator.SetBool("isPushing", false);
    }

    public override void Tick(float deltaTime) 
    {
//        Debug.Log("Ejecutando estado Idle");

        stateMachine.LogicaEscudoEnTikEstados();
        stateMachine.LogicaCheckUltimoHurt();


        if(stateMachine._nivelSalud<=0){
            stateMachine.SwitchState(new PlayerDeadState(stateMachine));
            return;
        }
    /*
        if (!stateMachine.isGrounded) 
        {
            stateMachine.velocity.y += stateMachine.gravity * Time.deltaTime;
            stateMachine.characterController.Move(stateMachine.velocity * Time.deltaTime);
        }   */

        if (stateMachine.inputReader.pauseAction.triggered) 
        {
            stateMachine.menuController.OpenCloseMenu();
        }

        if (stateMachine.inputReader.jumpAction.triggered || !stateMachine.isGrounded) 
        {
            stateMachine.SwitchState(new PlayerJumpState(stateMachine));
        }

        if (stateMachine.inputReader.moveAction.ReadValue<Vector2>() != Vector2.zero) 
        {
            stateMachine.SwitchState(new PlayerMoveState(stateMachine));
            return;
        }

        if (stateMachine.inputReader.interactAction.triggered) {
            CheckInteraccionable();
        }

    }

    public override void Exit() 
    {
        //Debug.Log("Exit Idle");
    }

    protected void CheckReparando(){
        
    }

    protected void CheckInteraccionable(){
  //      Debug.Log("check interaccionable");

        if(stateMachine._elementoCurable != null && stateMachine._interaccionCurarDisponible){
            //        Debug.Log("switch interaccionable");
            stateMachine.SwitchState(new PlayerSanarState(stateMachine));
        }

        //Debug.Log(stateMachine.hitPushable);
        if (stateMachine.dialogueManager.dialogOpen)
        {
            stateMachine.dialogueManager.DisplayNextSentence();
        }

        if(stateMachine._objetoInteraccionable != null){
            IReparable _iReparable = stateMachine._objetoInteraccionable.GetTransform().gameObject.GetComponent<IReparable>();
            if(_iReparable != null && !_iReparable.IsReparado()){
                stateMachine.SwitchState(new PlayerRepairState(stateMachine));
                return;
            }   else{
                //Debug.Log("switch interaccionable");
                stateMachine.SwitchState(new PlayerInteraccionState(stateMachine));
                return;
            }
        }
        if (stateMachine.hitPushable)
        {
            stateMachine.SwitchState(new PlayerPushState(stateMachine));
        }
    }
}
