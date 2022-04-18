
public abstract class PlayerBaseState : State
{
    // Solo las clases que hereden de aqu� podran acceder a esta variable
    protected PlayerStateMachine stateMachine;

    // Constructor
    public PlayerBaseState(PlayerStateMachine stateMachine) {
        this.stateMachine = stateMachine;
    }

}
