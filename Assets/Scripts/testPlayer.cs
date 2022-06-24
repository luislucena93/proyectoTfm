using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testPlayer : MonoBehaviour
{
    Vector3 velocity;
    Vector3 inputDirection;
    Vector2 movementValue;

    float Gravity = -3.5f;
    bool isGrounded;
    bool isJumping;

    [field: SerializeField]
    public InputReader inputReader;

    [field: SerializeField]
    public CharacterController characterController;

    [field: SerializeField]
    public float speed = 2f;

    [field: SerializeField]
    public float RotationDamping;

    [field: SerializeField]
    public Transform MainCameraTransform;

    void Update() {
        if (!isGrounded) {
            if (isJumping) {
                isJumping = false;
            }

            velocity.y += Gravity * Time.deltaTime;

            characterController.Move(velocity * Time.deltaTime);

        }
        else if (isJumping) {
            isGrounded = false;
            velocity.y = 0;
            velocity.y += 3f;

            characterController.Move(velocity * Time.deltaTime);
        }

        if (inputReader.jumpAction.triggered) {
            isJumping = true;
        }

        movementValue = inputReader.moveAction.ReadValue<Vector2>();
        inputDirection = CalculateMovement(movementValue);

        PlayerMovement(inputDirection * speed, Time.deltaTime);
        RotationDirection(inputDirection, Time.deltaTime);


    }


    private void OnControllerColliderHit(ControllerColliderHit hit) {
        if (hit.transform.CompareTag("Ground") && !isGrounded) {
            isGrounded = true;
        }
    }


    private Vector3 CalculateMovement(Vector3 movementValue) {
        Vector3 forward = MainCameraTransform.forward;
        Vector3 right = MainCameraTransform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        return forward * movementValue.y + right * movementValue.x;
    }

    protected void PlayerMovement(Vector3 movement, float deltaTime) {
        characterController.Move(movement * speed * Time.deltaTime);
    }

    private void RotationDirection(Vector3 inputDirection, float deltaTime) {
        //Lerp para girar suavemente
        transform.rotation = Quaternion.Lerp(
            transform.rotation,
            Quaternion.LookRotation(inputDirection),
            deltaTime * RotationDamping);
    }
}
