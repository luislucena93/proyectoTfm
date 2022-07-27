using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour
{
    PlayerInput playerInput;
    public InputAction moveAction;
    public InputAction interactAction;
    public InputAction lookAction;
    public InputAction jumpAction;
    public InputAction pauseAction;

    public InputAction escudoAction;


    void Awake() 
    {
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions["Move"];
        interactAction = playerInput.actions["Interact"];
        lookAction = playerInput.actions["Look"];
        escudoAction = playerInput.actions["Escudo"];
        jumpAction = playerInput.actions["Jump"];
        pauseAction = playerInput.actions["PauseMenu"];
    }
}
