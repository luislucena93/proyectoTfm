using UnityEngine;
using UnityEngine.Animations.Rigging;

public class PlayerStateMachine : StateMachine
{
    [field: SerializeField] public InputReader inputReader { get; private set; }
    [field: SerializeField] public Animator animator { get; private set; }
    [field: SerializeField] public float speed { get; private set; }
    [field: SerializeField] public float RotationDamping { get; private set; }
    [field: SerializeField] public Rigidbody rb { get; private set; }
    [field: SerializeField] public CharacterController characterController { get; private set; }
    [field: SerializeField] public Transform MainCameraTransform { get; private set; }
    [field: SerializeField] public bool isPushing { get; set; }
    [field: SerializeField] public bool hitPushable { get; private set; }
    [field: SerializeField] public float jumpForce { get; private set; }
    [field: SerializeField] public GameObject _ikReferenciaMano { get; private set; }
    [field: SerializeField] public Rig _ikRigMano { get; private set; }
    [field: SerializeField] public float distanceToGround { get; private set; }

    [field: SerializeField] public DialogueManager dialogueManager;

    [field: SerializeField] public MenuController menuController;


    public bool isGrounded;
    public bool isJumping;
    public float gravity;
    public Vector3 velocity;
    public Vector3 inputDirection;
    public Vector2 movementValue;
    public IInteraccionable _objetoInteraccionable;
    public GameObject _pistolaReparacion;
    public float pushForce;

    private void Start()
    {
        // Estado inicial, dando como referencia este PlayerStateMachine
        SwitchState(new PlayerIdleState(this));
    }

    private void FixedUpdate()
    {
        GroundCheck();
        CheckPushing();
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.transform.CompareTag("Ground") && !isGrounded)
        {
            isGrounded = true;
            animator.SetBool("isFalling", false);
        }
        if (isPushing)
        {
            Rigidbody body = hit.collider.attachedRigidbody;
            if (body == null || body.isKinematic) { return; }
            var pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
            body.velocity = pushDir * pushForce;
        }
    }


    public void GroundCheck()
    {
        if (!Physics.Raycast(transform.position, Vector3.down, distanceToGround))
        {
            isGrounded = false;
            animator.SetBool("isFalling", true);
        }
    }

    public void CheckPushing()
    {
        if (hitPushable && inputReader.interactAction.IsPressed())
        {
            isPushing = true;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag(Tags.TAG_INTERACCIONABLE)){
            GameObject otro = other.gameObject;
            IInteraccionable i = otro.GetComponent<IInteraccionable>();
            if(i != null){
                _objetoInteraccionable = i;
            }
            
        }
        if (other.CompareTag("Pushable"))
        {
            hitPushable = true;
            Debug.Log("enter pushable area");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag(Tags.TAG_INTERACCIONABLE) && _objetoInteraccionable != null){
            _objetoInteraccionable.FinalizarInteraccion();
            _objetoInteraccionable = null;
        }

        if (other.CompareTag("Pushable"))
        {
            hitPushable = false;
            Debug.Log("Leave pushable area");
        }
    }
}
