using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerController : MonoBehaviour
{
    private PlayerInput playerInput;

    private InputAction moverAction;
    private Rigidbody rb;
    private Animator animator;
    [SerializeField] private float velocidadChar = 5f;
    [SerializeField] private float velocidadRotation = 5f;
    [SerializeField] private string actionName;
    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        moverAction = playerInput.actions[actionName];

        rb = this.GetComponent<Rigidbody>();
        animator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        move();
    }

    private void move()
    {
        transform.Rotate(0, moverAction.ReadValue<Vector2>().x * velocidadRotation * Time.deltaTime, 0);
        float curSpeed = velocidadChar * moverAction.ReadValue<Vector2>().y;
        transform.Translate(Vector3.forward * (curSpeed) * Time.deltaTime);
        if (curSpeed != 0)
        {
            animator.SetBool("isMoving", true);   
        } else
        {
            animator.SetBool("isMoving", false);
        }
    }

    
}
