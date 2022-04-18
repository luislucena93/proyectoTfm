using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour
{
    PlayerInput playerInput;
    public InputAction moveAction;
    public InputAction repairAction;

    void Awake() 
    {
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions["Move"];
        repairAction = playerInput.actions["Repair"];
    }
}
