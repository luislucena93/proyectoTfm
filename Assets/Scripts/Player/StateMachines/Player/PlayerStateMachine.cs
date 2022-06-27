using UnityEngine;
using UnityEngine.Animations.Rigging;

public class PlayerStateMachine : StateMachine , IDanhable, IRecuperarSalud
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
    [field: SerializeField] public float jumpForce { get; private set; }
    [field: SerializeField] public GameObject _ikReferenciaMano { get; private set; }
    [field: SerializeField] public Rig _ikRigMano { get; private set; }
    [field: SerializeField] public float distanceToGround { get; private set; }

    [SerializeField]
    public int _nivelSalud = 1000;

    [SerializeField]
    public int _nivelSaludMaxima = 1000;

    [SerializeField]
    HUDJugador hudJugador;

    //[field: SerializeField] public BoxCollider boxCollider { get; private set; }
    public bool isGrounded;
    public bool isJumping;
    public float gravity;
    public Vector3 velocity;
    public Vector3 inputDirection;
    public Vector2 movementValue;
    public IInteraccionable _objetoInteraccionable;
    public GameObject _pistolaReparacion;
    RaycastHit hit;

    bool _curableAlAlcance;

    private void Start() 
    {
        hudJugador.SetNivelSalud(_nivelSalud); 
        hudJugador.SetNivelSaludMaxima(_nivelSaludMaxima); 

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


    public void RecibirDanho(int danho){
        _nivelSalud-=danho;
        if(_nivelSalud<0){
            _nivelSalud = 0;
        }
        hudJugador.SetNivelSalud(_nivelSalud);
    }

    public bool IsHurt(){
        Debug.Log("IsHurt "+_nivelSalud+"/"+_nivelSaludMaxima);
        return _nivelSalud < _nivelSaludMaxima;
    }
    public void RecuperarSalud(int puntosSalud){
        _nivelSalud += puntosSalud;
        if(_nivelSalud>_nivelSaludMaxima){
            _nivelSalud = _nivelSaludMaxima;
        }
        hudJugador.SetNivelSalud(_nivelSalud);     
    }

    public  bool IsDead(){
        return _nivelSalud <= 0;
    }


    public void SetAvisoCurable(bool curableAlAlcance){
        this._curableAlAlcance = curableAlAlcance;
    }
}
