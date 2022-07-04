using UnityEngine;

public class PlayerIdleState : PlayerBaseState 
{
    public PlayerIdleState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter() 
    {
        Debug.Log("Enter Idle");
        stateMachine.animator.SetBool("isPushing", false);
    }

    public override void Tick(float deltaTime) 
    {
        //Debug.Log("Ejecutando estado Idle");

        if (!stateMachine.isGrounded) 
        {
            stateMachine.velocity.y += stateMachine.gravity * Time.deltaTime;
            stateMachine.characterController.Move(stateMachine.velocity * Time.deltaTime);
        }

        if (stateMachine.inputReader.pauseAction.triggered) 
        {
            stateMachine.menuController.OpenCloseMenu();
        }

        if (stateMachine.inputReader.jumpAction.triggered && stateMachine.isGrounded) 
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

        if (stateMachine.inputReader.repairAction.triggered) {
            CheckReparando();
        }

        if (stateMachine.isPushing) {
            stateMachine.SwitchState(new PlayerPushState(stateMachine));
        }

    }

    public override void Exit() 
    {
        Debug.Log("Exit Idle");
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
        Debug.Log(stateMachine.hitPushable);
        if (stateMachine.dialogueManager.dialogOpen)
        {
            stateMachine.dialogueManager.DisplayNextSentence();
        }
        if (stateMachine._objetoInteraccionable != null){
            Debug.Log("switch interaccionable");
            stateMachine.SwitchState(new PlayerInteraccionState(stateMachine));
        }
        if (stateMachine.hitPushable)
        {
            stateMachine.SwitchState(new PlayerPushState(stateMachine));
        }
    }
}
