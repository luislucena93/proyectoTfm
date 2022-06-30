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
    [field: SerializeField] public bool isPushing { get; private set; }
    [field: SerializeField] public float jumpForce { get; private set; }
    [field: SerializeField] public GameObject _ikReferenciaMano { get; private set; }
    [field: SerializeField] public Rig _ikRigMano { get; private set; }
    [field: SerializeField] public float distanceToGround { get; private set; }

    [field: SerializeField] public DialogueManager dialogueManager;

    public bool isGrounded;
    public bool isJumping;
    public float gravity;
    public Vector3 velocity;
    public Vector3 inputDirection;
    public Vector2 movementValue;
    public IInteraccionable _objetoInteraccionable;
    public GameObject _pistolaReparacion;

    private void Start() 
    {
        // Estado inicial, dando como referencia este PlayerStateMachine
        SwitchState(new PlayerIdleState(this));
    }

    private void FixedUpdate() 
    {
        GroundCheck();
    }

    private void OnControllerColliderHit(ControllerColliderHit hit) 
    {
        if (hit.transform.CompareTag("Ground") && !isGrounded) 
        {
            isGrounded = true;
            animator.SetBool("isFalling", false);
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Pushable")
        {
            isPushing = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Pushable")
        {
            isPushing = false;
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
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag(Tags.TAG_INTERACCIONABLE) && _objetoInteraccionable != null){
            _objetoInteraccionable.FinalizarInteraccion();
            _objetoInteraccionable = null;
        }
    }
}
