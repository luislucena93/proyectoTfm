using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    PlayerStateMachine playerStateMachine;
    DialogueManager dialogManager;

    private void Awake()
    {
        playerStateMachine = GetComponent<PlayerStateMachine>();
    }

}
