using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDetectarPush : MonoBehaviour
{
    IDetectadoPushableListener _listenerDetectadoPushable;

    private void Start() {
        _listenerDetectadoPushable = GetComponentInParent<IDetectadoPushableListener>();
    }


    private void OnTriggerEnter(Collider other) {
        CheckPushableEnter(other);
    }

    private void OnTriggerStay(Collider other) { 
    }

    private void OnTriggerExit(Collider other) {
       CheckPushableExit(other);
    }

    private void OnCollisionEnter(Collision other) {
        CheckPushableEnter(other.collider);
    }

    private void OnCollisionStay(Collision other) { 
    }

    private void OnCollisionExit(Collision other) {
        CheckPushableExit(other.collider);

    }

    void CheckPushableEnter(Collider other){
        IPushable pushable = other.gameObject.GetComponent<IPushable>();
        if(pushable != null){
            pushable.SetAvisoPushable(true);
            _listenerDetectadoPushable.DetectadoPushable(true, pushable);
        }
    }

    void CheckPushableExit(Collider other){
        IPushable pushable = other.gameObject.GetComponent<IPushable>();
        if(pushable != null){
            pushable.SetAvisoPushable(false);
            _listenerDetectadoPushable.DetectadoPushable(false, null);
        }
    }
}