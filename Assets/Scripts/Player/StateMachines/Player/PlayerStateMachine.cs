using UnityEngine;

public class PlayerStateMachine : StateMachine 
{
    [field: SerializeField] public InputReader inputReader { get; private set; }
    [field: SerializeField] public Animator animator { get; private set; }
    [field: SerializeField] public float speed { get; private set; }
    [field: SerializeField] public float RotationDamping { get; private set; }
    [field: SerializeField] public Rigidbody rb { get; private set; }
    [field: SerializeField] public Collider playerCollider { get; private set; }
    [field: SerializeField] public float minDistanceToGround { get; private set; }
    [field: SerializeField] public CharacterController characterController { get; private set; }
    [field: SerializeField] public ForceReceiver ForceReceiver { get; private set; }


    [field: SerializeField] public Transform MainCameraTransform { get; private set; }


    //[field: SerializeField] public BoxCollider boxCollider { get; private set; }

    private void Start() 
    {
        //MainCameraTransform = Camera.main.transform;

        // Estado inicial, dando como referencia este PlayerStateMachine
        SwitchState(new PlayerIdleState(this));
    }

    public bool IsGrounded() {
        Vector3 targetCenter = playerCollider.bounds.center;
        return Physics.Raycast(targetCenter, Vector3.down, minDistanceToGround);
    }
}
