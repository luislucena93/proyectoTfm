
using UnityEngine;

public abstract class BotBaseState : State
{
    // Solo las clases que hereden de aqu� podran acceder a esta variable
    protected BotStateMachine stateMachine;

    // Constructor
    public BotBaseState(BotStateMachine stateMachine) {
        this.stateMachine = stateMachine;
    }

}
