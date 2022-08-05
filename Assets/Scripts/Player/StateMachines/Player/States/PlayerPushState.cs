using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPushState : PlayerBaseState {
    public PlayerPushState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    float _tiempoTransicionCongelar = 0.4f;
    IPushable _iPushable;
    Rigidbody _rbPush;

    Vector3 _v3DiferenciaJugadorCaja;

    float _tiempoActualActualizarPlayer = 0;
    float _tiempoActualizarPlayer = 0.06f;


    bool _puedeMoverCaja = false;
    bool _ligeraFuerte = false;


    public override void Enter() {
        Debug.Log("Enter Push");
        _iPushable = stateMachine._iPushableDetectado;
        _iPushable.SetPushing(true);
        stateMachine.animator.SetBool("isPushing", true);
        stateMachine.animator.speed = 1;

        _rbPush = _iPushable.GetRigidBody();
        Vector3 v3Translacion = stateMachine.transform.position - _rbPush.gameObject.transform.position;
        v3Translacion.y = 0;
        stateMachine.characterController.Move(v3Translacion.normalized*_iPushable.GetPaddingJugador());

        Physics.SyncTransforms();

        Debug.Log("Posicion rb enter " + _rbPush.gameObject.transform.position);
        _v3DiferenciaJugadorCaja = stateMachine.characterController.transform.position - _rbPush.gameObject.transform.position;

        TipoCajaEnum tipoCaja = _iPushable.GetTipoCajaEnum();
        if(stateMachine._fuerzaTipoCaja == TipoCajaEnum.Pesada){
            _puedeMoverCaja = true;
            _ligeraFuerte = tipoCaja ==TipoCajaEnum.Ligera;
        }   else{
            _puedeMoverCaja = tipoCaja ==TipoCajaEnum.Ligera;
        }
        

    }

    public override void Tick(float deltaTime) {
        //Debug.Log("Ejecutando estado Push");

        stateMachine.LogicaEscudoEnTikEstados();

        if(stateMachine._nivelSalud<=0){
            stateMachine.SwitchState(new PlayerDeadState(stateMachine));
            return;
        }

        if (!stateMachine.inputReader.interactAction.IsPressed()) {
            stateMachine.isPushing = false;
            stateMachine.SwitchState(new PlayerIdleState(stateMachine));
            return;
        }

        stateMachine.movementValue = stateMachine.inputReader.moveAction.ReadValue<Vector2>();
        stateMachine.inputDirection = CalculateMovement(stateMachine.movementValue);

        if(_puedeMoverCaja && !_iPushable.GetBloquearMoviento()){
            PlayerMovement(stateMachine.inputDirection * stateMachine.speed, deltaTime);
        }   else{
            //Debug.Log("No puedo mover caja");
            PlayerMovementNoPuedoMoverCaja(stateMachine.inputDirection * stateMachine.speed, deltaTime);
        }
        
    }

    public override void Exit() {
        Debug.Log("Exit Push");
        stateMachine.animator.speed = 1f;
        stateMachine.isPushing = false;
        stateMachine.animator.SetBool("isPushing", false);
        if(stateMachine._iPushableDetectado!=null){
            stateMachine._iPushableDetectado.SetPushing(false);
        }
        
    }

    private Vector3 CalculateMovement(Vector3 movementValue)
    {
        Vector3 forward = stateMachine.gameObject.transform.forward;
        
        forward.y = 0f;

        forward.Normalize();
        return movementValue.y > 0 ? forward * movementValue.y : forward * 0;
    }

    protected void PlayerMovement(Vector3 movement, float deltaTime)
    {
        if(_tiempoTransicionCongelar > 0){
            _tiempoTransicionCongelar -= deltaTime;
            //Debug.Log("_tiempoRestanteCongelar "+_tiempoTransicionCongelar);
        }   else{
            if(movement.magnitude > 0){
                stateMachine.animator.speed = 1f;
                Vector3 fuerza = movement.normalized * stateMachine.pushForce/4;
                if(_ligeraFuerte){
                    fuerza*=1.3f;
                }

                _rbPush.AddForce(movement.normalized * stateMachine.pushForce/4,ForceMode.VelocityChange);
            } else
            {
                stateMachine.animator.speed = 0f;
            }
        }
        Physics.SyncTransforms();
        _tiempoActualActualizarPlayer-=_tiempoActualizarPlayer;
       
        //Vector3 desplazamiento = _rbPush.gameObject.transform.position -  _posicionPushableAnterior;
        /*
        Debug.Log("desplazamiento "+desplazamiento.x+" , "+desplazamiento.y+" , "+desplazamiento.z );
        Debug.Log("anterior "+_posicionPushableAnterior.x+" , "+_posicionPushableAnterior.y+" , "+_posicionPushableAnterior.z);
        Debug.Log(" actual "+_rbPush.gameObject.transform.position.x+" , "+_rbPush.gameObject.transform.position.y+" , "+_rbPush.gameObject.transform.position.z );
        */
        //stateMachine.characterController.enabled = false;
        stateMachine.characterController.transform.position = _rbPush.gameObject.transform.position + _v3DiferenciaJugadorCaja;
        //stateMachine.characterController.enabled = true;

        Physics.SyncTransforms();

        /*
        Debug.Log("desplazamiento "+desplazamiento.x+" , "+desplazamiento.y+" , "+desplazamiento.z );
        Debug.Log("anterior "+_posicionPushableAnterior.x+" , "+_posicionPushableAnterior.y+" , "+_posicionPushableAnterior.z);
        Debug.Log(" actual "+_rbPush.gameObject.transform.position.x+" , "+_rbPush.gameObject.transform.position.y+" , "+_rbPush.gameObject.transform.position.z );
        */
    }

    protected void PlayerMovementNoPuedoMoverCaja(Vector3 movement, float deltaTime){
        
        if(_tiempoTransicionCongelar > 0){
            _tiempoTransicionCongelar -= deltaTime;
           // Debug.Log("_tiempoRestanteCongelar "+_tiempoTransicionCongelar);
        }   else{
            if(movement.magnitude > 0){
                stateMachine.animator.speed = 0.3f;
            } else
            {
                stateMachine.animator.speed = 0f;
            }
        }
    }

}
