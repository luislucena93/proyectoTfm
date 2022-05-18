using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour
{
    PlayerInput playerInput;
    public InputAction moveAction;
    public InputAction repairAction;
    public InputAction interactAction;
    public InputAction lookAction;


    void Awake() 
    {
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions["Move"];
        repairAction = playerInput.actions["Repair"];
        interactAction = playerInput.actions["Interact"];
        interactAction = playerInput.actions["Look"];

    }
}
