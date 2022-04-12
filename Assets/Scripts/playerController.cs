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
        float rotationDirection = moverAction.ReadValue<Vector2>().x ;
        transform.Rotate(0, rotationDirection * velocidadRotation * Time.deltaTime, 0);
        float curSpeed = velocidadChar * moverAction.ReadValue<Vector2>().y;
        curSpeed = curSpeed < 0 ? curSpeed / 4 : curSpeed;
        transform.Translate(Vector3.forward * (curSpeed) * Time.deltaTime);
        if (curSpeed > 0)
        {
            animator.SetBool("isRunning", true);
            animator.SetBool("isBacking", false);
        } else  if (curSpeed < 0)
        {
            animator.SetBool("isRunning", false);
            animator.SetBool("isBacking", true);
        } else
        {
            animator.SetBool("isRunning", false);
            animator.SetBool("isBacking", false);
        }
        if (rotationDirection > 0)
        {
            animator.SetBool("turningRight", true);
            animator.SetBool("turningLeft", false);
        } else if (rotationDirection < 0)
        {
            animator.SetBool("turningRight", false);
            animator.SetBool("turningLeft", true);
        } else
        {
            animator.SetBool("turningRight", false);
            animator.SetBool("turningLeft", false);
        }
    }

    
}