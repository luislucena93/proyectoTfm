using UnityEngine;
using UnityEngine.Animations.Rigging;

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

    [field: SerializeField] public bool isPushing { get; private set; }


    public IInteraccionable _objetoInteraccionable;

    public GameObject _pistolaReparacion;

    [field: SerializeField] public GameObject _ikReferenciaMano { get; private set; }

    [field: SerializeField] public Rig _ikRigMano { get; private set; }

    //[field: SerializeField] public BoxCollider boxCollider { get; private set; }

    private void Start() 
    {
        //MainCameraTransform = Camera.main.transform;

        // Estado inicial, dando como referencia este PlayerStateMachine
        SwitchState(new PlayerMoveState(this));
    }

    public bool IsGrounded() {
        Vector3 targetCenter = playerCollider.bounds.center;
        return Physics.Raycast(targetCenter, Vector3.down, minDistanceToGround);
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
