using UnityEngine;
using UnityEngine.Animations.Rigging;
using Unity.Collections;
using System.Collections.Generic;

public class PlayerStateMachine : StateMachine , IDanhable, IRecuperarSalud
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

    [SerializeField]
    public int _nivelSalud = 1000;

    [SerializeField]
    public int _nivelSaludMaxima = 1000;

    [SerializeField]
    HUDJugador hudJugador;

    //[field: SerializeField] public BoxCollider boxCollider { get; private set; }
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
    public GameObject _pistolaCurar;
    RaycastHit hit;

    public bool _finLevantado;


    public bool _interaccionCurarDisponible;

    public IRecuperarSalud _elementoCurable;

    public GameObject _goTriggerMuerto;

    public GameObject _goFuenteCorazones;

    public bool _contactoMePuedenCurar;

    [SerializeField]
    Color _colorDanhado;

    [SerializeField]
    Color _colorCurando;
    List<Color> _colorBase = new List<Color>();

    List<Material> _materialesCuerpo = new List<Material>();

    [SerializeField]
    Renderer[] _meshRenderersPlayer;    

    Escudo _escudo;

    bool _escudoActivo;


    [SerializeField]
    [Range (0.01f, 2)]
    float _tiempoUltimoHurt;
    float _tiempoUltimoHurtActual;

    bool _isHurt;

    
    public float pushForce;
    private void Start()
    {
        hudJugador.SetNivelSalud(_nivelSalud); 
        _escudo = GetComponent<Escudo>();
        

        if(_meshRenderersPlayer != null && _meshRenderersPlayer.Length >0){
            for(int i = 0; i < _meshRenderersPlayer.Length; i++){
                _materialesCuerpo.Add(_meshRenderersPlayer[i].material);
                _colorBase.Add(_meshRenderersPlayer[i].material.color);
            }
        }


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


    public void RecibirDanho(int danho){
        int danhoRestante = 0;
        if(_escudo.IsActivo()){
            danhoRestante = _escudo.ConsumirEscudo(danho);
        }   else{
            danhoRestante = -danho;
        }

        if(danhoRestante<0){
            _nivelSalud+=danhoRestante;
            if(_nivelSalud<=0){
                _nivelSalud = 0;
                _isHurt = false;
                SetColorHurting();
                animator.SetBool("isHurting", false);
            }   else{
                _isHurt = true;
                ReiniciarTiempoHurt();
                animator.SetBool("isHurting", true);
                Debug.Log("En is hurting");
                SetColorHurting();
            }
            hudJugador.SetNivelSalud(_nivelSalud);
        }

    }

    public bool IsHurt(){
//        Debug.Log("IsHurt "+_nivelSalud+"/"+_nivelSaludMaxima);
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


    public void SetAvisoCurable(bool disponible, IRecuperarSalud elementoCurable){
        _interaccionCurarDisponible = disponible;
        _elementoCurable = elementoCurable;
    }


    public void FinLevantarse(){
        _finLevantado = true;
    }



    public void SetAvisoMePuedenCurar(bool contacto){
        //Debug.Log("Contacto "+contacto);

        if(_materialesCuerpo.Count > 0){
            for(int i = 0; i < _materialesCuerpo.Count; i++){
                _materialesCuerpo[i].color = (contacto && _nivelSalud <= 0) ?_colorCurando:_colorBase[i];
            }
        }
    }

    private void ActivarEscudo(bool activar){
        _escudoActivo = _escudo.ActivarEscudo(activar);        
    }

    public void LogicaEscudoEnTikEstados(){
        if(inputReader.escudoAction.WasPressedThisFrame()){
            ActivarEscudo(true);
        }
        if(inputReader.escudoAction.WasReleasedThisFrame()){
            ActivarEscudo(false);
        }
    }


    private void ReiniciarTiempoHurt(){
        _tiempoUltimoHurtActual = _tiempoUltimoHurt;
    }

    public void LogicaCheckUltimoHurt(){
        if(_isHurt){
            _tiempoUltimoHurtActual-=Time.deltaTime;
            if(_tiempoUltimoHurtActual<=0){
                _isHurt = false;
                animator.SetBool("isHurting", false);
                SetColorHurting();
            }
        }
    }

    public HUDJugador GetHUDJugador(){
        return hudJugador;
    }


    public void SetColorHurting(){

        if(_materialesCuerpo.Count > 0){
            for(int i = 0; i < _materialesCuerpo.Count; i++){
                _materialesCuerpo[i].color = _isHurt ?_colorDanhado:_colorBase[i];
            }
        }
    }

}
